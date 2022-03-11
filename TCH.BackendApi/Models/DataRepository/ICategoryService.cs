using System.Collections.Generic;
using System.Threading.Tasks;
using TCH.ViewModel.Catalog;

namespace TCH.BackendApi.Service.Catalog
{
    public interface ICategoryService
    {
        Task<int> Create(string name);
        Task<int> Delete(int id);
        Task<IEnumerable<CategoryVm>> GetAll();
        Task<int> Update(int id, string name);
    }
}