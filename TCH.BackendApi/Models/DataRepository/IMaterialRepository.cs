using TCH.BackendApi.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface IMaterialRepository
{
    Task<MessageResult> CreateMaterialType(MaterialTypeRequest request);
    Task<MessageResult> UpdateMaterialType(string id, MaterialTypeRequest request);
    Task<MessageResult> DeleteMaterialType(string id);
    Task<Respond<MaterialType>> GetMaterialTypeByID(string id);
    Task<Respond<PagedList<MaterialType>>> GetAllMaterialType(Search request);
    Task<MessageResult> CreateMaterial(MaterialRequest request);
    Task<MessageResult> UpdateMaterial(string id, MaterialRequest request);
    Task<MessageResult> DeleteMaterial(string id);
    Task<Respond<Material>> GetMaterialByID(string id);
    Task<Respond<PagedList<Material>>> GetAllMaterial(Search request);
}
