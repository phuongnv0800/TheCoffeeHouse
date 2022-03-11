//using Microsoft.EntityFrameworkCore;
//using TCH.BackendApi.EF;
//using TCH.BackendApi.Entities;
//using TCH.Utilities.Constants;
//using TCH.ViewModel.Catalog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Service.Catalog
//{
//    public class CartService : ICartService
//    {
//        private readonly ShopDbContext _context;
//        private const string USER_CONTENT_FOLDER_NAME = "products";

//        public CartService(ShopDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<IEnumerable<CartVm>> Get(Guid userId)
//        {
//            var query = from p in _context.Products 
//                        join c in _context.Carts on p.Id equals c.ProductId
//                        where c.UserId == userId select new { c , p};
//            var query2 = from p in _context.Products
//                        join c in _context.Carts on p.Id equals c.ProductId
//                        join pm in _context.ProductImages on p.Id equals pm.ProductId
//                        where c.UserId == userId
//                        select new { pm, c, p };
//            var images = await query2.Select(x=>x.pm.ImagePath).ToListAsync();
//            var carts = await query.Select(x => new CartVm()
//            {
//                UserId = x.c.UserId,
//                ProductId = x.c.ProductId,
//                Price = x.c.Price,
//                Name  = x.p.Name,
//                ImagePath = SystemConstants.BaseUrlImage + USER_CONTENT_FOLDER_NAME + "/" + images.FirstOrDefault(),
//                Quantity = x.c.Quantity,
//                SubTotal = x.c.SubTotal,
//                DateCreated = x.c.DateCreated
//            }).ToListAsync();
//            return carts;
//        }
//        public async Task<IEnumerable<CartVm>> GetByUserName(string userName)
//        {
//            var query = from c in _context.Carts
//                        join p in _context.Products on c.ProductId equals p.Id
//                        join u in _context.Users on c.UserId equals u.Id
//                        select new {  c, p, u };
//            var query2 = from c in _context.Carts
//                        join p in _context.Products on c.ProductId equals p.Id
//                        join pm in _context.ProductImages on p.Id equals pm.ProductId
//                        join u in _context.Users on c.UserId equals u.Id
//                        select new { pm, c, p, u };
//            var images = await query2.Where(y => y.u.UserName.Equals(userName)).Select(x=> x.pm.ImagePath).ToListAsync();
//            var carts = await query.Where(y=>y.u.UserName.Equals(userName))
//                .Select(x => new CartVm()
//                {
//                    UserId = x.c.UserId,
//                    ProductId = x.c.ProductId,
//                    Price = x.c.Price,
//                    Name = x.p.Name,
//                    ImagePath = SystemConstants.BaseUrlImage + USER_CONTENT_FOLDER_NAME + "/" + images.FirstOrDefault(),
//                    Quantity = x.c.Quantity,
//                    SubTotal = x.c.SubTotal,
//                    DateCreated = x.c.DateCreated
//                }).ToListAsync();
//            return carts;
//        }
//        public async Task<bool> Create(CartRequest request)
//        {
//            var product = await _context.Products.FindAsync(request.ProductId);

//            var query = from c in _context.Carts where c.UserId == request.UserId && c.ProductId == request.ProductId select c;
//            var cart = await query.Select(x => new Cart()
//            {
//                ProductId = x.ProductId,
//                UserId = x.UserId,
//                Price = x.Price,
//                DateCreated = x.DateCreated,
//                Quantity = x.Quantity,
//                SubTotal = x.SubTotal,
//            }).FirstOrDefaultAsync();
//            if (cart != null)
//            {
//                cart.Quantity = request.Quantity;
//                cart.SubTotal = request.Quantity * product.Price;
//                _context.Update(cart);
//            }
//            else
//            {
//                Cart newCart = new Cart()
//                {
//                    ProductId = request.ProductId,
//                    UserId = request.UserId,
//                    Price = product.Price,
//                    DateCreated = DateTime.Now,
//                    Quantity = request.Quantity,
//                    SubTotal = request.Quantity * product.Price,
//                };
//                await _context.Carts.AddAsync(newCart);
//            }
            
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<bool> Delete(Guid userId, int productId)
//        {
//            var query = from c in _context.Carts where c.UserId == userId && c.ProductId == productId select c;
//            var cart = await query.Select(x => new Cart()
//            {
//                ProductId = x.ProductId,
//                UserId = x.UserId,
//                Price = x.Price,
//                DateCreated = x.DateCreated,
//                Quantity = x.Quantity,
//                SubTotal = x.SubTotal,
//            }).FirstOrDefaultAsync();
//            if (cart == null)
//                return true;
//            _context.Carts.Remove(cart);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}