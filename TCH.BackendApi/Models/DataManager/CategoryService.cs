//using Microsoft.EntityFrameworkCore;
//using TCH.BackendApi.EF;
//using TCH.BackendApi.Entities;
//using TCH.ViewModel.Catalog;
//using System.Collections.Generic;
//using System.Linq;// su dung ham john
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Service.Catalog
//{
//    public class CategoryService : ICategoryService
//    {
//        private readonly ApplicationDbContext _context;

//        public CategoryService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<CategoryVm>> GetAll()
//        {
//            var query = from c in _context.Categories select new { c };
//            return await query.Select(x => new CategoryVm()
//            {
//                Id = x.c.ID,
//                Name = x.c.Name
//            }).ToListAsync();
//        }

//        public async Task<int> Create(string name)
//        {
//            var category = new Category()
//            {
//                Name = name,
//            };
//            _context.Categories.Add(category);

//            return await _context.SaveChangesAsync();
//        }
//        public async Task<int> Update(int id, string name)
//        {
//            var category = await _context.Categories.FindAsync(id);

//            category.Name = name;
//            _context.Categories.Update(category);
//            return await _context.SaveChangesAsync();
//        }

//        public async Task<int> Delete(int id)
//        {
//            var category = await _context.Categories.FindAsync(id);
//            _context.Categories.Remove(category);
//            return await _context.SaveChangesAsync();
//        }
//    }
//}
