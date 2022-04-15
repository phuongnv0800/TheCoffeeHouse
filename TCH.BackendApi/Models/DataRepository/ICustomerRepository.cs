using TCH.BackendApi.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

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
    Task<Respond<Bean>> GetMemberTypeByID(string id);
    Task<MessageResult> UpdateMemberType(string id, MemberTypeRequest request);
    Task<Respond<PagedList<Bean>>> GetAllMemberType(Search request);

}