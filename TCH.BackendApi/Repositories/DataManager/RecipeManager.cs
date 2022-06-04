using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.Utilities.Claims;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;

namespace TCH.BackendApi.Repositories.DataManager;

public class RecipeManager : IDisposable, IRecipeRepository
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;
    private const string Upload = "recipe";
    private readonly IStorageService _storageService;

    public RecipeManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext, IStorageService storageService)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _storageService = storageService;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<Respond<IEnumerable<RecipeDetail>>> GetRecipeByProductID(string productID)
    {
        var result = await _context.RecipeDetails
            .Include(x => x.Product)
            .Include(x => x.Size)
            .Include(x => x.Material)
            .Where(x => x.ProductID == productID).ToListAsync();
        if (result.Count == 0)
        {
            return new Respond<IEnumerable<RecipeDetail>>()
            {
                Data = null,
                Result = 0,
                Message = "Chưa có công thức, bạn hãy thêm công thức cho sản phẩm này",
            };
        }

        return new Respond<IEnumerable<RecipeDetail>>()
        {
            Data = result,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<Respond<IEnumerable<RecipeDetail>>> GetRecipeByProductSize(string productID, string sizeID)
    {
        var result = await _context.RecipeDetails
            .Include(x => x.Product)
            .Include(x => x.Size)
            .Include(x => x.Material)
            .Where(x => x.ProductID == productID && x.SizeID == sizeID)
            .ToListAsync();
        if (result.Count == 0)
        {
            return new Respond<IEnumerable<RecipeDetail>>()
            {
                Data = null,
                Result = 0,
                Message = "Chưa có công thức, bạn hãy thêm công thức cho sản phẩm này",
            };
        }

        return new Respond<IEnumerable<RecipeDetail>>()
        {
            Data = result,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> Create(IEnumerable<RecipeRequest> request)
    {
        var recipes = request.Select(x => _mapper.Map<RecipeDetail>(x)).ToList();

        if (recipes == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không thể thêm công thức",
            };

        }
        await _context.RecipeDetails.AddRangeAsync(recipes);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }
    public async Task<Respond<PagedList<RecipeDetail>>> GetAll(Search request)
    {
        var query = _context.RecipeDetails
             .Include(x => x.Size)
             .Include(x => x.Material)
             .Include(x => x.Product);
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<RecipeDetail, Product>)query.Where(x => x.Product.Name.Contains(request.Name));
        }
        int totalRow = await query.CountAsync();
        //paging

        List<RecipeDetail> data = new List<RecipeDetail>();
        if (request.IsPging == true)
        {
            data = await query
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderByDescending(x => x.ProductID)
           .ToListAsync();
        }
        else
            data = await query.OrderByDescending(x => x.ProductID).ToListAsync();
        if (data.Count == 0)
        {
            return new Respond<PagedList<RecipeDetail>>()
            {
                Data = null,
                Result = 0,
                Message = "Chưa có công thức, bạn hãy thêm công thức cho sản phẩm",
            };
        }
        // select
        var pagedResult = new PagedList<RecipeDetail>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<RecipeDetail>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<MessageResult> Update(RecipeRequest request)
    {
        var entity = await _context.RecipeDetails.FindAsync(request.ProductID, request.SizeID, request.MaterialID);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 1,
                Message = "Không tìm thấy",
            };
        }
        entity.StandardUnitType = request.StandardUnitType;
        entity.Unit = request.Unit;
        entity.Weight = request.Weight;
        _context.RecipeDetails.Update(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> Delete(string productId)
    {
        var entity = await _context.RecipeDetails.Where(x => x.ProductID == productId).ToListAsync();
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.RecipeDetails.RemoveRange(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }
    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), Upload + "/" + fileName);
        return fileName;
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
