﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.SubModels;
using TCH.Utilities.Searchs;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.BackendApi.EF;
using System.Text.Json;
using TCH.Utilities.Enum;
using TCH.Utilities.Claims;

namespace TCH.BackendApi.Repositories.DataRepository;

public class UserManager : IUserRepository, IDisposable
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly APIContext _context;
    private readonly IConfiguration _config;
    private readonly IStorageService _storageService;
    private const string USER_CONTENT_FOLDER_NAME = "users";


    public UserManager(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager,
        APIContext context,
        IConfiguration config,
        IStorageService storageService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _config = config;
        _storageService = storageService;
    }

    public async Task<Respond<dynamic>> Authenicate(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return new Respond<dynamic>()
            {
                Result = 0,
                Message = "Tài khoản không tồn tại",
                Data = null,
            };
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
        if (!result.Succeeded)
        {
            return new Respond<dynamic>()
            {
                Result = 0,
                Message = "Tài khoản mật khẩu không chính xác",
                Data = null,
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? "" + " " + user.LastName),
            new Claim(ClaimTypes.Role, string.Join(";", roles)),
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimValue.Displayname, user.LastName ?? ""),
            new Claim(ClaimValue.BranhID,user.BranchID ?? ""),
            new Claim(ClaimValue.ID, user.Id),
            new Claim(ClaimValue.Role, JsonSerializer.Serialize(roles)),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_config["Jwt:ExpiryInDays"]));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );
        return new Respond<dynamic>()
        {
            Result = 1,
            Message = "Đăng nhập thành công",
            Data = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }

    public async Task<MessageResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Tài khoản không tồn tại",
            };
        await _storageService.DeleteFileAsync(USER_CONTENT_FOLDER_NAME + "/" + user.Avatar);
        var result = await _userManager.DeleteAsync(user);//xoa user
        if (result.Succeeded)
            return new MessageResult()
            {
                Result = 1,
                Message = "Xoá tài khoản thành công",
            };
        return new MessageResult()
        {
            Result = 0,
            Message = "Xoá tài khoản thất bại",
        };
    }

    public async Task<Respond<UserVm>> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return new Respond<UserVm>()
            {
                Result = -1,
                Message = "Tài khoản không tồn tại",
                Data = new UserVm(),
            };

        var roles = await _userManager.GetRolesAsync(user);
        var userVm = new UserVm()
        {
            Email = user.Email,
            FirstName = user.FirstName ?? "",
            LastName = user.LastName ?? "",
            Id = user.Id,
            DateOfBirth = user.DateOfBirth,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Gender = user.Gender,
            Address = user.Address ?? "",
            Status = user.Status,
            Avatar = USER_CONTENT_FOLDER_NAME + "/" + user.Avatar,
            Roles = roles,
        };
        return new Respond<UserVm>()
        {
            Result = 1,
            Message = "Lấy thông tin thành công",
            Data = userVm,
        };
    }

    public async Task<Respond<UserVm>> GetByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            return new Respond<UserVm>()
            {
                Result = 0,
                Message = "Tài khoản không tồn tại",
                Data = new UserVm(),
            };

        var roles = await _userManager.GetRolesAsync(user);
        var userVm = new UserVm()
        {
            Email = user.Email,
            FirstName = user.FirstName ?? "",
            LastName = user.LastName ?? "",
            Id = user.Id,
            DateOfBirth = user.DateOfBirth,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Gender = user.Gender,
            Address = user.Address ?? "",
            Status = user.Status,
            Avatar = USER_CONTENT_FOLDER_NAME + "/" + user.Avatar,
            Roles = roles,
        };
        return new Respond<UserVm>()
        {
            Result = 1,
            Message = "Lấy thông tin thành công",
            Data = userVm,
        };
    }

    public async Task<PagedList<UserVm>> GetAll(Search request)
    {
        var query = _userManager.Users;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.UserName.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<UserVm>();
        if (request.IsPging)
            data = await query
                .Select(
                    x => new UserVm()
                    {
                        Email = x.Email,
                        FirstName = x.FirstName ?? "",
                        LastName = x.LastName ?? "",
                        UserName = x.UserName,
                        Id = x.Id,
                        PhoneNumber = x.PhoneNumber,
                        DateOfBirth = x.DateOfBirth,
                        Gender = x.Gender,
                        Status = x.Status,
                        Address = x.Address ?? "",
                        Avatar = USER_CONTENT_FOLDER_NAME + "/" + x.Avatar,
                    }
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        else
            data = await query
                .Select(
                    x => new UserVm()
                    {
                        Email = x.Email,
                        FirstName = x.FirstName ?? "",
                        LastName = x.LastName ?? "",
                        UserName = x.UserName,
                        Id = x.Id,
                        PhoneNumber = x.PhoneNumber,
                        DateOfBirth = x.DateOfBirth,
                        Gender = x.Gender,
                        Status = x.Status,
                        Address = x.Address ?? "",
                        Avatar = USER_CONTENT_FOLDER_NAME + "/" + x.Avatar,
                    }
                ).ToListAsync();
        var pagedResult = new PagedList<UserVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return pagedResult;
    }

    public async Task<MessageResult> Register(RegisterRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
            return new MessageResult()
            {
                Message = "Tài khoản đã tồn tại",
                Result = -1,
            };
        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return new MessageResult()
            {
                Message = "Email đã được sử dụng",
                Result = 0,
            };

        user = new AppUser()
        {
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            Address = request.Address,
            BranchID =request.branchID,
            Status = Status.Active,
        };
        if (request.AvatarFile != null)
            user.Avatar = await SaveFileIFormFile(request.AvatarFile);

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
            return new MessageResult()
            {
                Message = "Tạo tài khoản thành công",
                Result = 1,
            };
        return new MessageResult()
        {
            Message = "Tạo tài khoản thất bại",
            Result = -2,
        };
    }

    public async Task<MessageResult> RoleAssign(string id, RoleAssignRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không thể cập nhật quyền hạn",
            };
        }
        var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();

        //await _userManager.RemoveFromRolesAsync(user, removedRoles);
        foreach (var roleName in removedRoles)
        {
            if (await _userManager.IsInRoleAsync(user, roleName) == true)
                await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
        foreach (var roleName in addedRoles)
        {
            if (await _userManager.IsInRoleAsync(user, roleName) == false)
                await _userManager.AddToRoleAsync(user, roleName);
        }
        return new MessageResult()
        {
            Result = 1,
            Message = "Phân quyền thành công",
        };
    }

    public async Task<MessageResult> Update(string id, UserUpdateRequest request)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không thể cập nhật thông tin"
            };
        }
        var user = await _userManager.FindByIdAsync(id.ToString());
        user.DateOfBirth = request.Dob;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Gender = request.Gender;
        user.Address = request.Address;
        if (request.AvatarFile != null)
        {
            await _storageService.DeleteFileAsync(USER_CONTENT_FOLDER_NAME + "/" + user.Avatar);
            user.Avatar = await SaveFileIFormFile(request.AvatarFile);
        }
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Cập nhật thông tin thành công",
            };
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Không thể cập nhật thông tin",
        };
    }
    public async Task<MessageResult> LockUser(string id)
    {
        if (await _userManager.Users.AnyAsync(x => x.Id != id))
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tồn tại tài khoản"
            };
        }
        var user = await _userManager.FindByIdAsync(id.ToString());
        user.Status = Status.Deactivate;
        await _userManager.UpdateAsync(user);
        return new MessageResult()
        {
            Result = 0,
            Message = "Khoá tài khoản thành công",
        };
    }
    public async Task<MessageResult> ChangePasword(ChangePassword req)
    {
        if (await _userManager.FindByNameAsync(req.UserName) == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Tài khoản không tồn tại",
            };
        }
        var user = await _userManager.FindByLoginAsync(req.UserName, req.PasswordOld);
        if (user == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Mật khẩu không chính xác",
            };
        }
        if (req.PasswordNew.Equals(req.PasswordConfirm) == false)
        {
            return new MessageResult()
            {
                Result = -1,
                Message = "Mật khẩu mới khác nhau",
            };
        }
        var hasher = new PasswordHasher<AppUser>();
        user.PasswordHash = hasher.HashPassword(null, req.PasswordNew);
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Thay đổi mật khẩu thành công",
            };
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Không thể cập nhật mật khẩu",
        };
    }

    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), USER_CONTENT_FOLDER_NAME + "/" + fileName);
        return fileName;
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}