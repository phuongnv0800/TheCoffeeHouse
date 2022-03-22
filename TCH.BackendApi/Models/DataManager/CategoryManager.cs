using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.ViewModels;
using AutoMapper;
using System.Security.Claims;
using TCH.BackendApi.Config;

namespace TCH.BackendApi.Models.DataManager
{
    public class CategoryManager : ICategoryRepository, IDisposable
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string UserID ;
        private readonly string _accessToken;

        public CategoryManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContext;
            UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
            _accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
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
                     x => _mapper.Map<CategoryVm>(x)
                )
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                //.OrderBy(x=>x.Name)
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
                return new Respond<PagedList<CategoryVm>>()
                {
                    Data = pagedResult,
                    Result = 1,
                    Message = "Thành công",
                };
            }
            else
            {
                var data = await query
                .Select(
                    x => _mapper.Map<CategoryVm>(x)
                )
                //.OrderBy(x => x.Name)
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

        public async Task<MessageResult> Create(CategoryVm request)
        {
            var category = _mapper.Map<Category>(request);
            category.ID = Guid.NewGuid().ToString();
            category.UpdateDate = DateTime.Now;
            category.CreateDate = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Result = 1,
                Message = "Tạo danh mục thành công",
            };
        }
        public async Task<MessageResult> Update(string id, CategoryVm request)
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
            category.Name = request.Name;
            category.Description = request.Description;
            category.UpdateDate = DateTime.Now;
            
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
            if (category == null)
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
        public void Dispose()
        {
            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(this);
        }
    }
}
