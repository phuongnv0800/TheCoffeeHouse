using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.ViewModel.SubModels;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.Utilities.Claims;
using TCH.Utilities.Enum;

namespace TCH.BackendApi.Repositories.DataManager;

public class PromotionManager : IDisposable, IPromotionRepository
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
        var query = await _context.Promotions.Include(x => x.PromotionGifts).ToListAsync();
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
    public async Task<Respond<Promotion>> GetByCode(string code)
    {
        var result = await _context.Promotions
            .Include(x => x.PromotionGifts)
            .FirstOrDefaultAsync(x => x.Code.Contains(code) || x.ID.Contains(code));
        if (result == null)
            new Respond<Promotion>()
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
    public async Task<Respond<dynamic>> GetReduceMoney(string code, List<OrderItem> orderItems)
    {
        if (string.IsNullOrEmpty(code))
        {
            return new Respond<dynamic>()
            {
                Data = null,
                Result = 0,
                Message = "Nhập code",
            };
        }
        var result = await _context.Promotions
            .Include(x => x.PromotionGifts)
            .FirstOrDefaultAsync(x => x.Code.Contains(code));
        if (result == null)
        {
            return new Respond<dynamic>()
            {
                Data = null,
                Result = 0,
                Message = "Code không tồn tại",
            };
        }
        if (!(result.StartDate <= DateTime.Now && result.EndDate >= DateTime.Now) || result.Quantity <= 0)
        {
            return new Respond<dynamic>()
            {
                Data = null,
                Result = 0,
                Message = "Code hết hạn sử dụng",
            };
        }
        double totalAmount = 0;
        foreach (var item in orderItems)
        {
            totalAmount += item.Quantity * (item.PriceProduct + item.PriceSize);
            if (item.Toppings.Count > 0)
            {
                foreach (var topping in item.Toppings)
                {
                    totalAmount += topping.SubPrice * topping.Quantity;
                }
            }
        }
        if (result.PromotionObject == PromotionObject.Invoice)
        {
            if (result.PromotionType == PromotionType.SumPercent)
            {
                var reduce = totalAmount * result.ReducePercent;
                return new Respond<dynamic>()
                {
                    Data = new { ReducePromotion = reduce },
                    Result = 1,
                    Message = "Thành công",
                };
            }
            else if (result.PromotionType == PromotionType.SumAmount)
            {
                return new Respond<dynamic>()
                {
                    Data = new { ReducePromotion = result.ReduceAmount },
                    Result = 1,
                    Message = "Thành công",
                };
            }
        }
        else if (result.PromotionObject == PromotionObject.Food)
        {
            if (result.PromotionType == PromotionType.Percent)
            {
                double reduce = 0;
                foreach (var item in result.PromotionGifts)
                {
                    foreach (var product in orderItems)
                    {
                        if (item.ProductID == product.ProductID)
                        {
                            reduce += (product.PriceProduct * item.ReducePercent) * product.Quantity;
                        }
                    }
                }
                return new Respond<dynamic>()
                {
                    Data = new { ReducePromotion = reduce },
                    Result = 1,
                    Message = "Thành công",
                };
            }
            else if (result.PromotionType == PromotionType.Amount)
            {
                double reduce = 0;
                foreach (var item in result.PromotionGifts)
                {
                    foreach (var product in orderItems)
                    {
                        if (item.ProductID == product.ProductID)
                        {
                            reduce += item.ReduceAmount * product.Quantity;
                        }
                    }
                }
                return new Respond<dynamic>()
                {
                    Data = new { ReducePromotion = reduce },
                    Result = 1,
                    Message = "Thành công",
                };
            }
        }
        return new Respond<dynamic>()
        {
            Data = null,
            Result = 0,
            Message = "Code không thể sử dụng sử dụng",
        };
    }

    public async Task<MessageResult> Create(PromotionRequest request)
    {
        var promotionDb = await _context.Promotions.FirstOrDefaultAsync(x => x.Code.Contains(request.Code));
        if (promotionDb != null)
        {
            return new MessageResult()
            {
                Message = "Code đã tồn tại, hãy nhập code khác",
                Result = 0,
            };
        }
        var promotion = _mapper.Map<Promotion>(request);
        promotion.ID = Guid.NewGuid().ToString();
        promotion.CreateDate = DateTime.Now;
        await _context.Promotions.AddAsync(promotion);
        if (request.PromotionLists != null && request.PromotionLists.Count != 0)
            foreach (var item in request.PromotionLists)
            {
                var promotionGift = new PromotionGift()
                {
                    ProductID = item.ProductID,
                    Description = item.Description,
                    PromotionID = promotion.ID,
                    ReduceAmount = item.ReduceAmount,
                    ReducePercent = item.ReducePercent,
                    IsRequired = item.IsRequired,
                };
                await _context.PromotionGifts.AddAsync(promotionGift);
            }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Taọ thành công",
            Result = 1,
        };
    }

    public async Task<MessageResult> Delete(string promotionID)
    {
        var result = await _context.Promotions
            .Include(x => x.PromotionGifts)
            .FirstOrDefaultAsync(x => x.ID == promotionID);
        if (result == null)
            return new MessageResult()
            {
                Message = "Không tồn tại hoặc đã bị xoá",
                Result = 0
            };
        _context.Promotions.Remove(result);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Xoá thành công",
            Result = 1,
        };
    }
    public async Task<MessageResult> DeleteByCode(string code)
    {
        var result = await _context.Promotions
            .Include(x => x.PromotionGifts)
            .FirstOrDefaultAsync(x => x.Code == code);
        if (result == null)
            return new MessageResult()
            {
                Message = "Không tồn tại hoặc đã bị xoá",
                Result = 0
            };
        _context.Promotions.Remove(result);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Xoá thành công",
            Result = 1,
        };
    }

    public async Task<MessageResult> Update(string promotionID, PromotionRequest request)
    {
        var result = await _context.Promotions.FindAsync(promotionID);
        if (result == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };
        _context.Promotions.Update(result);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
