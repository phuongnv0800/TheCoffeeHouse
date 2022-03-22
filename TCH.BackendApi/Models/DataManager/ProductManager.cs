using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Entities;
using System.Net.Http.Headers;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.EF;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.ViewModels;
using TCH.BackendApi.Models.Searchs;
using AutoMapper;
using TCH.BackendApi.Config;

namespace TCH.BackendApi.Models.DataManager
{
    public class ProductManager : IProductRepository, IDisposable
    {
        private readonly APIContext _context;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? UserID;
        private const string USER_CONTENT_FOLDER_NAME = "products";

        public IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;

        public ProductManager(APIContext context, IStorageService storageService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
            //_accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        }

        private async Task<string> SaveFileIFormFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), USER_CONTENT_FOLDER_NAME + "/" + fileName);
            return fileName;
        }

        public async Task<Respond<PagedList<ProductVm>>> GetAll(Search request)
        {
            var query = from c in _context.Products select c;
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            //paging
            int totalRow = await query.CountAsync();
            if (request.IsPging == true)
            {
                var data = await query
                .Select(x => _mapper.Map<ProductVm>(x))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(x => x.Name)
                .ToListAsync();
                // select
                var pagedResult = new PagedList<ProductVm>()
                {
                    TotalRecord = totalRow,
                    PageSize = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                    Items = data,
                };
                return new Respond<PagedList<ProductVm>>()
                {
                    Data = pagedResult,
                    Result = 1,
                    Message = "Thành công",
                };
            }
            else
            {
                var data = await query
                .Select(x => _mapper.Map<ProductVm>(x))
                .OrderBy(x => x.Name)
                .ToListAsync();
                // select
                var pagedResult = new PagedList<ProductVm>()
                {
                    TotalRecord = totalRow,
                    PageSize = totalRow,
                    CurrentPage = 1,
                    TotalPages = 1,
                    Items = data,
                };
                return new Respond<PagedList<ProductVm>>()
                {
                    Data = pagedResult,
                    Result = 1,
                    Message = "Thành công",
                };
            }
        }

        public async Task<MessageResult> Create(ProductRequest request)
        {
            var category = await _context.Categories.FindAsync(request.CategoryID);
            if (category == null)
            {
                return new MessageResult()
                {
                    Message = "Danh mục không tồn tại",
                    Result = -1,
                };
            }
            var product = _mapper.Map<Product>(request);
            product.ID = Guid.NewGuid().ToString();
            product.CreateDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;
            product.UserCreateID = UserID;
            product.UserUpdateID = UserID;
            product.NormalizedName = request.Name.Normalize();

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

        public async Task<MessageResult> Update(string productID, ProductVm request)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
                //    throw new CustomException($"Can't not find product with Id: {request.Id}");
                new MessageResult()
                {
                    Result = -1,
                    Message = "Không tìm thấy sản phẩm",
                };
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.UpdateDate = DateTime.Now;
            product.Formula = request.Formula;
            product.ProductType = request.ProductType;
            product.UserUpdateID = UserID;
            product.Description = request.Description;
            product.Unit = request.Unit;
            product.NormalizedName = request.Name.Normalize();
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Result = 1,
                Message = "Cập nhật sản phẩm thành công",
            };
        }

        public async Task<MessageResult> CategoryAssign(string productID, string categoryID)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                //throw new CustomException($"No find produc with id:{productID}");
                new MessageResult()
                {
                    Result = -1,
                    Message = "Không tìm thấy sản phẩm",
                };
            }
            var category = await _context.Categories.FindAsync(categoryID);
            if (category == null)
            {
                new MessageResult()
                {
                    Result = -2,
                    Message = "Không tìm thấy danh mục",
                };
            }
            product.CategoryID = category.ID;
            product.Category = category;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return new MessageResult()
            {
                Result = 1,
                Message = "Cập nhật danh mục thành công",
            };
        }

        public async Task<Respond<Product>> GetById(string productID)
        {
            var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.ID == productID);
            if (product == null)
                return new Respond<Product>()
                {
                    Result = -1,
                    Message = "Không tìm thấy sản phẩm",
                };

            return new Respond<Product>()
            {
                Result = 1,
                Message = "Không tìm thấy sản phẩm",
                Data = product,
            };
        }

        public async Task<MessageResult> AddImage(string productID, ProductImageRequest request)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                return new MessageResult()
                {
                    Message = "Sản phẩm không tồn tại",
                    Result = -1,
                };
            }
            var productImage = new ProductImage()
            {
                ID = Guid.NewGuid().ToString(),
                Caption = request.Caption,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsShowHome = request.IsShowHome,
                ProductId = productID,
                Product = product,
            };
            var nameFile = await SaveFileIFormFile(request.ImageFile);
            if (request.ImageFile != null)
            {
                productImage.ImagePath = USER_CONTENT_FOLDER_NAME + "/" + nameFile;
                productImage.Size = request.ImageFile.Length;
                productImage.Name = nameFile;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Message = "Thêm hình ảnh sản phẩm thành công",
                Result = 1,
            };
        }

        public async Task<Respond<PagedList<ProductImageVm>>> GetAllImages(string productID)
        {
            var productImages = await _context.ProductImages.Where(x => x.ProductId == productID)
                .Select(i => _mapper.Map<ProductImageVm>(i)).ToListAsync();
            if (productImages == null)
            {
                return new Respond<PagedList<ProductImageVm>>()
                {
                    Data = null,
                    Result = -1,
                    Message = "Không có dữ liệu",
                };
            }
            var pagedResult = new PagedList<ProductImageVm>()
            {
                TotalRecord = productImages.Count,
                PageSize = productImages.Count,
                CurrentPage = 1,
                TotalPages = productImages.Count,
                Items = productImages,
            };
            return new Respond<PagedList<ProductImageVm>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }

        public async Task<Respond<ProductImageVm>> GetImageById(string imageID)
        {
            var image = await _context.ProductImages.FindAsync(imageID);
            if (image == null)
                throw new CustomException($"Cannot find an image with id {imageID}");

            var viewModel = _mapper.Map<ProductImageVm>(image);
            return new Respond<ProductImageVm>()
            {
                Result = 1,
                Message = "Thành công",
                Data = viewModel,
            };
        }

        public async Task<MessageResult> RemoveImage(string imageID)
        {
            var productImage = await _context.ProductImages.FindAsync(imageID);
            if (productImage == null)
                throw new CustomException($"Cannot find an image with id {imageID}");
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Message = "Xoá hình ảnh sản phẩm thành công",
                Result = 1,
            };
        }

        public async Task<MessageResult> UpdateImage(string imageID, ProductImageRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageID);
            if (productImage == null)
                return new MessageResult()
                {
                    Message = "Hình ảnh sản phẩm không tồn tại",
                    Result = -1,
                };
            var namefile = await SaveFileIFormFile(request.ImageFile);
            if (request.ImageFile != null)
            {
                productImage.Name = namefile;
                productImage.ImagePath = USER_CONTENT_FOLDER_NAME + "/" + namefile;
                productImage.Size = request.ImageFile.Length;
                productImage.IsShowHome = request.IsShowHome;
                productImage.Caption = request.Caption;
                productImage.UpdateDate = DateTime.Now;
            }
            _context.ProductImages.Update(productImage);
            await _context.SaveChangesAsync();
            return new MessageResult()
            {
                Message = "Cập nhật hình ảnh sản phẩm thành công",
                Result = 1,
            };
        }
        public void Dispose()
        {
            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(this);
        }

        public async Task<Respond<PagedList<SizeVm>>> GetAllSize()
        {
            var sizes = await _context.Sizes.ToListAsync();
            if (sizes == null)
            {
                return new Respond<PagedList<SizeVm>>()
                {
                    Data = null,
                    Result = -1,
                    Message = "Không có dữ liệu",
                };
            }
            var sizeVms = sizes.Select(x=> _mapper.Map<SizeVm>(x)).ToList();
            var pagedResult = new PagedList<SizeVm>()
            {
                TotalRecord = sizeVms.Count,
                PageSize = sizeVms.Count,
                CurrentPage = 1,
                TotalPages = sizeVms.Count,
                Items = sizeVms,
            };
            return new Respond<PagedList<SizeVm>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }

        public Task<MessageResult> UpdateSize(string sizeID, SizeVm size)
        {
            throw new NotImplementedException();
        }

        public Task<MessageResult> CreateSize(SizeVm size)
        {
            throw new NotImplementedException();
        }

        public Task<MessageResult> DeleteSize(string sizeID)
        {
            throw new NotImplementedException();
        }
    }
}
