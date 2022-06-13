using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.Utilities.Claims;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class StockManager : IDisposable, IStockRepository
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;
    private const string Upload = "branch";
    private readonly IStorageService _storageService;

    public StockManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext,
        IStorageService storageService)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _storageService = storageService;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }

    public async Task<Respond<PagedList<StockVm>>> GetAllStockByBranchID(string branchID, Search request)
    {
        var query = from c in _context.StockMaterials
            join b in _context.Branches on c.BranchID equals b.ID
            join m in _context.Materials on c.MaterialID equals m.ID
            join mea in _context.Measures on c.MeasureID equals mea.ID
            where c.BranchID == branchID && DateTime.Compare(c.ExpirationDate, DateTime.Now) >= 0
            select new {c, b, m, mea};
        if (request.StartDate != null)
            query = query.Where(x => DateTime.Compare(x.c.BeginDate, (DateTime) request.StartDate) <= 0);

        //paging
        int totalRow = await query.CountAsync();
        List<StockVm> data = new List<StockVm>();
        if (request.IsPging == true)
        {
            data = await query.Select(x =>
                    new StockVm()
                    {
                        ID = x.c.ID,
                        BranchID = branchID,
                        BranchName = x.c.Branch.Name,
                        MaterialName = x.m.Name,
                        Quantity = x.c.Quantity,
                        BeginDate = x.c.BeginDate,
                        ExpirationDate = x.c.ExpirationDate,
                        Status = x.c.Status,
                        PriceOfUnit = x.c.PriceOfUnit,
                        IsDelete = x.c.IsDelete,
                        Mass = x.c.Mass,
                        StandardMass = x.c.StandardMass,
                        MeasureName = x.mea.Name,
                        MeasureType = x.c.MeasureType,
                        Description = x.c.Description,
                        MaterialID = x.c.MaterialID,
                        MeasureID = x.c.MeasureID,
                    }
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
            data = await query.Select(x =>
                new StockVm()
                {
                    ID = x.c.ID,
                    BranchID = branchID,
                    BranchName = x.c.Branch.Name,
                    MaterialName = x.m.Name,
                    Quantity = x.c.Quantity,
                    BeginDate = x.c.BeginDate,
                    ExpirationDate = x.c.ExpirationDate,
                    Status = x.c.Status,
                    PriceOfUnit = x.c.PriceOfUnit,
                    IsDelete = x.c.IsDelete,
                    Mass = x.c.Mass,
                    StandardMass = x.c.StandardMass,
                    MeasureName = x.mea.Name,
                    MeasureType = x.c.MeasureType,
                    Description = x.c.Description,
                    MaterialID = x.c.MaterialID,
                    MeasureID = x.c.MeasureID,
                }
            ).ToListAsync();

        // select
        var pagedResult = new PagedList<StockVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockVm>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<MessageResult> NotificationMaterialInStock(string branchId)
    {
        var query = await _context
            .StockMaterials
            .Include(x => x.Material)
            .Where(x => x.BranchID == branchId && x.StandardMass <= 100)
            .Select(x => x.Material.Name)
            .ToListAsync();
        if (query == null || query.Count == 0)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Nguyên liệu đủ dùng",
            };
        }
        return new MessageResult()
        {
            Message ="Những nguyên liệu này sắp hết: "+ string.Join(", ", query),
            Result = 1,
        };
    }

    public async Task<Respond<PagedList<StockVm>>> GetAllStockExpireByBranchID(string branchID, Search request)
    {
        var query = from c in _context.StockMaterials
            join b in _context.Branches on c.BranchID equals b.ID
            join m in _context.Materials on c.MaterialID equals m.ID
            join mea in _context.Measures on c.MeasureID equals mea.ID
            where c.BranchID == branchID && DateTime.Compare(c.ExpirationDate, DateTime.Now) < 0
            select new {c, b, m, mea};
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.m.Name.Contains(request.Name));
        }

        //paging
        int totalRow = await query.CountAsync();
        List<StockVm> data = new List<StockVm>();
        if (request.IsPging == true)
        {
            data = await query.Select(x =>
                    new StockVm()
                    {
                        ID = x.c.ID,
                        BranchID = branchID,
                        BranchName = x.c.Branch.Name,
                        MaterialName = x.m.Name,
                        Quantity = x.c.Quantity,
                        BeginDate = x.c.BeginDate,
                        ExpirationDate = x.c.ExpirationDate,
                        Status = x.c.Status,
                        PriceOfUnit = x.c.PriceOfUnit,
                        IsDelete = x.c.IsDelete,
                        Mass = x.c.Mass,
                        StandardMass = x.c.StandardMass,
                        MeasureName = x.mea.Name,
                        MeasureType = x.c.MeasureType,
                        Description = x.c.Description,
                        MaterialID = x.c.MaterialID,
                        MeasureID = x.c.MeasureID,
                    }
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
            data = await query.Select(x =>
                new StockVm()
                {
                    ID = x.c.ID,
                    BranchID = branchID,
                    BranchName = x.c.Branch.Name,
                    MaterialName = x.m.Name,
                    Quantity = x.c.Quantity,
                    BeginDate = x.c.BeginDate,
                    ExpirationDate = x.c.ExpirationDate,
                    Status = x.c.Status,
                    PriceOfUnit = x.c.PriceOfUnit,
                    IsDelete = x.c.IsDelete,
                    Mass = x.c.Mass,
                    StandardMass = x.c.StandardMass,
                    MeasureName = x.mea.Name,
                    MeasureType = x.c.MeasureType,
                    Description = x.c.Description,
                    MaterialID = x.c.MaterialID,
                    MeasureID = x.c.MeasureID,
                }
            ).ToListAsync();

        // select
        var pagedResult = new PagedList<StockVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockVm>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<StockVm>>> GetAllStockExpire(Search request)
    {
        var query = from c in _context.StockMaterials
            join b in _context.Branches on c.BranchID equals b.ID
            join m in _context.Materials on c.MaterialID equals m.ID
            join mea in _context.Measures on c.MeasureID equals mea.ID
            where DateTime.Compare(c.ExpirationDate, DateTime.Now) < 0
            select new {c, b, m, mea};
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.m.Name.Contains(request.Name));
        }

        //paging
        int totalRow = await query.CountAsync();
        List<StockVm> data = new List<StockVm>();
        if (request.IsPging == true)
        {
            data = await query.Select(x =>
                    new StockVm()
                    {
                        ID = x.c.ID,
                        BranchID = x.b.ID,
                        BranchName = x.c.Branch.Name,
                        MaterialName = x.m.Name,
                        Quantity = x.c.Quantity,
                        BeginDate = x.c.BeginDate,
                        ExpirationDate = x.c.ExpirationDate,
                        Status = x.c.Status,
                        PriceOfUnit = x.c.PriceOfUnit,
                        IsDelete = x.c.IsDelete,
                        Mass = x.c.Mass,
                        StandardMass = x.c.StandardMass,
                        MeasureName = x.mea.Name,
                        MeasureType = x.c.MeasureType,
                        Description = x.c.Description,
                        MaterialID = x.c.MaterialID,
                        MeasureID = x.c.MeasureID,
                    }
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
            data = await query.Select(x =>
                new StockVm()
                {
                    ID = x.c.ID,
                    BranchID = x.b.ID,
                    BranchName = x.c.Branch.Name,
                    MaterialName = x.m.Name,
                    Quantity = x.c.Quantity,
                    BeginDate = x.c.BeginDate,
                    ExpirationDate = x.c.ExpirationDate,
                    Status = x.c.Status,
                    PriceOfUnit = x.c.PriceOfUnit,
                    IsDelete = x.c.IsDelete,
                    Mass = x.c.Mass,
                    StandardMass = x.c.StandardMass,
                    MeasureName = x.mea.Name,
                    MeasureType = x.c.MeasureType,
                    Description = x.c.Description,
                    MaterialID = x.c.MaterialID,
                    MeasureID = x.c.MeasureID,
                }
            ).ToListAsync();

        // select
        var pagedResult = new PagedList<StockVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockVm>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<StockVm>>> GetAllStock(Search request)
    {
        var query = from c in _context.StockMaterials
            join b in _context.Branches on c.BranchID equals b.ID
            join m in _context.Materials on c.MaterialID equals m.ID
            join mea in _context.Measures on c.MeasureID equals mea.ID
            where DateTime.Compare(c.ExpirationDate, DateTime.Now) >= 0
            select new {c, b, m, mea};
        if (request.StartDate != null)
            query = query.Where(x => DateTime.Compare(x.c.BeginDate, (DateTime) request.StartDate) < 0);

        //paging
        int totalRow = await query.CountAsync();
        List<StockVm> data = new List<StockVm>();
        if (request.IsPging == true)
        {
            data = await query.Select(x =>
                    new StockVm()
                    {
                        ID = x.c.ID,
                        BranchID = x.b.ID,
                        BranchName = x.c.Branch.Name,
                        MaterialName = x.m.Name,
                        Quantity = x.c.Quantity,
                        BeginDate = x.c.BeginDate,
                        ExpirationDate = x.c.ExpirationDate,
                        Status = x.c.Status,
                        PriceOfUnit = x.c.PriceOfUnit,
                        IsDelete = x.c.IsDelete,
                        Mass = x.c.Mass,
                        StandardMass = x.c.StandardMass,
                        MeasureName = x.mea.Name,
                        MeasureType = x.c.MeasureType,
                        Description = x.c.Description,
                        MaterialID = x.c.MaterialID,
                        MeasureID = x.c.MeasureID,
                    }
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
            data = await query.Select(x =>
                new StockVm()
                {
                    BranchID = x.b.ID,
                    BranchName = x.c.Branch.Name,
                    MaterialName = x.m.Name,
                    Quantity = x.c.Quantity,
                    BeginDate = x.c.BeginDate,
                    ExpirationDate = x.c.ExpirationDate,
                    Status = x.c.Status,
                    PriceOfUnit = x.c.PriceOfUnit,
                    IsDelete = x.c.IsDelete,
                    Mass = x.c.Mass,
                    StandardMass = x.c.StandardMass,
                    MeasureName = x.mea.Name,
                    MeasureType = x.c.MeasureType,
                    Description = x.c.Description,
                    MaterialID = x.c.MaterialID,
                    MeasureID = x.c.MeasureID,
                }
            ).ToListAsync();

        // select
        var pagedResult = new PagedList<StockVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockVm>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<MessageResult> CreateStockMaterial(StockRequest request)
    {
        var branch = await _context.Branches.FindAsync(request.BranchID);
        if (branch == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy chi nhánh",
            };
        }

        var material = await _context.Materials.FindAsync(request.MaterialID);
        if (material == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy nguyên liệu",
            };
        }

        var measure = await _context.Measures.FindAsync(request.MeasureID);
        if (measure == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy đơn vị tính",
            };
        }

        var stockItem = _mapper.Map<StockMaterial>(request);
        stockItem.ID = Guid.NewGuid().ToString();
        stockItem.IsDelete = false;
        stockItem.Status = 1;
        stockItem.StandardMass = measure.ConversionFactor * request.Mass;
        await _context.StockMaterials.AddAsync(stockItem);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> UpdateStockMaterial(string Id, StockRequest request)
    {
        var stock = await _context.StockMaterials.FindAsync(Id);
        if (stock == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tồn tại dữ liệu để xoá",
            };
        }

        var measure = await _context.Measures.FindAsync(request.MeasureID);
        if (measure == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy đơn vị tính",
            };
        }

        stock = _mapper.Map<StockMaterial>(request);
        stock.IsDelete = false;
        stock.Status = 1;
        stock.StandardMass = measure.ConversionFactor * request.Mass;
        _context.StockMaterials.Update(stock);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> DeleteStockMaterial(string branchID, string materialID)
    {
        var stock = await _context.StockMaterials.FirstOrDefaultAsync(x =>
            x.BranchID == branchID && x.MaterialID == materialID);
        if (stock == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tồn tại dữ liệu để xoá",
            };
        }

        _context.StockMaterials.Remove(stock);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), Upload + "/" + fileName);
        return fileName;
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}