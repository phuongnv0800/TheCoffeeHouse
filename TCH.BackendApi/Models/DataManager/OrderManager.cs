using AutoMapper;
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
    public OrderManager(APIContext context, HttpContextAccessor httpContext, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
    //    public async Task<bool> Create(OrderRequest request)
    //    {
    //        var orderRe = new Order()
    //        {
    //            DateCreated = DateTime.Now,
    //            ShipPhone = request.ShipPhone,
    //            ShipAddress = request.ShipAddress,
    //            ShipName = request.ShipName,
    //            Status = OrderStatus.InProgress,
    //            UserId = request.UserId
    //        };
    //        _context.Orders.Add(orderRe);
    //        await _context.SaveChangesAsync();
    //        var order = await _context.Orders
    //            .Where(x => x.UserId == orderRe.UserId && x.DateCreated == orderRe.DateCreated)
    //            .FirstOrDefaultAsync();
    //        var query = from c in _context.Carts where c.UserId == request.UserId select new { c };
    //        var orderDetails = await _context.Carts.Where(x => x.UserId == request.UserId).Select(x => new OrderDetail()
    //        {
    //            ProductId = x.ProductId,
    //            Price = x.Price,
    //            SubTotal = x.SubTotal,
    //            Quantity = x.Quantity,
    //            OrderId = order.Id
    //        }).ToListAsync();
    //        order.OrderDetails = orderDetails;
    //        _context.Orders.Update(order);
    //        var carts = await _context.Carts.Where(x => x.UserId == request.UserId).ToListAsync();
    //        _context.Carts.RemoveRange(carts);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    public async Task<bool> Update(OrderRequest request)
    //    {
    //        var order = await _context.Orders.FindAsync(request.Id);
    //        if (!string.IsNullOrWhiteSpace(request.ShipAddress))
    //            order.ShipAddress = request.ShipAddress;
    //        if (!string.IsNullOrWhiteSpace(request.ShipName))
    //            order.ShipName = request.ShipName;
    //        if (!string.IsNullOrWhiteSpace(request.ShipPhone))
    //            order.ShipPhone = request.ShipPhone;
    //        order.Status = request.Status;
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    public async Task<PagedList<OrderVm>> GetAllPaging(PagingRequest request)
    //    {
    //        var query = from order in _context.Orders select new { order };
    //        if (!string.IsNullOrEmpty(request.Keyword))
    //            query = query.Where(x => x.order.ShipName.Contains(request.Keyword));
    //        int totalRow = await query.CountAsync();
    //        var data = await query
    //            .Select(x => new OrderVm()
    //            {
    //                Id = x.order.Id,
    //                ShipAddress = x.order.ShipAddress,
    //                ShipName = x.order.ShipName,
    //                ShipPhone = x.order.ShipPhone,
    //                Status = x.order.Status,
    //                DateCreated = x.order.DateCreated,
    //            })
    //            .OrderBy(x => x.Id)
    //            .Skip((request.PageNumber - 1) * request.PageSize)
    //            .Take(request.PageSize)
    //            .ToListAsync();
    //        select
    //        var pagedResult = new PagedList<OrderVm>()
    //        {
    //            MetaData = new MetaData()
    //            {
    //                TotalRecord = totalRow,
    //                PageSize = request.PageSize,
    //                CurrentPage = request.PageNumber,
    //                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
    //            },
    //            Items = data,
    //        };
    //        return pagedResult;
    //    }
    //    public async Task<PagedList<OrderVm>> GetByUser(Guid userId, PagingRequest request)
    //    {
    //        var query = from order in _context.Orders where order.UserId == userId  select new { order };
    //        if (!string.IsNullOrEmpty(request.Keyword))
    //            query = query.Where(x => x.order.ShipName.Contains(request.Keyword));
    //        int totalRow = await query.CountAsync();
    //        var data = await query.Select(x => new OrderVm()
    //            {
    //                Id = x.order.Id,
    //                ShipAddress = x.order.ShipAddress,
    //                ShipName = x.order.ShipName,
    //                ShipPhone = x.order.ShipPhone,
    //                Status = x.order.Status,
    //                DateCreated = x.order.DateCreated,
    //            })
    //            .OrderBy(x => x.Id)
    //            .Skip((request.PageNumber - 1) * request.PageSize)
    //            .Take(request.PageSize)
    //            .ToListAsync();
    //        select
    //        var pagedResult = new PagedList<OrderVm>()
    //        {
    //            MetaData = new MetaData()
    //            {
    //                TotalRecord = totalRow,
    //                PageSize = request.PageSize,
    //                CurrentPage = request.PageNumber,
    //                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
    //            },
    //            Items = data,
    //        };
    //        return pagedResult;
    //    }
    //    public async Task<OrderVm> GetById(int orderId)
    //    {
    //        var order = await _context.Orders.FindAsync(orderId);
    //        if (order == null)
    //            throw new TCHException("order not found");

    //        var query = from o in _context.Orders
    //                    join od in _context.OrderDetails on o.Id equals od.OrderId
    //                    join p in _context.Products on od.ProductId equals p.Id
    //                    select new { od, o, p };
    //        var orderList = await query.Select(x => new OrderList()
    //        {
    //            ProductId = x.od.ProductId,
    //            Price = x.od.Price,
    //            SubTotal = x.od.SubTotal,
    //            Quantity = x.od.Quantity,
    //            OrderId = order.Id,
    //            Name = x.p.Name,
    //            ImagePath = x.p.ImagePath
    //        }).ToListAsync();
    //        var orderVm = new OrderVm()
    //        {
    //            Id = order.Id,
    //            ShipAddress = order.ShipAddress,
    //            ShipName = order.ShipName,
    //            ShipPhone = order.ShipPhone,
    //            Status = order.Status,
    //            DateCreated = order.DateCreated,
    //            OrderLists = orderList
    //        };
    //        return orderVm;
    //    }

    public async Task<MessageResult> Create(OrderRequest request)
    {
        var branch = await _context.Branches.FindAsync(request.BranchID);
        if( branch == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không có thông tin nhánh",
            };
        }

        var orderRe = new Order()
        {
            ID = Guid.NewGuid().ToString(),
            Code = "",
            SubAmount = 0,
            TotalAmount =0,
            OrderType = request.OrderType,
            ReducePromotion = request.ReducePromotion,
            ReduceAmount = request.ReduceAmount,
            CustomerPut = request.CustomerPut,
            CustomerReceive = request.CustomerReceive, 
            ShippingFee = request.ShippingFee,
            CreateDate = DateTime.Now,
            Description  = request.Description,
            CancellationReason  = "",
            UserCreateID = UserID,
            PaymentType = request.PaymentType,
            CustomerID = request.CustomerID,
            BranchID = request.BranchID,
        };
        _context.Orders.Add(orderRe);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = ""
        };
    }

    public Task<MessageResult> Update(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> Delete(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Respond<PagedList<OrderVm>>> GetByBranhID(string branhID, Search request)
    {
        throw new NotImplementedException();
    }

    public Task<Respond<PagedList<OrderVm>>> GetAll(Search request)
    {
        throw new NotImplementedException();
    }

    public Task<OrderVm> GetById(string orderID)
    {
        throw new NotImplementedException();
    }

    public Task<Respond<PagedList<OrderVm>>> GetByUser(Guid userID, Search request)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResult> Delete(string orderID)
    {
        throw new NotImplementedException();
    }
}
