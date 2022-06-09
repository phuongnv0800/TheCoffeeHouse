using System.Drawing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TCH.BackendApi.EF;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Claims;
using TCH.Utilities.Enum;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class ReportManager : IReportRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IMapper _mapper;
    private readonly string? _userId;
    private readonly IStorageService _storageService;
    private const string UserContentFolderName = "users";

    public ReportManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,
        IStorageService storageService)
    {
        _context = context;
        _mapper = mapper;
        _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
        _storageService = storageService;
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
        if (stockDetails == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tim thấy thông tin kho theo chi nhánh",
            };
        }

        foreach (var item in request.ReportDetails)
        {
            var measure = await _context.Measures.FindAsync(item.MeasureID);
            if (measure == null)
            {
                return new MessageResult()
                {
                    Result = 0,
                    Message = "Không tim thấy thông tin đơn vị",
                };
            }

            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate &&
                    item.ExpirationDate == stock.ExpirationDate)
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
        if (stockDetails == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tim thấy thông tin kho theo chi nhánh",
            };
        }

        foreach (var item in request.ReportDetails)
        {
            var measure = await _context.Measures.FindAsync(item.MeasureID);
            if (measure == null)
            {
                return new MessageResult()
                {
                    Result = 0,
                    Message = "Không tim thấy thông tin đơn vị",
                };
            }

            bool isAddStock = true;
            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate &&
                    item.ExpirationDate == stock.ExpirationDate)
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
        if (stockDetails == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tim thấy thông tin kho theo chi nhánh",
            };
        }

        foreach (var item in request.ReportDetails)
        {
            var measure = await _context.Measures.FindAsync(item.MeasureID);
            if (measure == null)
            {
                return new MessageResult()
                {
                    Result = 0,
                    Message = "Không tim thấy thông tin đơn vị",
                };
            }

            foreach (var stock in stockDetails)
            {
                if (item.MaterialID == stock.MaterialID && item.BeginDate == stock.BeginDate &&
                    item.ExpirationDate == stock.ExpirationDate)
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
                IsDelete = false,
                Mass = item.Mass,
                MeasureType = item.MeasureType,
                StandardMass = measure != null ? measure.ConversionFactor * item.Mass : item.Mass,
                Description = item.Description,
                MeasureID = item.MeasureID,
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
        var report =
            await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Liquidation);
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
            .Where(x => x.BranchID == branchID && x.ReportType == ReportType.Export)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) < 0 &&
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) > 0).ToList();

        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
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
            .Where(x => x.ReportType == ReportType.Export)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) <= 0 &&
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) >= 0).ToList();

        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Report>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<string> ExcelExportReport(string branchId, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Export && x.BranchID == branchId)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) <= 0 &&
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) >= 0).ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo xuất";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["A4:I4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:I4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:I4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelLiquidationReport(string branchId, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Liquidation && x.BranchID == branchId)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query
                .Where(
                    x => DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) <= 0
                         && DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) >= 0)
                .ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo thanh lý";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["J4"].Value = "Tên người thanh lý";
            worksheet.Cells["K4"].Value = "Vai trò";
            worksheet.Cells["L4"].Value = "Giá trị khôi phục";
            worksheet.Cells["A4:L4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:L4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:L4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;
                worksheet.Cells[row, 10].Value = item.value.LiquidationName;
                worksheet.Cells[row, 11].Value = item.value.LiquidationRole;
                worksheet.Cells[row, 12].Value = item.value.RecoveryValue;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelImportReport(string branchId, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Import && x.BranchID == branchId)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) <= 0 &&
                DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) >= 0).ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo nhập";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["A4:I4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:I4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:I4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelExportAllReport(Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Export)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x => DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) <= 0
                                     && DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) >= 0)
                .ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo xuất";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["A4:I4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:I4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:I4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelLiquidationAllReport(Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Liquidation)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x => DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) <= 0
                                     && DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date) >= 0)
                .ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo thanh lý";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["J4"].Value = "Tên người thanh lý";
            worksheet.Cells["K4"].Value = "Vai trò";
            worksheet.Cells["L4"].Value = "Giá trị khoi phục";
            worksheet.Cells["A4:L4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:L4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:L4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;
                worksheet.Cells[row, 10].Value = item.value.LiquidationName;
                worksheet.Cells[row, 11].Value = item.value.LiquidationRole;
                worksheet.Cells[row, 12].Value = item.value.RecoveryValue;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelImportAllReport(Search request)
    {
        var query = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .Where(x => x.ReportType == ReportType.Import)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x => DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) <= 0
                                     && DateTime.Compare(x.CreateDate.Date, (DateTime) request.StartDate?.Date!) >= 0)
                .ToList();

        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo nhập";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã phiếu";
            worksheet.Cells["C4"].Value = "Lý do";
            worksheet.Cells["D4"].Value = "Tên chi nhánh";
            worksheet.Cells["E4"].Value = "Ngày tạo";
            worksheet.Cells["F4"].Value = "Địa chỉ";
            worksheet.Cells["G4"].Value = "Nhà cung cấp";
            worksheet.Cells["H4"].Value = "Tên kho";
            worksheet.Cells["I4"].Value = "Tổng giá trị";
            worksheet.Cells["A4:I4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:I4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:I4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.Conclude;
                worksheet.Cells[row, 4].Value = item.value.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.Address;
                worksheet.Cells[row, 7].Value = item.value.Supplier;
                worksheet.Cells[row, 8].Value = item.value.StockName;
                worksheet.Cells[row, 9].Value = item.value.TotalAmount;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelImportReportById(string id)
    {
        var report = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Material)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Measure)
            .FirstOrDefaultAsync(x => x.ReportType == ReportType.Import && x.ID == id);
        if (report == null)
        {
            return null;
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo nhập";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã báo cáo";
            worksheet.Cells["C4"].Value = "Ngày tạo báo cáo";
            worksheet.Cells["D4"].Value = "Tên kho";
            worksheet.Cells["E4"].Value = "Tên nguyên liệu";
            worksheet.Cells["F4"].Value = "Số lượng";
            worksheet.Cells["G4"].Value = "Ngày bắt đầu";
            worksheet.Cells["H4"].Value = "Ngày hết hạn";
            worksheet.Cells["I4"].Value = "Trạng thái";
            worksheet.Cells["J4"].Value = "Giá theo đơn vị";
            worksheet.Cells["K4"].Value = "Khối lượng";
            worksheet.Cells["L4"].Value = "Loại đơn vị tiêu chuẩn";
            worksheet.Cells["M4"].Value = "Khối lượng tiêu chuẩn";
            worksheet.Cells["N4"].Value = "Tên đơn vị tính";
            worksheet.Cells["O4"].Value = "Mô tả";
            worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:O4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in report.ReportDetails.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = report.Code;
                worksheet.Cells[row, 3].Value = report.CreateDate.ToShortDateString();
                worksheet.Cells[row, 4].Value = report.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.Material.Name;
                worksheet.Cells[row, 6].Value = item.value.Quantity;
                worksheet.Cells[row, 7].Value = item.value.BeginDate.ToShortDateString();
                worksheet.Cells[row, 8].Value = item.value.ExpirationDate.ToShortDateString();
                worksheet.Cells[row, 9].Value = item.value.Status == 1 ? "Sử dụng" : "";
                worksheet.Cells[row, 10].Value = item.value.PriceOfUnit;
                worksheet.Cells[row, 11].Value = item.value.Mass;
                worksheet.Cells[row, 12].Value =
                    item.value.MeasureType == MeasureType.Mass ? "Khối lượng" : "Trọng lượng";
                worksheet.Cells[row, 13].Value = item.value.StandardMass;
                worksheet.Cells[row, 14].Value = item.value.Measure.Name;
                worksheet.Cells[row, 15].Value = item.value.Description;

                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelLiquidationReportById(string id)
    {
        var report = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Material)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Measure)
            .FirstOrDefaultAsync(x => x.ReportType == ReportType.Liquidation && x.ID == id);
        if (report == null)
        {
            return null;
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo thanh lý";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã báo cáo";
            worksheet.Cells["C4"].Value = "Ngày tạo báo cáo";
            worksheet.Cells["D4"].Value = "Tên kho";
            worksheet.Cells["E4"].Value = "Tên nguyên liệu";
            worksheet.Cells["F4"].Value = "Số lượng";
            worksheet.Cells["G4"].Value = "Ngày bắt đầu";
            worksheet.Cells["H4"].Value = "Ngày hết hạn";
            worksheet.Cells["I4"].Value = "Trạng thái";
            worksheet.Cells["J4"].Value = "Giá theo đơn vị";
            worksheet.Cells["K4"].Value = "Khối lượng";
            worksheet.Cells["L4"].Value = "Loại đơn vị tiêu chuẩn";
            worksheet.Cells["M4"].Value = "Khối lượng tiêu chuẩn";
            worksheet.Cells["N4"].Value = "Tên đơn vị tính";
            worksheet.Cells["O4"].Value = "Mô tả";
            worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:O4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in report.ReportDetails.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = report.Code;
                worksheet.Cells[row, 3].Value = report.CreateDate.ToShortDateString();
                worksheet.Cells[row, 4].Value = report.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.Material.Name;
                worksheet.Cells[row, 6].Value = item.value.Quantity;
                worksheet.Cells[row, 7].Value = item.value.BeginDate.ToShortDateString();
                worksheet.Cells[row, 8].Value = item.value.ExpirationDate.ToShortDateString();
                worksheet.Cells[row, 9].Value = item.value.Status == 1 ? "Sử dụng" : "";
                worksheet.Cells[row, 10].Value = item.value.PriceOfUnit;
                worksheet.Cells[row, 11].Value = item.value.Mass;
                worksheet.Cells[row, 12].Value =
                    item.value.MeasureType == MeasureType.Mass ? "Khối lượng" : "Trọng lượng";
                worksheet.Cells[row, 13].Value = item.value.StandardMass;
                worksheet.Cells[row, 14].Value = item.value.Measure.Name;
                worksheet.Cells[row, 15].Value = item.value.Description;
                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelExportReportById(string id)
    {
        var report = await _context.Reports
            .Include(x => x.Branch)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Material)
            .Include(x => x.ReportDetails)
            .ThenInclude(x => x.Measure)
            .FirstOrDefaultAsync(x => x.ReportType == ReportType.Export && x.ID == id);
        if (report == null)
        {
            return null;
        }

        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Export");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo xuất";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "STT";
            worksheet.Cells["B4"].Value = "Mã báo cáo";
            worksheet.Cells["C4"].Value = "Ngày tạo báo cáo";
            worksheet.Cells["D4"].Value = "Tên kho";
            worksheet.Cells["E4"].Value = "Tên nguyên liệu";
            worksheet.Cells["F4"].Value = "Số lượng";
            worksheet.Cells["G4"].Value = "Ngày bắt đầu";
            worksheet.Cells["H4"].Value = "Ngày hết hạn";
            worksheet.Cells["I4"].Value = "Trạng thái";
            worksheet.Cells["J4"].Value = "Giá theo đơn vị";
            worksheet.Cells["K4"].Value = "Khối lượng";
            worksheet.Cells["L4"].Value = "Loại đơn vị tiêu chuẩn";
            worksheet.Cells["M4"].Value = "Khối lượng tiêu chuẩn";
            worksheet.Cells["N4"].Value = "Tên đơn vị tính";
            worksheet.Cells["O4"].Value = "Mô tả";
            worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A4:O4"].Style.Font.Bold = true;

            row = 5;
            foreach (var item in report.ReportDetails.Select((value, i) => new {value, i}))
            {
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = report.Code;
                worksheet.Cells[row, 3].Value = report.CreateDate.ToShortDateString();
                worksheet.Cells[row, 4].Value = report.Branch.Name;
                worksheet.Cells[row, 5].Value = item.value.Material.Name;
                worksheet.Cells[row, 6].Value = item.value.Quantity;
                worksheet.Cells[row, 7].Value = item.value.BeginDate.ToShortDateString();
                worksheet.Cells[row, 8].Value = item.value.ExpirationDate.ToShortDateString();
                worksheet.Cells[row, 9].Value = item.value.Status == 1 ? "Sử dụng" : "";
                worksheet.Cells[row, 10].Value = item.value.PriceOfUnit;
                worksheet.Cells[row, 11].Value = item.value.Mass;
                worksheet.Cells[row, 12].Value =
                    item.value.MeasureType == MeasureType.Mass ? "Khối lượng" : "Trọng lượng";
                worksheet.Cells[row, 13].Value = item.value.StandardMass;
                worksheet.Cells[row, 14].Value = item.value.Measure.Name;
                worksheet.Cells[row, 15].Value = item.value.Description;
                row++;
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<Respond<PagedList<Report>>> GetAllImportReportByBranchID(string branchID, Search request)
    {
        var query = await _context.Reports
            .Include(x => x.ReportDetails)
            .Where(x => x.BranchID == branchID && x.ReportType == ReportType.Import)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
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
            .Where(x => x.ReportType == ReportType.Import)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
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
            .Where(x => x.BranchID == branchID && x.ReportType == ReportType.Liquidation)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
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
            .Where(x => x.ReportType == ReportType.Liquidation)
            .ToListAsync();
        if (request.Name != null)
        {
            query = query.Where(x => request.Name.Contains(x.Code)).ToList();
        }

        if (request.StartDate != null && request.EndDate != null)
            query = query.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        //paging
        int totalRow = query.Count;
        var data = new List<Report>();
        if (request.IsPging)
            data = query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        else
        {
            data = query.ToList();
        }

        var pagedResult = new PagedList<Report>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
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
        var result =
            await _context.Reports.FirstOrDefaultAsync(x => x.ID == id && x.ReportType == ReportType.Liquidation);
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

    public async Task<Respond<PagedList<MassMaterial>>> GetMassMaterialInDay(Search request)
    {
        var result = await _context
            .Orders
            .Include(x => x.OrderDetails)
            .ToListAsync();
        if (result == null || result.Count == 0)
        {
            return new Respond<PagedList<MassMaterial>>
            {
                Data = new(),
                Result = 1,
                Message = "Thành công",
            };
        }

        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name)).ToList();
        if (request.StartDate != null && request.EndDate != null)
            result = result.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        var orderDetails = new List<OrderDetail>();
        foreach (var order in result)
        {
            if (order.OrderDetails.Count > 0 && order.OrderDetails != null)
            {
                orderDetails.AddRange(order.OrderDetails);
            }
        }

        var data = new List<MassMaterial>();
        foreach (var item in orderDetails)
        {
            var recipes = await _context
                .RecipeDetails
                .Include(x => x.Material)
                .Where(x => x.ProductID == item.ProductID && x.SizeID == item.SizeID)
                .ToListAsync();
            if (recipes != null)
            {
                foreach (var recipe in recipes)
                {
                    bool add = true;
                    foreach (var i in data)
                    {
                        if (i.MaterialID == recipe.MaterialID)
                        {
                            add = false;
                            i.StandardMass += item.Quantity * recipe.Weight;
                        }
                    }

                    if (add)
                    {
                        data.Add(new MassMaterial()
                        {
                            MaterialID = recipe.MaterialID,
                            Name = recipe.Material.Name,
                            Description = item.Description,
                            StandardUnitType = recipe.StandardUnitType,
                            StandardMass = recipe.Weight * item.Quantity,
                            DateStart = request.StartDate,
                            DateEnd = request.EndDate,
                        });
                    }
                }
            }
        }

        //paging
        int totalRow = data.Count();
        if (request.IsPging)
        {
            data = data.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = data.ToList();

        var pagedResult = new PagedList<MassMaterial>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<MassMaterial>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<MassMaterial>>> GetMassMaterialInDayByBranchId(string branchId, Search request)
    {
        var result = await _context
            .Orders
            .Include(x => x.OrderDetails)
            .Where(x => x.BranchID == branchId)
            .ToListAsync();
        if (result == null || result.Count == 0)
        {
            return new Respond<PagedList<MassMaterial>>
            {
                Data = new(),
                Result = 1,
                Message = "Thành công",
            };
        }

        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name)).ToList();
        if (request.StartDate != null && request.EndDate != null)
            result = result.Where(x =>
                x.CreateDate.Date <= request.EndDate?.Date && x.CreateDate.Date >= request.StartDate?.Date).ToList();
        var orderDetails = new List<OrderDetail>();
        foreach (var order in result)
        {
            if (order.OrderDetails.Count > 0 && order.OrderDetails != null)
            {
                orderDetails.AddRange(order.OrderDetails);
            }
        }

        var data = new List<MassMaterial>();
        foreach (var item in orderDetails)
        {
            var recipes = await _context
                .RecipeDetails
                .Include(x => x.Material)
                .Where(x => x.ProductID == item.ProductID && x.SizeID == item.SizeID)
                .ToListAsync();
            if (recipes != null)
            {
                foreach (var recipe in recipes)
                {
                    bool add = true;
                    foreach (var i in data)
                    {
                        if (i.MaterialID == recipe.MaterialID)
                        {
                            add = false;
                            i.StandardMass += item.Quantity * recipe.Weight;
                        }
                    }

                    if (add)
                    {
                        data.Add(new MassMaterial()
                        {
                            MaterialID = recipe.MaterialID,
                            Name = recipe.Material.Name,
                            Description = item.Description,
                            StandardUnitType = recipe.StandardUnitType,
                            StandardMass = recipe.Weight * item.Quantity,
                            DateStart = request.StartDate,
                            DateEnd = request.EndDate,
                        });
                    }
                }
            }
        }

        //paging
        int totalRow = data.Count();
        if (request.IsPging)
        {
            data = data.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = data.ToList();

        var pagedResult = new PagedList<MassMaterial>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<MassMaterial>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), UserContentFolderName + "/" + fileName);
        return fileName;
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}