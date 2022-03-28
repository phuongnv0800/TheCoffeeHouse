using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;

namespace TCH.BackendApi.Models.DataManager;

public class ReportManager : IReportRepository
{
    private readonly APIContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;

    public ReportManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
    }
    public async Task<MessageResult> CreateExportReport(ExportReport request)
    {
        var export = new ExportReport()
        {
            ID = Guid.NewGuid().ToString(),
            UserCreateID = UserID,
            CreateDate = DateTime.Now,
            Supplier = request.Supplier,
            StockName = request.StockName,
            Address = request.Address,
            TotalAmount = request.TotalAmount,
            Code = request.Code,
            Description = request.Description
        };
        _context.ExportReports.Add(export);
        foreach (var item in request.ExportMaterials)
        {
            var exportMaterial = new ExportMaterial()
            {
                ExportID = export.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                Expriydate = item.Expriydate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Status = item.Status,
            };
            _context.ExportMaterials.Add(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateImportReport(ImportReport request)
    {
        var import = new ImportReport()
        {
            ID = Guid.NewGuid().ToString(),
            UserCreateID = UserID,
            CreateDate = DateTime.Now,
            Supplier = request.Supplier,
            StockName = request.StockName,
            Address = request.Address,
            TotalAmount = request.TotalAmount,
            Code = request.Code,
            Description = request.Description
        };
        _context.ImportReports.Add(import);
        foreach (var item in request.ImportMaterials)
        {
            var exportMaterial = new ImportMaterial()
            {
                ImportID = import.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                Expriydate = item.Expriydate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Status = item.Status,
            };
            _context.ImportMaterials.Add(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateLiquidationReport(LiquidationReport request)
    {
        var import = new LiquidationReport()
        {
            ID = Guid.NewGuid().ToString(),
            Name = request.Name,
            CreateDate = DateTime.Now,
            Reason = request.Reason,
            ExpiryDate = request.ExpiryDate,
            BeginDate = request.BeginDate,
            Depreciation = request.Depreciation,
            LiquidationCost = request.LiquidationCost,
            Conclude = request.Conclude,
            RecoveryValue = request.RecoveryValue,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            Description = request.Description,
        };
        _context.LiquidationReports.Add(import);
        foreach (var item in request.LiquidationMaterials)
        {
            var exportMaterial = new LiquidationMaterial()
            {
                LiquidationID = import.ID,
                MaterialID = item.MaterialID,
                BeginDate = item.BeginDate,
                Expriydate = item.Expriydate,
                PriceOfUnit = item.PriceOfUnit,
                Quantity = item.Quantity,
                Status = item.Status,
            };
            _context.LiquidationMaterials.Add(exportMaterial);
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
        var exportReports = await _context.ExportReports.FindAsync(id);
        if (exportReports == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var exportMaterials = await _context.ExportMaterials.Where(x => x.ExportID == id).ToListAsync();
        _context.ExportMaterials.RemoveRange(exportMaterials);
        _context.ExportReports.Remove(exportReports);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> DeleteImportReport(string id)
    {
        var report = await _context.ImportReports.FindAsync(id);
        if (report == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var materials = await _context.ImportMaterials.Where(x => x.ImportID == id).ToListAsync();
        _context.ImportMaterials.RemoveRange(materials);
        _context.ImportReports.Remove(report);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> DeleteLiquidationReport(string id)
    {
        var report = await _context.LiquidationReports.FindAsync(id);
        if (report == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        var materials = await _context.LiquidationMaterials.Where(x => x.LiquidationID == id).ToListAsync();
        _context.LiquidationMaterials.RemoveRange(materials);
        _context.LiquidationReports.Remove(report);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<Respond<PagedList<ExportReport>>> GetAllExportReport(Search request)
    {
        var query = from c in _context.ExportReports select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Code.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<ExportReport>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<ExportReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(x => x)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<ExportReport>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<ExportReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }

    public async Task<Respond<PagedList<ImportReport>>> GetAllImportReport(Search request)
    {
        var query = from c in _context.ImportReports select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Code.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<ImportReport>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<ImportReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(x => x)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<ImportReport>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<ImportReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }

    public async Task<Respond<PagedList<LiquidationReport>>> GetAllLiquidationReport(Search request)
    {
        var query = from c in _context.LiquidationReports select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<LiquidationReport>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<LiquidationReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(x => x)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<LiquidationReport>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<LiquidationReport>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }

    public async Task<Respond<ExportReport>> GetExportReportByID(string id)
    {
        var result = await _context.ExportReports.FirstOrDefaultAsync(x => x.ID == id);
        if (result == null)
            return new Respond<ExportReport>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<ExportReport>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public async Task<Respond<ImportReport>> GetImportReportByID(string id)
    {
        var result = await _context.ImportReports.FirstOrDefaultAsync(x => x.ID == id);
        if (result == null)
            return new Respond<ImportReport>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<ImportReport>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public async Task<Respond<LiquidationReport>> GetLiquidationReportByID(string id)
    {
        var result = await _context.LiquidationReports.FirstOrDefaultAsync(x => x.ID == id);
        if (result == null)
            return new Respond<LiquidationReport>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<LiquidationReport>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }

    public Task<MessageResult> UpdateExportReport(string id, ExportReport request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> UpdateImportReport(string id, ImportReport request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> UpdateLiquidationReport(string id, ImportReport request)
    {
        throw new NotImplementedException();
    }
}
