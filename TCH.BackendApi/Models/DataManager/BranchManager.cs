using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataManager;

public class BranchManager : IDisposable, IBranchRepository
{

    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;

    public BranchManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<Respond<PagedList<Branch>>> GetAll(Search request)
    {
        var query = from c in _context.Branches select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        List<Branch> data = new List<Branch>();
        if (request.IsPging == true)
        {
            data = await query
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .ToListAsync();
        }
        else
            data = await query.ToListAsync();

        // select
        var pagedResult = new PagedList<Branch>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Branch>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> AddUserToBranch(string userID, string branchID)
    {
        var entity = await _context.Branches.FindAsync(branchID);
        var user = await _context.Users.FindAsync(userID);
        if (user == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "User không tồn tại",
            };
        }
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Chi nhánh không tồn tại",
            };
        }
        var userBranch = new UserBranch()
        {
            User = user,
            UserId = userID,
            Branch = entity,
            BranchID = branchID,
        };
        _context.UserBranches.Add(userBranch);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }
    public async Task<MessageResult> RemoveUserToBranch(string userID, string branchID)
    {
        var entity = await _context.UserBranches.FirstOrDefaultAsync(x => x.BranchID == branchID && x.UserId == userID);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Tài khoản hoặc chi nhánh không tồn tại",
            };
        }

        _context.UserBranches.Remove(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tài khoản đã xoá khỏi chi nhánh",
        };
    }
    public async Task<MessageResult> Create(BranchRequest request)
    {
        var entity = _mapper.Map<Branch>(request);
        entity.ID = Guid.NewGuid().ToString();
        entity.UpdateDate = DateTime.Now;
        entity.CreateDate = DateTime.Now;
        _context.Branches.Add(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }
    public async Task<MessageResult> Update(string id, BranchRequest request)
    {
        var entity = await _context.Branches.FindAsync(id);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        entity.Name = request.Name ?? entity.Name;
        entity.City = request.City ?? entity.City;
        entity.Email = request.Email ?? entity.Email;
        entity.District = request.District ?? entity.District;
        entity.Adderss = request.Adderss ?? entity.Adderss;
        entity.UpdateDate = DateTime.Now;
        _context.Branches.Update(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> Delete(string id)
    {
        var entity = await _context.Branches.FindAsync(id);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.Branches.Remove(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }

    public async Task<Respond<Branch>> GetByID(string branchID)
    {
        var branch = await _context.Branches.FindAsync(branchID);
        if(branch == null)
        {
            return new Respond<Branch>()
            {
                Data = null,
                Message = "Không tồn tại",
                Result = 0,
            };
        }
        return new Respond<Branch>()
        {
            Data = branch,
            Message = "Thành công",
            Result = 1,
        };
    }
}
