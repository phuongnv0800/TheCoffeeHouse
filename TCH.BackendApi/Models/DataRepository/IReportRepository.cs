using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface IReportRepository
{
    Task<MessageResult> CreateImportReport(ImportReport request);
    Task<MessageResult> UpdateImportReport(string id, ImportReport request);
    Task<MessageResult> DeleteImportReport(string id);
    Task<Respond<ImportReport>> GetImportReportByID(string id);
    Task<Respond<PagedList<ImportReport>>> GetAllImportReport(Search request);

    Task<MessageResult> CreateExportReport(ExportReport request);
    Task<MessageResult> UpdateExportReport(string id, ExportReport request);
    Task<MessageResult> DeleteExportReport(string id);
    Task<Respond<ExportReport>> GetExportReportByID(string id);
    Task<Respond<PagedList<ExportReport>>> GetAllExportReport(Search request);

    Task<MessageResult> CreateLiquidationReport(LiquidationReport request);
    Task<MessageResult> UpdateLiquidationReport(string id, ImportReport request);
    Task<MessageResult> DeleteLiquidationReport(string id);
    Task<Respond<LiquidationReport>> GetLiquidationReportByID(string id);
    Task<Respond<PagedList<LiquidationReport>>> GetAllLiquidationReport(Search request);
}
