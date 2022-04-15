using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.ViewModel.SubModels;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.Utilities.Claims;

namespace TCH.BackendApi.Models.DataManager;

public class PromotionManager : IDisposable
{
    private readonly APIContext _context;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? _userID;
    private const string Upload = "promotions";
    public PromotionManager(APIContext context,
        IHttpContextAccessor httpContextAccessor,
        IStorageService storageService,
        IMapper mapper)
    {
        _context = context;
        _storageService = storageService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
        //_accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
    }
    public async Task<Respond<PagedList<Promotion>>> GetAll(Search request)
    {
        var query = await _context.Promotions.Include(x=>x.PromotionGifts).ToListAsync();
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Code.Contains(request.Name)).ToList();
        //paging
        int totalRow = query.Count;
        var data = new List<Promotion>();
        if (request.IsPging)
        {
            data = query.Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ToList();
        }
        else
            data = query.Select(x => x).ToList(); 
        var pagedResult = new PagedList<Promotion>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Promotion>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<Respond<Promotion>> GetByCode(string Code)
    {
        var result = await _context.Promotions
            .Include(x => x.PromotionGifts)
            .FirstOrDefaultAsync(x=>x.Code.Contains(Code) || x.ID.Contains(Code));
        if (result == null)
            new Respond<PagedList<Promotion>>()
            {
                Data = null,
                Result = 1,
                Message = "Không tồn tại",
            };

        return new Respond<Promotion>()
        {
            Data = result,
            Result = 1,
            Message = "Thành công",
        };
    }


    public async Task<MessageResult> Create(PromotionRequest request)
    {
        var product = _mapper.Map<Product>(request);
        product.ID = Guid.NewGuid().ToString();
        product.CreateDate = DateTime.Now;
        product.UpdateDate = DateTime.Now;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Taọ sản phẩm thành công",
            Result = 1,
        };
    }

    public async Task<MessageResult> Delete(string productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return new MessageResult()
            {
                Message = "Sản phẩm không tồn tại",
                Result = -1,
            };
        var productImages = await _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
        foreach (var item in productImages)
        {
            await _storageService.DeleteFileAsync(item.ImagePath);
            _context.ProductImages.Remove(item);
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Xoá sản phẩm thành công",
            Result = 1,
        };
    }

    public async Task<MessageResult> Update(string productID, PromotionRequest request)
    {
        var product = await _context.Products.FindAsync(productID);
        if (product == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy sản phẩm",
            };
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật sản phẩm thành công",
        };
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
