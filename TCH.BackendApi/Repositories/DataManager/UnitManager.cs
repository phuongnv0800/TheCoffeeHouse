using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.Utilities.Claims;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class UnitManager : IDisposable, IUnitRepository
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;

    public UnitManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<Respond<PagedList<MeasuresVm>>> GetAll(Search request)
    {
        var query = from c in _context.Measures select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<MeasuresVm>();
        if (request.IsPging)
        {
            data = await query
                .Select(x => _mapper.Map<MeasuresVm>(x))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
        {
            data = await query.Select(x => _mapper.Map<MeasuresVm>(x)).ToListAsync();
        }
        return new Respond<PagedList<MeasuresVm>>()
        {
            Result = 1,
            Message = "Thành công",
            Data = new PagedList<MeasuresVm>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            },
        };
    }

    public async Task<MessageResult> Create(UnitRequest request)
    {
        var category = _mapper.Map<Measure>(request);
        category.MeasureID = Guid.NewGuid().ToString();
        category.UpdateDate = DateTime.Now;
        category.CreateDate = DateTime.Now;
        category.UserCreateID = UserID;
        category.UserUpdateID = UserID;
        _context.Measures.Add(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> Update(string id, UnitRequest request)
    {
        var category = await _context.Measures.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        category.Name = request.Name ?? "";
        category.Code = request.Code;
        category.UpdateDate = DateTime.Now;
        category.UserUpdateID = UserID;

        _context.Measures.Update(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> Delete(string id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> CreateExchangeUnit(ExchangeUnitRequest request)
    {
        var unit = await _context.UnitConversions
            .Where(x => x.DestinationUnitID == request.DestinationUnitID && x.SourceUnitID == request.SourceUnitID)
            .FirstOrDefaultAsync();
        if (unit != null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Đã tồn tại",
            };
        }
        var unitConversion = _mapper.Map<UnitConversion>(request);
        await _context.UnitConversions.AddAsync(unitConversion);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> UpdateExchangeUnit(ExchangeUnitRequest request)
    {
        var unit = await _context.UnitConversions
            .Where(x => x.DestinationUnitID == request.DestinationUnitID && x.SourceUnitID == request.SourceUnitID)
            .FirstOrDefaultAsync();
        if (unit == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        var unitConversion = _mapper.Map<UnitConversion>(request);

        _context.UnitConversions.Update(unitConversion);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }
    public async Task<Respond<List<UnitConversion>>> GetAllExchangeUnit()
    {
        var data = await _context.UnitConversions.ToListAsync();
        if (data == null)
        {
            return new Respond<List<UnitConversion>>()
            {
                Result = 0,
                Message = "Chưa có dữ liệu",
                Data = null,
            };
        }
        return new Respond<List<UnitConversion>>()
        {
            Result = 1,
            Message = "Thành công",
            Data = data,
        };
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
