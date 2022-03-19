using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.ViewModel.Catalog;
using System.Collections.Generic;
using System.Linq;// su dung ham john
using System.Threading.Tasks;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.Paginations;

namespace TCH.BackendApi.Service.Catalog
{
    public class CategoryManager : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Respond<PagedList<CategoryVm>>> GetAll(Search request)
        {
            var query = from c in _context.Categories select c;
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            //paging
            int totalRow = await query.CountAsync();
            if (request.IsPging == true)
            {
                var data = await query
                .Select(
                    x => new CategoryVm()
                    {
                        Id = x.ID,
                        Name = x.Name,
                    }
                )
                //.OrderBy(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
                // select
                var pagedResult = new PagedList<CategoryVm>()
                {
                    TotalRecord = totalRow,
                    PageSize = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                    Items = data,
                };
                return new Respond<PagedList<CategoryVm>>() { 
                    Data =pagedResult,
                    Result = 1,
                    Message = "Thành công",
                };
            }
            else
            {
                var data = await query
                .Select(
                    x => new CategoryVm()
                    {
                        Id = x.ID,
                        Name = x.Name,
                    }
                )
                //.OrderBy(x => x.Id)
                .ToListAsync();
                // select
                var pagedResult = new PagedList<CategoryVm>()
                {
                    TotalRecord = totalRow,
                    PageSize = totalRow,
                    CurrentPage = 1,
                    TotalPages = 1,
                    Items = data,
                };
                return new Respond<PagedList<CategoryVm>>()
                {
                    Data = pagedResult,
                    Result = 1,
                    Message = "Thành công",
                };
            }
        }

        public async Task<MessageResult> Create(string name)
        {
            var category = new Category()
            {
                Name = name,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Result = 1,
                Message = "Tạo danh mục thành công",
            };
        }
        public async Task<MessageResult> Update(string id, string name)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new MessageResult()
                {
                    Result = 1,
                    Message = "Không tìm thấy danh mục",
                };
            }
            category.Name = name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Result = 1,
                Message = "Cập nhật danh mục thành công",
            };
        }

        public async Task<MessageResult> Delete(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return new MessageResult()
                {
                    Result = 1,
                    Message = "Không tìm thấy danh mục",
                };
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Result = 1,
                Message = "Xoá danh mục thành công",
            };
        }
    }
}
