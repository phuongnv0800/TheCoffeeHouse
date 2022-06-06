using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IReportRepository
{
    Task<MessageResult> CreateImportReport(ImportRequest request);
    Task<MessageResult> UpdateImportReport(string id, ImportRequest request);
    Task<MessageResult> DeleteImportReport(string id);
    Task<string> ExcelExportReport(string branchId, Search request);
    Task<string> ExcelImportReport(string branchId, Search request);
    Task<string> ExcelLiquidationReport(string branchId, Search request);
    
    Task<string> ExcelExportAllReport(Search request);
    Task<string> ExcelImportAllReport(Search request);
    Task<string> ExcelLiquidationAllReport(Search request);
    Task<string> ExcelImportReportById(string id);
    Task<string> ExcelLiquidationReportById(string id);
    Task<string> ExcelExportReportById(string id);
    Task<Respond<Report>> GetImportReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllImportReportByBranchID(string branchID, Search request);
    Task<Respond<PagedList<Report>>> GetAllImportReport(Search request);

    Task<MessageResult> CreateExportReport(ExportRequest request);
    Task<MessageResult> UpdateExportReport(string id, Report request);
    Task<MessageResult> DeleteExportReport(string id);
    Task<Respond<Report>> GetExportReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllExportReport(Search request);
    Task<Respond<PagedList<Report>>> GetAllExportReportByBranchID(string branchID, Search request);

    Task<MessageResult> CreateLiquidationReport(ImportRequest request);
    Task<MessageResult> UpdateLiquidationReport(string id, Report request);
    Task<MessageResult> DeleteLiquidationReport(string id);
    Task<Respond<Report>> GetLiquidationReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllLiquidationReport(Search request);
    Task<Respond<PagedList<Report>>> GetAllLiquidationReportByBranchID(string branchID, Search request);

    Task<Respond<PagedList<MassMaterial>>> GetMassMaterialInDay(Search request);
    Task<Respond<PagedList<MassMaterial>>> GetMassMaterialInDayByBranchId(string branchId, Search request);
}
