// using TCH.BackendApi.Entities;
// using TCH.BackendApi.Models.Paginations;
// using TCH.BackendApi.Models.Searchs;
// using TCH.BackendApi.Models.SubModels;
//
// namespace TCH.BackendApi.Models.DataRepository;
//
// public interface IReportRepository
// {
//     Task<MessageResult> CreateImportReport(Import request);
//     Task<MessageResult> UpdateImportReport(string id, Import request);
//     Task<MessageResult> DeleteImportReport(string id);
//     Task<Respond<Import>> GetImportReportByID(string id);
//     Task<Respond<PagedList<Import>>> GetAllImportReport(Search request);
//
//     Task<MessageResult> CreateExportReport(Export request);
//     Task<MessageResult> UpdateExportReport(string id, Export request);
//     Task<MessageResult> DeleteExportReport(string id);
//     Task<Respond<Export>> GetExportReportByID(string id);
//     Task<Respond<PagedList<Export>>> GetAllExportReport(Search request);
//
//     Task<MessageResult> CreateLiquidationReport(Liquidation request);
//     Task<MessageResult> UpdateLiquidationReport(string id, Import request);
//     Task<MessageResult> DeleteLiquidationReport(string id);
//     Task<Respond<Liquidation>> GetLiquidationReportByID(string id);
//     Task<Respond<PagedList<Liquidation>>> GetAllLiquidationReport(Search request);
// }
