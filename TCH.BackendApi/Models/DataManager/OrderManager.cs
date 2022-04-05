using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Enum;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataManager;

public class OrderManager : IOrderRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;
    public OrderManager(APIContext context, IHttpContextAccessor httpContext, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<MessageResult> Create(OrderRequest request)
    {
        var branch = await _context.Branches.FindAsync(request.BranchID);
        if( branch == null)
            return new MessageResult()
            {
                Result = 0,
                Message = "Không có thông tin nhánh",
            };
        double subTotal = 0;
        foreach (var item in request.OrderItems)
        {
            //check số lượng
            if (item.Toppings.Count > 2)
                return new MessageResult()
                {
                    Result = 0,
                    Message = "Só lượng topping là 2.",
                };
            int quantity = 0;
            foreach (var topping in item.Toppings)
            {
                quantity += topping.Quantity;
            }
            if (quantity > 2)
            {
                return new MessageResult()
                {
                    Result = 0,
                    Message = "Só lượng topping là 2.",
                };
            }
            //
            subTotal += (item.PriceProduct + item.PriceSize) * item.Quantity;
            foreach (var topping in item.Toppings)
            {
                subTotal += topping.SubPrice * topping.Quantity;
            }
        }
        var orderRe = new Order()
        {
            ID = Guid.NewGuid().ToString(),
            TableNum = request.TableNum,
            Cashier = request.Cashier,
            Code = "",
            SubAmount = subTotal,
            TotalAmount = subTotal - (request.ReducePromotion + request.ReduceAmount),
            Status = request.OrderType == OrderType.Shipping ? OrderStatus.Open : OrderStatus.Finished,
            OrderType = request.OrderType,
            ReducePromotion = request.ReducePromotion,
            ReduceAmount = request.ReduceAmount,
            CustomerPut = request.CustomerPut ,
            CustomerReceive = request.CustomerPut - (subTotal - request.ReducePromotion - request.ReduceAmount), 
            ShippingFee = request.ShippingFee,
            CreateDate = DateTime.Now,
            Description  = request.Description,
            CancellationReason  = null,
            UserCreateID = UserID,
            PaymentType = request.PaymentType,
            CustomerID = request.CustomerID,
            BranchID = request.BranchID,
        };
        _context.Orders.Add(orderRe);
        var orderDetails = new List<OrderDetail>();
        foreach (var item in request.OrderItems)
        {
            if (item.Toppings.Count > 0)
            {
                orderDetails.Add(new OrderDetail()
                {
                    OrderID = orderRe.ID,
                    Quantity = item.Quantity,
                    PriceProduct = item.PriceProduct,
                    Description = item.Description,
                    SugarType = item.SugarType,
                    PriceSize = item.PriceSize,
                    SizeID = item.SizeID,
                    ProductID = item.ProductID,
                    ToppingID1 = item.Toppings[0].ID,
                    Topping1Name = null,
                    PriceToppping1 = item.Toppings[0].SubPrice,
                    ToppingID2 = item.Toppings[1] != null ? item.Toppings[1]?.ID : "",
                    Topping2Name  = null,
                    PriceToppping2 = item.Toppings[1] != null ? item.Toppings[1].SubPrice : 0,
                });
            }
            else
            {
                orderDetails.Add(new OrderDetail()
                {
                    OrderID = orderRe.ID,
                    Quantity = item.Quantity,
                    PriceProduct = item.PriceProduct,
                    Description = item.Description,
                    SugarType = item.SugarType,
                    PriceSize = item.PriceSize,
                    SizeID = item.SizeID,
                    ProductID = item.ProductID,
                    ToppingID1 = null,
                    Topping1Name = null,
                    PriceToppping1 = 0,
                    ToppingID2 = null,
                    Topping2Name = null,
                    PriceToppping2 =0,
                });
            }
        }
        _context.OrderDetails.AddRange(orderDetails);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công"
        };
    }

    public Task<MessageResult> Update(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Respond<PagedList<Order>>> GetByBranhID(string branhID, Search request)
    {
        var query = from c in _context.Orders where c.BranchID == branhID select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Code.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<Order>();
        if (request.IsPging)
        {
            data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
            data = await query.ToListAsync();
        var pagedResult = new PagedList<Order>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<Respond<PagedList<Order>>> GetByUser(string userID, Search request)
    {
        var query = from c in _context.Orders where c.UserCreateID == userID select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Code.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<Order>();
        if (request.IsPging)
        {
            data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
            data = await query.ToListAsync();
        var pagedResult = new PagedList<Order>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<Respond<PagedList<Order>>> GetAll(Search request)
    {
        var query = from c in _context.Orders select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Code.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<Order>();
        if (request.IsPging)
        {
            data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
            data = await query.ToListAsync();
        var pagedResult = new PagedList<Order>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<Order>> GetById(string orderID)
    {
        var order = await _context.Orders
            .Include(x=>x.OrderDetails)
            .FirstOrDefaultAsync(x=>x.ID==orderID);
        if (order == null)
            return new Respond<Order>()
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
                Data = null,
            };
        return new Respond<Order>()
        {
            Message = "Thành công",
            Result = 1,
            Data = order,
        };
    }
    
    public async Task<MessageResult> UpdateStatus(string orderID, OrderStatus status)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x=>x.ID==orderID);
        if (order == null)
            return new MessageResult()
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
            };
        order.Status = status;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Thành công",
            Result = 1,
        };
    }
    public async Task<MessageResult> Delete(string orderID)
    {
        var order = await _context.Orders
            .Include(x=>x.OrderDetails)
            .FirstOrDefaultAsync(x=>x.ID==orderID);
        if (order == null)
            return new MessageResult()
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
            };
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Thành công",
            Result = 1,
        };
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
