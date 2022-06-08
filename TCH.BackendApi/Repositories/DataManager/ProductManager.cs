using Microsoft.EntityFrameworkCore;
using TCH.Data.Entities;
using System.Net.Http.Headers;
using TCH.Utilities.Error;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.SubModels;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.Utilities.Searchs;
using AutoMapper;
using TCH.Utilities.Claims;
using TCH.Utilities.Enum;

namespace TCH.BackendApi.Repositories.DataManager;

public class ProductManager : IProductRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? _userID;
    private const string Upload = "products";

    public ProductManager(APIContext context,
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
    public async Task<Respond<PagedList<ProductVm>>> GetAllByBranchID(string branchID, Search request)
    {
        var query = from c in _context.Products select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<ProductVm>();
        var item1 = from sp in _context.SizeInProducts
                    join s in _context.Sizes on sp.SizeID equals s.ID
                    select new { s, sp, };
        var item2 = from tp in _context.ToppingInProducts
                    join t in _context.Toppings on tp.ToppingID equals t.ID
                    select new { t, tp, };
        if (request.IsPging)
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            })
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            }).ToListAsync();
        }
        foreach (var item in data)
        {
            var sizes = await item1.Where(x => x.sp.ProductID == item.ID).Select(x => x.s).IgnoreAutoIncludes().ToListAsync();
            var toppings = await item2.Where(x => x.tp.ProductID == item.ID).Select(x => x.t).IgnoreAutoIncludes().ToListAsync();
            item.Sizes = sizes.Select(x => _mapper.Map<SizeVm>(x)).OrderBy(x=>x.SubPrice).ToList();
            item.Toppings = toppings.Select(x => _mapper.Map<ToppingVm>(x)).OrderBy(x=>x.SubPrice).ToList();
        }
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
        //var menu = await _context.Menus.FirstOrDefaultAsync(x => x.BranchID == branchID);
        //if (menu == null)
        //{
        //    return new Respond<PagedList<ProductVm>>()
        //    {
        //        Result = 0,
        //        Message = "Chi nhánh chưa có menu",
        //        Data = null,
        //    };
        //}
        //var query = from p in _context.Products
        //            join pm in _context.ProductInMenus on p.ID equals pm.ProductID
        //            where pm.MenuID == menu.ID && pm.IsActive == true
        //            select new { p, pm };
        //if (!string.IsNullOrEmpty(request.Name))
        //    query = query.Where(x => x.p.Name.Contains(request.Name));
        ////paging
        //int totalRow = await query.CountAsync();
        //var item1 = from sp in _context.SizeInProducts
        //            join s in _context.Sizes on sp.SizeID equals s.ID
        //            select new { s, sp, };
        //var item2 = from tp in _context.ToppingInProducts
        //            join t in _context.Toppings on tp.ToppingID equals t.ID
        //            select new { t, tp, };
        ////var products = await _context.Products.Include(x=>x.ProductInMenus).ToListAsync();
        //var data = new List<ProductVm>();
        //if (request.IsPging == true)
        //{
        //    data = await query.Select(x => new ProductVm()
        //    {
        //        ID = x.p.ID,
        //        Name = x.p.Name,
        //        ProductType = x.p.ProductType,
        //        CreateDate = x.p.CreateDate,
        //        UpdateDate = x.p.UpdateDate,
        //        IsSale = x.p.IsSale,
        //        PriceSale = x.p.PriceSale,
        //        IsAvailable = x.p.IsAvailable,
        //        Price = x.p.Price,
        //        Description = x.p.Description,
        //        LinkImage = x.p.LinkImage,
        //        CategoryID = x.p.CategoryID,
        //        IsActive = x.pm.IsActive,
        //    })
        //    .Skip((request.PageNumber - 1) * request.PageSize)
        //    .Take(request.PageSize).ToListAsync();
        //}
        //else
        //    data = await query.Select(x => new ProductVm()
        //    {
        //        ID = x.p.ID,
        //        Name = x.p.Name,
        //        ProductType = x.p.ProductType,
        //        CreateDate = x.p.CreateDate,
        //        UpdateDate = x.p.UpdateDate,
        //        IsSale = x.p.IsSale,
        //        PriceSale = x.p.PriceSale,
        //        IsAvailable = x.p.IsAvailable,
        //        Price = x.p.Price,
        //        Description = x.p.Description,
        //        LinkImage = x.p.LinkImage,
        //        CategoryID = x.p.CategoryID,
        //        IsActive = x.pm.IsActive,
        //    }).ToListAsync();
        //foreach (var item in data)
        //{
        //    var sizes = await item1.Where(x => x.sp.ProductID == item.ID).Select(x => x.s).IgnoreAutoIncludes().ToListAsync();
        //    var toppings = await item2.Where(x => x.tp.ProductID == item.ID).Select(x => x.t).IgnoreAutoIncludes().ToListAsync();
        //    item.Sizes = sizes.Select(x => _mapper.Map<SizeVm>(x)).ToList();
        //    item.Toppings = toppings.Select(x => _mapper.Map<ToppingVm>(x)).ToList();
        //}
        //var pagedResult = new PagedList<ProductVm>()
        //{
        //    TotalRecord = totalRow,
        //    PageSize = request.PageSize,
        //    CurrentPage = request.PageNumber,
        //    TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
        //    Items = data,
        //};
        //return new Respond<PagedList<ProductVm>>()
        //{
        //    Data = pagedResult,
        //    Result = 1,
        //    Message = "Thành công",
        //};
    }

    public async Task<Respond<PagedList<ProductVm>>> GetAll(Search request)
    {
        var query = from c in _context.Products select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<ProductVm>();
        var item1 = from sp in _context.SizeInProducts
                    join s in _context.Sizes on sp.SizeID equals s.ID
                    select new { s, sp, };
        var item2 = from tp in _context.ToppingInProducts
                    join t in _context.Toppings on tp.ToppingID equals t.ID
                    select new { t, tp, };
        if (request.IsPging)
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            })
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            }).ToListAsync();
        }
        foreach (var item in data)
        {
            var sizes = await item1.Where(x => x.sp.ProductID == item.ID).Select(x => x.s).IgnoreAutoIncludes().ToListAsync();
            var toppings = await item2.Where(x => x.tp.ProductID == item.ID).Select(x => x.t).IgnoreAutoIncludes().ToListAsync();
            item.Sizes = sizes.Select(x => _mapper.Map<SizeVm>(x)).ToList();
            item.Toppings = toppings.Select(x => _mapper.Map<ToppingVm>(x)).ToList();
        }
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
    public async Task<Respond<PagedList<ProductVm>>> GetProductByCategoryID(string categoryID, Search request)
    {
        var query = from c in _context.Products where c.CategoryID == categoryID select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<ProductVm>();
        var item1 = from sp in _context.SizeInProducts
                    join s in _context.Sizes on sp.SizeID equals s.ID
                    select new { s, sp, };
        var item2 = from tp in _context.ToppingInProducts
                    join t in _context.Toppings on tp.ToppingID equals t.ID
                    select new { t, tp, };
        if (request.IsPging)
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            })
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
        {
            data = await query.Select(x => new ProductVm()
            {
                ID = x.ID,
                Name = x.Name,
                ProductType = x.ProductType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                IsSale = x.IsSale,
                PriceSale = x.PriceSale,
                IsAvailable = x.IsAvailable,
                Price = x.Price,
                Description = x.Description,
                LinkImage = x.LinkImage,
                CategoryID = x.CategoryID,
                IsActive = true,
            }).ToListAsync();
        }
        foreach (var item in data)
        {
            var sizes = await item1.Where(x => x.sp.ProductID == item.ID).Select(x => x.s).IgnoreAutoIncludes().ToListAsync();
            var toppings = await item2.Where(x => x.tp.ProductID == item.ID).Select(x => x.t).IgnoreAutoIncludes().ToListAsync();
            item.Sizes = sizes.Select(x => _mapper.Map<SizeVm>(x)).ToList();
            item.Toppings = toppings.Select(x => _mapper.Map<ToppingVm>(x)).ToList();
        }
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

    public async Task<Respond<PagedList<Product>>> GetAll1(Search request)
    {
        var query = await _context.Products
            .Include(x => x.ToppingInProducts)
            .ThenInclude(x => x.Topping)
            .Include(x => x.SizeInProducts).ToListAsync();
        // if (!string.IsNullOrEmpty(request.Name))
        //     query = query.Where(x => x.Name.Contains(request.Name));
        //paging
        int totalRow = query.Count;
        var data = new List<Product>();
        if (request.IsPging)
        {
            data = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = query.ToList();
        var pagedResult = new PagedList<Product>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Product>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
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
        product.Description = request.Description ?? "Sản phẩm mới của chuỗi cửa hàng";
        if (request.File != null)
        {
            try
            {
                var nameFile = await SaveFileIFormFile(request.File);
                product.LinkImage = Upload + "/" + nameFile;
            }
            catch (Exception e)
            {
                throw new CustomException("Save File Create Error: " + e.Message);
            }

        }
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Tạo sản phẩm thành công",
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
        if (product.LinkImage != null)
        {
            try
            {
                await _storageService.DeleteFileAsync(product.LinkImage);
            }
            catch
            {
                throw new CustomException("Failed delete file");
            }
        }
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

    public async Task<MessageResult> Update(string productID, ProductRequest request)
    {
        var product = await _context.Products.FindAsync(productID);
        if (product == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy sản phẩm",
            };

        if (request.Price != product.Price)
        {
            var history = new HistoryPriceUpdate()
            {
                ID = Guid.NewGuid().ToString(),
                Name = product.Name,
                UpdateDate = product.UpdateDate,
                CreateDate = DateTime.UtcNow,
                StartDate = product.UpdateDate,//ngày cũ
                EndDate = DateTime.Now,
                Description = $"Cập nhật giá ngày: {product.UpdateDate} cho sản phẩm: {product.Name}",
                PriceOld = product.Price,
                PriceNew = request.Price,
                UserCreateID = _userID,
                HistoryType = HistoryType.Product,
                ProductID = productID,
            };
            await _context.HistoryPriceUpdates.AddAsync(history);
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.UpdateDate = DateTime.Now;
        product.ProductType = request.ProductType;
        product.Description = request.Description ?? product.Description;
        if (request.File != null)
        {
            try
            {
                if (product.LinkImage != null)
                {
                    await _storageService.DeleteFileAsync(product.LinkImage);
                }
                var nameFile = await SaveFileIFormFile(request.File);
                product.LinkImage = Upload + "/" + nameFile;
            }
            catch (Exception e)
            {
                throw new CustomException("Save File Create Error: " + e.Message);
            }
        }
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
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy sản phẩm",
            };
        }
        var category = await _context.Categories.FindAsync(categoryID);
        if (category == null)
        {
            return new MessageResult()
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

    public async Task<Respond<ProductVm>> GetById(string productID)
    {
        var item1 = from sp in _context.SizeInProducts
                    join s in _context.Sizes on sp.SizeID equals s.ID
                    select new { s, sp, };
        var item2 = from tp in _context.ToppingInProducts
                    join t in _context.Toppings on tp.ToppingID equals t.ID
                    select new { t, tp, };
        var product = await _context.Products.Where(x => x.ID == productID).Select(x => new ProductVm()
        {
            ID = x.ID,
            Name = x.Name,
            ProductType = x.ProductType,
            CreateDate = x.CreateDate,
            UpdateDate = x.UpdateDate,
            IsSale = x.IsSale,
            PriceSale = x.PriceSale,
            IsAvailable = x.IsAvailable,
            Price = x.Price,
            Description = x.Description,
            LinkImage = x.LinkImage,
            CategoryID = x.CategoryID,
            IsActive = true,
        }).FirstOrDefaultAsync();
        if (product != null)
        {
            var sizes = await item1.Where(x => x.sp.ProductID == product.ID).Select(x => x.s).IgnoreAutoIncludes().ToListAsync();
            var toppings = await item2.Where(x => x.tp.ProductID == product.ID).Select(x => x.t).IgnoreAutoIncludes().ToListAsync();
            product.Sizes = sizes.Select(x => _mapper.Map<SizeVm>(x)).ToList();
            product.Toppings = toppings.Select(x => _mapper.Map<ToppingVm>(x)).ToList();
        }

        if (product == null)
            return new Respond<ProductVm>()
            {
                Result = 0,
                Message = "Không tìm thấy sản phẩm",
            };

        return new Respond<ProductVm>()
        {
            Result = 1,
            Message = "Thành công",
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
                Result = 0,
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
            productImage.ImagePath = Upload + "/" + nameFile;
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
            productImage.ImagePath = Upload + "/" + namefile;
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

    public async Task<Respond<PagedList<Size>>> GetAllSize()
    {
        var sizes = await _context.Sizes.ToListAsync();
        if (sizes == null)
        {
            return new Respond<PagedList<Size>>()
            {
                Data = null,
                Result = -1,
                Message = "Không có dữ liệu",
            };
        }
        var sizeVms = sizes.Select(x => _mapper.Map<SizeVm>(x)).ToList();
        var pagedResult = new PagedList<Size>()
        {
            TotalRecord = sizeVms.Count,
            PageSize = sizeVms.Count,
            CurrentPage = 1,
            TotalPages = sizeVms.Count,
            Items = sizes,
        };
        return new Respond<PagedList<Size>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<MessageResult> UpdateSize(string sizeID, Size request)
    {
        var size = await _context.Sizes.FindAsync(sizeID);
        if (size == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy topping",
            };
        if (request.SubPrice != size.SubPrice)
        {
            var history = new HistoryPriceUpdate()
            {
                ID = Guid.NewGuid().ToString(),
                Name = request.Name,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.UtcNow,
                StartDate = size.UpdateDate ?? DateTime.Now,//ngày cũ
                EndDate = DateTime.Now,
                Description = $"Cập nhật giá ngày: { DateTime.Now} cho kichs thuocw: {request.Name}",
                PriceOld = size.SubPrice,
                PriceNew = request.SubPrice,
                UserCreateID = _userID,
                HistoryType = HistoryType.Size,
                SizeID = sizeID,
            };
            _context.HistoryPriceUpdates.Add(history);
        }

        size.Name = request.Name;
        size.SubPrice = request.SubPrice;
        size.UpdateDate = DateTime.Now;
        size.UserUpdateID = _userID;
        _context.Sizes.Update(size);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> CreateSize(Size size)
    {
        size.ID = Guid.NewGuid().ToString();
        size.CreateDate = DateTime.Now;
        size.UpdateDate = DateTime.Now;
        size.UserCreateID = _userID;
        size.UserUpdateID = _userID;
        _context.Sizes.Add(size);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo mới thành công",
        };
    }

    public async Task<MessageResult> DeleteSize(string sizeID)
    {
        var size = await _context.Sizes.FindAsync(sizeID);
        if (size == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };
        _context.Sizes.Remove(size);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<Respond<PagedList<Topping>>> GetAllTopping()
    {
        var toppings = await _context.Toppings.ToListAsync();
        if (toppings == null)
        {
            return new Respond<PagedList<Topping>>()
            {
                Data = null,
                Result = -1,
                Message = "Không có dữ liệu",
            };
        }
        var pagedResult = new PagedList<Topping>()
        {
            TotalRecord = toppings.Count,
            PageSize = toppings.Count,
            CurrentPage = 1,
            TotalPages = toppings.Count,
            Items = toppings,
        };
        return new Respond<PagedList<Topping>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<MessageResult> UpdateTopping(string toppingID, Topping request)
    {
        var topping = await _context.Toppings.FindAsync(toppingID);
        if (topping == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy topping",
            };
        if (request.SubPrice != topping.SubPrice)
        {
            var history = new HistoryPriceUpdate()
            {
                ID = Guid.NewGuid().ToString(),
                Name = request.Name,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.UtcNow,
                StartDate = topping.UpdateDate ?? DateTime.Now,//ngày cũ
                EndDate = DateTime.Now,
                Description = $"Cập nhật giá ngày: { DateTime.Now} cho topping: {request.Name}",
                PriceOld = topping.SubPrice,
                PriceNew = request.SubPrice,
                UserCreateID = _userID,
                HistoryType = HistoryType.Topping,
                ToppingID = toppingID,
            };
            _context.HistoryPriceUpdates.Add(history);
        }

        topping.Name = request.Name;
        topping.Description = request.Description;
        topping.SubPrice = request.SubPrice;
        topping.UpdateDate = DateTime.Now;
        topping.UserCreateID = _userID;
        topping.UserUpdateID = _userID;
        _context.Toppings.Update(topping);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> CreateTopping(Topping topping)
    {
        topping.ID = Guid.NewGuid().ToString();
        topping.CreateDate = DateTime.Now;
        topping.UpdateDate = DateTime.Now;
        topping.UserCreateID = _userID;
        topping.UserUpdateID = _userID;
        _context.Toppings.Add(topping);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo mới thành công",
        };
    }

    public async Task<MessageResult> DeleteTopping(string toppingID)
    {
        var topping = await _context.Toppings.FindAsync(toppingID);
        if (topping == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy topping",
            };
        _context.Toppings.Remove(topping);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }

    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), Upload + "/" + fileName);
        return fileName;
    }

    public async Task<Respond<PagedList<HistoryPriceUpdate>>> GetHistoryPriceByID(string ID, Search request)
    {

        var query = from c in _context.HistoryPriceUpdates where c.ProductID == ID || c.SizeID == ID || c.ToppingID == ID select c;
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.Name!.Contains(request.Name));
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<HistoryPriceUpdate>();
        if (request.IsPging)
        {
            data = await query.Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
        {
            data = await query.Select(x => x).ToListAsync();
        }

        var pagedResult = new PagedList<HistoryPriceUpdate>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<HistoryPriceUpdate>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
}
