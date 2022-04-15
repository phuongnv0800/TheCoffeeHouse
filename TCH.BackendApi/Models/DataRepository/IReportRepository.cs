using TCH.BackendApi.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface IReportRepository
{
    Task<MessageResult> CreateImportReport(Report request);
    Task<MessageResult> UpdateImportReport(string id, Report request);
    Task<MessageResult> DeleteImportReport(string id);
    Task<Respond<Report>> GetImportReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllImportReport(Search request);

    Task<MessageResult> CreateExportReport(Report request);
    Task<MessageResult> UpdateExportReport(string id, Report request);
    Task<MessageResult> DeleteExportReport(string id);
    Task<Respond<Report>> GetExportReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllExportReport(Search request);

    Task<MessageResult> CreateLiquidationReport(Report request);
    Task<MessageResult> UpdateLiquidationReport(string id, Report request);
    Task<MessageResult> DeleteLiquidationReport(string id);
    Task<Respond<Report>> GetLiquidationReportByID(string id);
    Task<Respond<PagedList<Report>>> GetAllLiquidationReport(Search request);
}
