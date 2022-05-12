using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.SubModels;
using TCH.Utilities.Searchs;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using AutoMapper;

namespace TCH.BackendApi.Repositories.DataRepository;

public class CategoryManager : ICategoryRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IMapper _mapper;

    public CategoryManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _context = context;
        _mapper = mapper;
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
        var data = new List<CategoryVm>();
        if (request.IsPging)
        {
            data = await query
                .Select(x => _mapper.Map<CategoryVm>(x))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
        {
            data = await query.Select(x => _mapper.Map<CategoryVm>(x)).ToListAsync();
        }
        return new Respond<PagedList<CategoryVm>>()
        {
            Result = 1,
            Message = "Thành công",
            Data = new PagedList<CategoryVm>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            },
        };
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
        category.Name = request.Name ?? "";
        category.Description = request.Description ?? "";
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
