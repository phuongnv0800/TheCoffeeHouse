//using Microsoft.EntityFrameworkCore;
//using TCH.BackendApi.EF;
//using TCH.BackendApi.Entities;
//using TCH.Utilities.Enums;
//using TCH.Utilities.Exceptions;
//using TCH.ViewModel.Catalog;
//using TCH.ViewModel.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Service.Catalog
//{
//    public class OrderService : IOrderService
//    {
//        private readonly ShopDbContext _context;

//        public OrderService(ShopDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<bool> Create(OrderRequest request)
//        {
//            var orderRe = new Order()
//            {
//                DateCreated = DateTime.Now,
//                ShipPhone = request.ShipPhone,
//                ShipAddress = request.ShipAddress,
//                ShipName = request.ShipName,
//                Status = OrderStatus.InProgress,
//                UserId = request.UserId
//            };
//            _context.Orders.Add(orderRe);
//            await _context.SaveChangesAsync();
//            var order = await _context.Orders
//                .Where(x => x.UserId == orderRe.UserId && x.DateCreated == orderRe.DateCreated)
//                .FirstOrDefaultAsync();
//            //var query = from c in _context.Carts where c.UserId == request.UserId select new { c };
//            var orderDetails = await _context.Carts.Where(x => x.UserId == request.UserId).Select(x => new OrderDetail()
//            {
//                ProductId = x.ProductId,
//                Price = x.Price,
//                SubTotal = x.SubTotal,
//                Quantity = x.Quantity,
//                OrderId = order.Id
//            }).ToListAsync();
//            order.OrderDetails = orderDetails;
//            _context.Orders.Update(order);
//            var carts = await _context.Carts.Where(x => x.UserId == request.UserId).ToListAsync();
//            _context.Carts.RemoveRange(carts);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<bool> Update(OrderRequest request)
//        {
//            var order = await _context.Orders.FindAsync(request.Id);
//            if (!string.IsNullOrWhiteSpace(request.ShipAddress))
//                order.ShipAddress = request.ShipAddress;
//            if (!string.IsNullOrWhiteSpace(request.ShipName))
//                order.ShipName = request.ShipName;
//            if (!string.IsNullOrWhiteSpace(request.ShipPhone))
//                order.ShipPhone = request.ShipPhone;
//            order.Status = request.Status;
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<PagedList<OrderVm>> GetAllPaging(PagingRequest request)
//        {
//            var query = from order in _context.Orders select new { order };
//            if (!string.IsNullOrEmpty(request.Keyword))
//                query = query.Where(x => x.order.ShipName.Contains(request.Keyword));
//            int totalRow = await query.CountAsync();
//            var data = await query
//                .Select(x => new OrderVm()
//                {
//                    Id = x.order.Id,
//                    ShipAddress = x.order.ShipAddress,
//                    ShipName = x.order.ShipName,
//                    ShipPhone = x.order.ShipPhone,
//                    Status = x.order.Status,
//                    DateCreated = x.order.DateCreated,
//                })
//                .OrderBy(x => x.Id)
//                .Skip((request.PageNumber - 1) * request.PageSize)
//                .Take(request.PageSize)
//                .ToListAsync();
//            // select
//            var pagedResult = new PagedList<OrderVm>()
//            {
//                MetaData = new MetaData()
//                {
//                    TotalRecord = totalRow,
//                    PageSize = request.PageSize,
//                    CurrentPage = request.PageNumber,
//                    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
//                },
//                Items = data,
//            };
//            return pagedResult;
//        }
//        public async Task<PagedList<OrderVm>> GetByUser(Guid userId, PagingRequest request)
//        {
//            var query = from order in _context.Orders where order.UserId == userId  select new { order };
//            if (!string.IsNullOrEmpty(request.Keyword))
//                query = query.Where(x => x.order.ShipName.Contains(request.Keyword));
//            int totalRow = await query.CountAsync();
//            var data = await query.Select(x => new OrderVm()
//                {
//                    Id = x.order.Id,
//                    ShipAddress = x.order.ShipAddress,
//                    ShipName = x.order.ShipName,
//                    ShipPhone = x.order.ShipPhone,
//                    Status = x.order.Status,
//                    DateCreated = x.order.DateCreated,
//                })
//                .OrderBy(x => x.Id)
//                .Skip((request.PageNumber - 1) * request.PageSize)
//                .Take(request.PageSize)
//                .ToListAsync();
//            // select
//            var pagedResult = new PagedList<OrderVm>()
//            {
//                MetaData = new MetaData()
//                {
//                    TotalRecord = totalRow,
//                    PageSize = request.PageSize,
//                    CurrentPage = request.PageNumber,
//                    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
//                },
//                Items = data,
//            };
//            return pagedResult;
//        }
//        public async Task<OrderVm> GetById(int orderId)
//        {
//            var order = await _context.Orders.FindAsync(orderId);
//            if (order == null)
//                throw new TCHException("order not found");

//            var query = from o in _context.Orders
//                        join od in _context.OrderDetails on o.Id equals od.OrderId
//                        join p in _context.Products on od.ProductId equals p.Id
//                        select new { od, o, p };
//            var orderList = await query.Select(x => new OrderList()
//            {
//                ProductId = x.od.ProductId,
//                Price = x.od.Price,
//                SubTotal = x.od.SubTotal,
//                Quantity = x.od.Quantity,
//                OrderId = order.Id,
//                Name = x.p.Name,
//                ImagePath = x.p.ImagePath
//            }).ToListAsync();
//            var orderVm = new OrderVm()
//            {
//                Id = order.Id,
//                ShipAddress = order.ShipAddress,
//                ShipName = order.ShipName,
//                ShipPhone = order.ShipPhone,
//                Status = order.Status,
//                DateCreated = order.DateCreated,
//                OrderLists = orderList
//            };
//            return orderVm;
//        }
//    }
//}
