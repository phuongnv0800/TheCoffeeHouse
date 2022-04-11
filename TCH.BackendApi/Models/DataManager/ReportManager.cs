﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Enum;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;

namespace TCH.BackendApi.Models.DataManager;

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
    public async Task<MessageResult> CreateExportReport(Report request)
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
            Reason =request.Reason,
            ReportType = ReportType.Export,
            Depreciation = request.Depreciation,
            RecoveryValue = request.RecoveryValue,
            Conclude = request.Conclude,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            BranchID = request.BranchID,
            UserCreateID = _userId,
        };
        _context.Reports.Add(export);
        foreach (var item in request.ReportDetails)
        {
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
            _context.ReportDetails.Add(exportMaterial);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateImportReport(Report request)
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
            Reason =request.Reason,
            ReportType = ReportType.Import,
            Depreciation = request.Depreciation,
            RecoveryValue = request.RecoveryValue,
            Conclude = request.Conclude,
            LiquidationName = request.LiquidationName,
            LiquidationRole = request.LiquidationRole,
            BranchID = request.BranchID,
            UserCreateID = _userId,
        };
        _context.Reports.Add(report);
        foreach (var item in request.ReportDetails)
        {
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

    public async Task<MessageResult> CreateLiquidationReport(Report request)
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
            Reason =request.Reason,
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
        foreach (var item in request.ReportDetails)
        {
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
        var report = await _context.Reports.FirstOrDefaultAsync(x=>x.ID==id && x.ReportType==ReportType.Export);
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
        var report = await _context.Reports.FirstOrDefaultAsync(x=>x.ID==id && x.ReportType==ReportType.Import);
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
        var report = await _context.Reports.FirstOrDefaultAsync(x=>x.ID==id && x.ReportType==ReportType.Liquidation);
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

    public async Task<Respond<PagedList<Report>>> GetAllExportReport(Search request)
    {
        var query =await _context.Reports
            .Include(x=>x.ReportDetails)
            .Where(x=>request.Name.Contains(x.Code) && x.ReportType == ReportType.Export)
            .ToListAsync();
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
        var query =await _context.Reports
            .Include(x=>x.ReportDetails)
            .Where(x=>request.Name.Contains(x.Code) && x.ReportType == ReportType.Import)
            .ToListAsync();
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
        var query =await _context.Reports
            .Include(x=>x.ReportDetails)
            .Where(x=>request.Name.Contains(x.Code) && x.ReportType == ReportType.Liquidation)
            .ToListAsync();
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

    public Task<MessageResult> UpdateImportReport(string id, Report request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> UpdateLiquidationReport(string id, Report request)
    {
        throw new NotImplementedException();
    }
}
