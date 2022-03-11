//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.IO;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using TCH.BackendApi.EF;
//using TCH.ViewModel.Common;
//using TCH.Utilities.Exceptions;
//using TCH.BackendApi.Service.Common;
//using TCH.ViewModel.Catalog;
//using TCH.BackendApi.Entities;
//using System.Net.Http.Headers;
//using System.Collections.Generic;
//using TCH.Utilities.Enums;
//using TCH.Utilities.Constants;

//namespace TCH.BackendApi.Service.Catalog
//{
//    public class ProductService : IProductService
//    {
//        private readonly ShopDbContext _context;
//        private readonly IStorageService _storageService;
//        private const string USER_CONTENT_FOLDER_NAME = "products";

//        public ProductService(ShopDbContext context, IStorageService storageService)
//        {
//            _context = context;
//            _storageService = storageService;
//        }

//        public async Task<int> Create(ProductRequest request)
//        {
//            var product = new Product()
//            {
//                Price = request.Price,
//                Name = request.Name,
//                Description = request.Description,
//                Quantity = request.Quantity,
//                Rating = 0,
//                RatingCount = 0,
//                Discount = request.Discount,
//                ProductMovement = request.ProductMovement,
//                ProductColor = request.ProductColor,
//                CategoryId = request.CategoryId,
//            };

//            if (request.ImagePath != null)
//            {
//                product.ProductImages = new List<ProductImage>()
//                {
//                    new ProductImage()
//                    {
//                        Caption = "product image",
//                        DateCreated = DateTime.Now,
//                        Size = request.ImagePath.Length,
//                        Name = request.ImagePath.FileName,
//                        ImagePath = await SaveFileIFormFile(request.ImagePath),
//                        IsShowHome = false,
//                    }
//                };
//            }
//            _context.Products.Add(product);
//            await _context.SaveChangesAsync();
//            return product.Id;
//        }

//        public async Task<int> Update(ProductRequest request)
//        {
//            var product = await _context.Products.FindAsync(request.Id);
//            if (product == null)
//                throw new TCHException($"Can't not find product with Id: {request.Id}");
//            product.Name = request.Name;
//            product.Description = request.Description;
//            product.Price = request.Price;
//            product.Quantity = request.Quantity;
//            product.Discount = request.Discount;
//            product.ProductColor = request.ProductColor;
//            product.Rating = request.Rating;
//            product.RatingCount = request.RatingCount;
//            if (request.ImagePath != null)
//            {
//                var productImage = await _context.ProductImages.Where(x => x.ProductId == request.Id).FirstOrDefaultAsync();
//                if (productImage != null)
//                {
//                    await _storageService.DeleteFileAsync(productImage.ImagePath);
//                    productImage.ImagePath = await SaveFileIFormFile(request.ImagePath);
//                    productImage.Size = request.ImagePath.Length;
//                    productImage.Name = request.ImagePath.FileName;
//                }
//            }
//            return await _context.SaveChangesAsync();
//        }

//        public async Task<int> Delete(int productId)
//        {
//            var product = await _context.Products.FindAsync(productId);
//            if (product == null)
//                throw new TCHException($"Can't not find product {productId}");
//            var productImages = await _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
//            foreach (var item in productImages)
//            {
//                await _storageService.DeleteFileAsync(USER_CONTENT_FOLDER_NAME + "/" + item.ImagePath);
//                _context.ProductImages.Remove(item);
//            }
//            _context.Products.Remove(product);
//            return await _context.SaveChangesAsync();
//        }

//        public async Task<ProductVm> GetById(int productId)
//        {
//            var product = await _context.Products.FindAsync(productId);
//            var category = await _context.Categories.FindAsync(product.CategoryId);
//            if (product == null)
//                throw new TCHException($"Can't not find product with Id: {productId}");

//            var productVm = new ProductVm()
//            {
//                Id = product.Id,
//                Name = product.Name,
//                Price = product.Price,
//                Description = product.Description,
//                Quantity = product.Quantity,
//                ProductColor = product.ProductColor,
//                ProductMovement = product.ProductMovement,
//                Rating = product.Rating,
//                RatingCount = product.RatingCount,
//                Discount = product.Discount,
//                CategoryId = product.CategoryId,
//                CategoryName = category.Name,
//            };
//            return productVm;
//        }

//        public async Task<PagedList<ProductVm>> GetAll(PagingRequest request, int categoryId = 0)
//        {
//            var query = from p in _context.Products join c in _context.Categories on p.CategoryId equals c.Id select new { p, c };
//            if (!string.IsNullOrEmpty(request.Keyword))
//                query = query.Where(x => x.p.Name.Contains(request.Keyword));
//            //filter de tim kiem
//            if (categoryId != 0)
//                query = query.Where(x => x.p.CategoryId == categoryId);

//            //paging
//            int totalRow = await query.CountAsync();
//            var data = await query
//                .Select(x => new ProductVm()
//                {
//                    Id = x.p.Id,
//                    Name = x.p.Name,
//                    Price = x.p.Price,
//                    Description = x.p.Description,
//                    Quantity = x.p.Quantity,
//                    ProductColor = x.p.ProductColor,
//                    ProductMovement = x.p.ProductMovement,
//                    Rating = x.p.Rating,
//                    RatingCount = x.p.RatingCount,
//                    Discount = x.p.Discount,
//                    CategoryId = x.p.CategoryId,
//                    CategoryName = x.c.Name,
//                })
//                .OrderBy(x => x.Id)
//                .Skip((request.PageNumber - 1) * request.PageSize)
//                .Take(request.PageSize)
//                .ToListAsync();
//            // select
//            var pagedResult = new PagedList<ProductVm>()
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

//        public async Task<bool> CategoryAssign(int id, int categoryId)
//        {
//            var product = await _context.Products.FindAsync(id);
//            if (product == null)
//            {
//                return false;
//            }
//            product.CategoryId = categoryId;
//            await _context.SaveChangesAsync();

//            return true;
//        }

//        public async Task<List<ProductImageVm>> GetAllImages(int productId)
//        {
//            return await _context.ProductImages.Where(x => x.ProductId == productId)
//                .Select(i => new ProductImageVm()
//                {
//                    Caption = i.Caption,
//                    DateCreated = i.DateCreated,
//                    Size = i.Size,
//                    Id = i.Id,
//                    ImagePath = SystemConstants.BaseUrlImage+ USER_CONTENT_FOLDER_NAME + "/"+  i.ImagePath,
//                    IsShowHome = i.IsShowHome,
//                    ProductId = i.ProductId,
//                    Name = i.Name,
//                }).ToListAsync();
//        }

//        public async Task<int> AddImage(int productId, ProductImageRequest request)
//        {
//            var productImage = new ProductImage()
//            {
//                Caption = request.Caption,
//                DateCreated = DateTime.Now,
//                IsShowHome = request.IsShowHome,
//                ProductId = productId,
//            };

//            if (request.ImageFile != null)
//            {
//                productImage.ImagePath = await this.SaveFileIFormFile(request.ImageFile);
//                productImage.Size = request.ImageFile.Length;
//                productImage.Name = request.ImageFile.FileName;
//            }
//            _context.ProductImages.Add(productImage);
//            await _context.SaveChangesAsync();
//            return productImage.Id;
//        }

//        public async Task<ProductImageVm> GetImageById(int imageId)
//        {
//            var image = await _context.ProductImages.FindAsync(imageId);
//            if (image == null)
//                throw new TCHException($"Cannot find an image with id {imageId}");

//            var viewModel = new ProductImageVm()
//            {
//                Caption = image.Caption,
//                DateCreated = image.DateCreated,
//                Size = image.Size,
//                Name = image.Name,
//                Id = image.Id,
//                ImagePath = SystemConstants.BaseUrlImage + USER_CONTENT_FOLDER_NAME + "/" + image.ImagePath,
//                IsShowHome = image.IsShowHome,
//                ProductId = image.ProductId,
//            };
//            return viewModel;
//        }

//        public async Task<int> UpdateImage(int imageId, ProductImageRequest request)
//        {
//            var productImage = await _context.ProductImages.FindAsync(imageId);
//            if (productImage == null)
//                throw new TCHException($"Cannot find an image with id {imageId}");

//            if (request.ImageFile != null)
//            {
//                productImage.Name = request.Name;
//                productImage.ImagePath = await this.SaveFileIFormFile(request.ImageFile);
//                productImage.Size = request.ImageFile.Length;
//                productImage.IsShowHome = request.IsShowHome;
//                productImage.Caption = request.Caption;
//            }
//            _context.ProductImages.Update(productImage);
//            return await _context.SaveChangesAsync();
//        }

//        public async Task<int> RemoveImage(int imageId)
//        {
//            var productImage = await _context.ProductImages.FindAsync(imageId);
//            if (productImage == null)
//                throw new TCHException($"Cannot find an image with id {imageId}");
//            await _storageService.DeleteFileAsync(USER_CONTENT_FOLDER_NAME + "/" + productImage.ImagePath);
//            _context.ProductImages.Remove(productImage);
//            return await _context.SaveChangesAsync();
//        }

//        private async Task<string> SaveFileIFormFile(IFormFile file)
//        {
//            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
//            await _storageService.SaveFileAsync(file.OpenReadStream(), USER_CONTENT_FOLDER_NAME + "/" + fileName);
//            return fileName;
//        }
//    }
//}
