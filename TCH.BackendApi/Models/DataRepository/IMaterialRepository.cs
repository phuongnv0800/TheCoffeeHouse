using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

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
