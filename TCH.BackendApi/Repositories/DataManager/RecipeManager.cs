//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using TCH.BackendApi.EF;
//using TCH.BackendApi.Repositories.DataRepository;
//using TCH.Data.Entities;
//using TCH.Utilities.Claims;
//using TCH.Utilities.Error;
//using TCH.Utilities.Paginations;
//using TCH.Utilities.Searchs;
//using TCH.Utilities.SubModels;

//namespace TCH.BackendApi.Repositories.DataManager;

//public class RecipeManager
//{
//    private readonly APIContext _context;
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly string? UserID;
//    private readonly string _accessToken;
//    private readonly IMapper _mapper;
//    private const string Upload = "branch";
//    private readonly IStorageService _storageService;

//    public RecipeManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext, IStorageService storageService)
//    {
//        _context = context;
//        _httpContextAccessor = httpContext;
//        _storageService = storageService;
//        _mapper = mapper;
//        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
//        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
//    }
//    public async Task<Respond<PagedList<RecipeDetail>>> GetRecipeByProductID(string productID)
//    {
//        //var query = await _context.RecipeDetails
//        //    .Include(x => x.Product)
//        //    .Include(x=>x.Size)
//        //    .Include(x=>x.Material)
//        //    .Where(x => x.ProductID == productID).ToListAsync();

//        ////paging
//        //int totalRow = query.Count;
//        //if (request.IsPging)
//        //    query = query
//        //        .Skip((request.PageNumber - 1) * request.PageSize)
//        //        .Take(request.PageSize)
//        //        .ToList();
//        //var pagedResult = new PagedList<AppUser>()
//        //{
//        //    TotalRecord = totalRow,
//        //    PageSize = request.PageSize,
//        //    CurrentPage = request.PageNumber,
//        //    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
//        //    Items = query,
//        //};
//        //return new Respond<PagedList<AppUser>>()
//        //{
//        //    Data = pagedResult,
//        //    Result = 1,
//        //    Message = "Success",
//        //};
//        return null;
//    } 
//    public async Task<Respond<PagedList<Branch>>> GetAll(Search request)
//    {
//        var query = from c in _context.Branches select c;
//        if (!string.IsNullOrEmpty(request.Name))
//        {
//            query = query.Where(x => x.Name.Contains(request.Name));
//        }
//        //paging
//        int totalRow = await query.CountAsync();
//        List<Branch> data = new List<Branch>();
//        if (request.IsPging == true)
//        {
//            data = await query
//           .Skip((request.PageNumber - 1) * request.PageSize)
//           .Take(request.PageSize)
//           .ToListAsync();
//        }
//        else
//            data = await query.ToListAsync();

//        // select
//        var pagedResult = new PagedList<Branch>()
//        {
//            TotalRecord = totalRow,
//            PageSize = request.PageSize,
//            CurrentPage = request.PageNumber,
//            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
//            Items = data,
//        };
//        return new Respond<PagedList<Branch>>()
//        {
//            Data = pagedResult,
//            Result = 1,
//            Message = "Thành công",
//        };
//    }
//    public async Task<MessageResult> AddUserToBranch(string userID, string branchID)
//    {
//        var entity = await _context.Branches.FindAsync(branchID);
//        var user = await _context.Users.FindAsync(userID);
//        if (user == null)
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "User không tồn tại",
//            };

//        if (entity == null)
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "Chi nhánh không tồn tại",
//            };
//        user.BranchID = entity.ID;
//        user.Branch = entity;
//        _context.Users.Update(user);
//        await _context.SaveChangesAsync();
//        return new MessageResult()
//        {
//            Result = 1,
//            Message = "Tạo thành công",
//        };
//    }
//    public async Task<MessageResult> RemoveUserToBranch(string userID, string branchID)
//    {
//        var entity = await _context.Branches.FindAsync(branchID);
//        var user = await _context.Users.FindAsync(userID);
//        if (user == null)
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "User không tồn tại",
//            };

//        if (entity == null)
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "Chi nhánh không tồn tại",
//            };
//        if ((user.BranchID ?? "") != entity.ID)
//        {
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "Tài khoản không trong chi nhánh",
//            };
//        }
//        user.BranchID = null;
//        _context.Users.Update(user);
//        await _context.SaveChangesAsync();
//        return new MessageResult()
//        {
//            Result = 1,
//            Message = "Tài khoản đã xoá khỏi chi nhánh",
//        };
//    }
//    public async Task<MessageResult> Create(BranchRequest request)
//    {
//        var entity = _mapper.Map<Branch>(request);
//        entity.ID = Guid.NewGuid().ToString();
//        entity.UpdateDate = DateTime.Now;
//        entity.CreateDate = DateTime.Now;
//        entity.UserCreateID = UserID;
      
//        if (request.ImageUpload != null)
//        {
//            try
//            {
//                var nameFile = await SaveFileIFormFile(request.ImageUpload);
//                entity.LinkImage = Upload + "/" + nameFile;
//            }
//            catch (Exception e)
//            {
//                throw new CustomException("Save File Create Error: " + e.Message);
//            }

//        }
//        _context.Branches.Add(entity);
//        await _context.SaveChangesAsync();
//        return new MessageResult()
//        {
//            Result = 1,
//            Message = "Tạo thành công",
//        };
//    }
//    public async Task<MessageResult> Update(string id, BranchRequest request)
//    {
//        var entity = await _context.Branches.FindAsync(id);
//        if (entity == null)
//        {
//            return new MessageResult()
//            {
//                Result = 1,
//                Message = "Không tìm thấy",
//            };
//        }
//        entity.Name = request.Name ?? entity.Name;
//        entity.City = request.City ?? entity.City;
//        entity.Email = request.Email ?? entity.Email;
//        entity.District = request.District ?? entity.District;
//        entity.Adderss = request.Adderss ?? entity.Adderss;
//        entity.UpdateDate = DateTime.Now;
//        if (request.ImageUpload != null)
//        {
//            try
//            {
//                if (entity.LinkImage != null)
//                {
//                    await _storageService.DeleteFileAsync(entity.LinkImage);
//                }
//                var nameFile = await SaveFileIFormFile(request.ImageUpload);
//                entity.LinkImage = Upload + "/" + nameFile;
//            }
//            catch (Exception e)
//            {
//                throw new CustomException("Save File update Error: " + e.Message);
//            }
//        }
//        _context.Branches.Update(entity);
//        await _context.SaveChangesAsync();
//        return new MessageResult()
//        {
//            Result = 1,
//            Message = "Cập nhật thành công",
//        };
//    }

//    public async Task<MessageResult> Delete(string id)
//    {
//        var entity = await _context.Branches.FindAsync(id);
//        if (entity == null)
//        {
//            return new MessageResult()
//            {
//                Result = 0,
//                Message = "Không tìm thấy",
//            };
//        }
//        if (entity.LinkImage != null)
//        {
//            try
//            {
//                await _storageService.DeleteFileAsync(entity.LinkImage);
//            }
//            catch
//            {
//                throw new CustomException("Failed delete file");
//            }
//        }
//        _context.Branches.Remove(entity);
//        await _context.SaveChangesAsync();
//        return new MessageResult()
//        {
//            Result = 1,
//            Message = "Xoá thành công",
//        };
//    }
//    public async Task<Respond<Branch>> GetByID(string branchID)
//    {
//        var branch = await _context.Branches.FindAsync(branchID);
//        if (branch == null)
//        {
//            return new Respond<Branch>()
//            {
//                Data = null,
//                Message = "Không tồn tại",
//                Result = 0,
//            };
//        }
//        return new Respond<Branch>()
//        {
//            Data = branch,
//            Message = "Thành công",
//            Result = 1,
//        };
//    }
//    private async Task<string> SaveFileIFormFile(IFormFile file)
//    {
//        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
//        await _storageService.SaveFileAsync(file.OpenReadStream(), Upload + "/" + fileName);
//        return fileName;
//    }

//    public void Dispose()
//    {
//        GC.Collect(2, GCCollectionMode.Forced, true);
//        GC.WaitForPendingFinalizers();
//        GC.SuppressFinalize(this);
//    }
//}
