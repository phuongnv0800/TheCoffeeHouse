using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Claims;
using TCH.Utilities.Enum;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class ReportManager : IReportRepository
{
    private readonly APIContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? _userId;

    public ReportManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
    }
    public async Task<MessageResult> CreateExportReport(ExportRequest request)
    {
        var export = new Report()
        {
            ID = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now,
            Supplier = request.Supplier,
            StockName = request.StockName,
            Address = request.Address,
            TotalAmount = request.TotalAmount,
            Code = request.Code,
            Name = request.Name,
            Reason = request.Reason,
            ReportType = ReportType.Export,
            Depreciation = request.Depreciation,
            RecoveryValue = request.RecoveryValue,
            Conclude = request.Conclude,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            BranchID = request.BranchID,
            UserCreateID = _userId,
        };
        await _context.Reports.AddAsync(export);
        var stockDetails = await _context.StockMaterials.Where(x => x.BranchID == request.BranchID).ToListAsync();
        foreach (var item in request.ReportDetails)
        {
            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate && item.ExpirationDate == stock.ExpirationDate)
                {
                    stock.Quantity -= item.Quantity;
                    if (stock.Quantity < 0)
                    {
                        stock.Quantity = 0;
                    }
                    break;
                }
            }
            var exportMaterial = new ReportDetail()
            {
                ReportID = export.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                ExpirationDate = item.ExpirationDate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Unit = item.Unit,
                Status = item.Status,
            };
            await _context.ReportDetails.AddAsync(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateImportReport(ImportRequest request)
    {
        var report = new Report()
        {
            ID = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now,
            Supplier = request.Supplier,
            StockName = request.StockName,
            Address = request.Address,
            TotalAmount = request.TotalAmount,
            Code = request.Code,
            Name = request.Name,
            Reason = request.Reason,
            ReportType = ReportType.Import,
            Depreciation = request.Depreciation,
            RecoveryValue = request.RecoveryValue,
            Conclude = request.Conclude,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            BranchID = request.BranchID,
            UserCreateID = _userId,
        };
        await _context.Reports.AddAsync(report);
        var stockDetails = await _context.StockMaterials.Where(x => x.BranchID == request.BranchID).ToListAsync();

        foreach (var item in request.ReportDetails)
        {
            var measure = await _context.Measures.FindAsync(item.MeasureID);
            bool isAddStock = true;
            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate && item.ExpirationDate == stock.ExpirationDate)
                {
                    isAddStock = false;
                    stock.Quantity += item.Quantity;
                    break;
                }
            }
            if (isAddStock)
            {
                var stockMaterial = new StockMaterial()
                {
                    BranchID = request.BranchID,
                    MaterialID = item.MaterialID,
                    BeginDate = item.BeginDate,
                    ExpirationDate = item.ExpirationDate,
                    PriceOfUnit = item.PriceOfUnit,
                    Quantity = item.Quantity,
                    Status = item.Status,
                    IsDelete = false,
                    Mass = item.Mass,
                    MeasureType = item.MeasureType,
                    StandardMass = measure != null ? measure.ConversionFactor * item.Mass : item.Mass,
                    Description = item.Description,
                    MeasureID = item.MeasureID,
                };
                await _context.StockMaterials.AddAsync(stockMaterial);
            }
            var exportMaterial = new ReportDetail()
            {
                ReportID = report.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                ExpirationDate = item.ExpirationDate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Unit = item.Unit,
                Status = item.Status,
                IsDelete = false,
                Mass = item.Mass,
                MeasureType = item.MeasureType,
                StandardMass = measure != null ? measure.ConversionFactor * item.Mass : item.Mass,
                Description = item.Description,
                MeasureID = item.MeasureID,
            };
            await _context.ReportDetails.AddAsync(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateLiquidationReport(ImportRequest request)
    {
        var report = new Report()
        {
            ID = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now,
            Supplier = request.Supplier,
            StockName = request.StockName,
            Address = request.Address,
            TotalAmount = request.TotalAmount,
            Code = request.Code,
            Name = request.Name,
            Reason = request.Reason,
            ReportType = ReportType.Liquidation,
            Depreciation = request.Depreciation,
            RecoveryValue = request.RecoveryValue,
            Conclude = request.Conclude,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            BranchID = request.BranchID,
            UserCreateID = _userId,
        };
        _context.Reports.Add(report);

        var stockDetails = await _context.StockMaterials.Where(x => x.BranchID == request.BranchID).ToListAsync();
        foreach (var item in request.ReportDetails)
        {
            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate && item.ExpirationDate == stock.ExpirationDate)
                {
                    stock.Quantity -= item.Quantity;
                    if (stock.Quantity < 0)
                    {
                        stock.Quantity = 0;
                    }
                    break;
                }
            }
            var exportMaterial = new ReportDetail()
            {
                ReportID = report.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                ExpirationDate = item.ExpirationDate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Unit = item.Unit,
                Status = item.Status,
            };
            _context.ReportDetails.Add(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> DeleteExportReport(string id)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Export);
        if (report == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var details = await _context.ReportDetails.Where(x => x.ReportID == id).ToListAsync();
        _context.ReportDetails.RemoveRange(details);
        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> DeleteImportReport(string id)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Import);
        if (report == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var details = await _context.ReportDetails.Where(x => x.ReportID == id).ToListAsync();
        _context.ReportDetails.RemoveRange(details);
        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> DeleteLiquidationReport(string id)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Liquidation);
        if (report == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var details = await _context.ReportDetails.Where(x => x.ReportID == id).ToListAsync();
        _context.ReportDetails.RemoveRange(details);
        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<Respond<PagedList<Report>>> GetAllExportReportByBranchID(string branchID, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.ReportDetails)
            .Where(x => x.BranchID == branchID)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Export).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Report>>> GetAllExportReport(Search request)
    {
        var query = await _context.Reports
            .Include(x => x.ReportDetails)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Export).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }


    public async Task<Respond<PagedList<Report>>> GetAllImportReportByBranchID(string branchID, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.ReportDetails)
            .Where(x => x.BranchID == branchID)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Import).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Report>>> GetAllImportReport(Search request)
    {
        var query = await _context.Reports
            .Include(x => x.ReportDetails)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Import).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Report>>> GetAllLiquidationReportByBranchID(string branchID, Search request)
    {
        var query = await _context.Reports
             .Include(x => x.ReportDetails)
            .Where(x => x.BranchID == branchID)
             .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Liquidation).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Report>>> GetAllLiquidationReport(Search request)
    {
        var query = await _context.Reports
             .Include(x => x.ReportDetails)
             .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code) && x.ReportType == ReportType.Liquidation).ToList();
        }
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<Report>> GetExportReportByID(string id)
    {
        var result = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Export);
        if (result == null)
            return new Respond<Report>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<Report>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public async Task<Respond<Report>> GetImportReportByID(string id)
    {
        var result = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Import);
        if (result == null)
            return new Respond<Report>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<Report>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public async Task<Respond<Report>> GetLiquidationReportByID(string id)
    {
        var result = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Liquidation);
        if (result == null)
            return new Respond<Report>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<Report>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public Task<MessageResult> UpdateExportReport(string id, Report request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> UpdateImportReport(string id, ImportRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> UpdateLiquidationReport(string id, Report request)
    {
        throw new NotImplementedException();
    }
}
