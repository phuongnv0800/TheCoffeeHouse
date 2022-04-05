using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface ICustomerRepository
{
    Task<MessageResult> Create(CustomerRequest request);
    Task<MessageResult> Delete(string id);
    Task<Respond<Customer>> GetByID(string id);
    Task<MessageResult> Update(string id, CustomerRequest request);
    Task<Respond<PagedList<Customer>>> GetAll(Search request);
    Task<MessageResult> CreateMemberType(MemberTypeRequest request);
    Task<MessageResult> DeleteMemberType(string id);
    Task<Respond<MemberType>> GetMemberTypeByID(string id);
    Task<MessageResult> UpdateMemberType(string id, MemberTypeRequest request);
    Task<Respond<PagedList<MemberType>>> GetAllMemberType(Search request);

}