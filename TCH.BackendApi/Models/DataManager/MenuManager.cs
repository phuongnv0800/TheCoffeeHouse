using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Models.DataManager;

public class MenuManager : IDisposable, IMenuRepository
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;

    public MenuManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<MessageResult> Create(string branchID, MenuRequest request)
    {
        var branch = await _context.Branches.FindAsync(branchID);
        if (branch == null)
        {
            return new MessageResult()
            {
                Message = "Không tồn tại chi nhánh",
                Result = 0,
            };
        }
        var menu = new Menu()
        {
            ID = Guid.NewGuid().ToString(),
            Name = request.Name,
            UpdateDate = DateTime.Now,
            CreateDate = DateTime.Now,
            Branch = branch,
            BranchID = branchID,
            Description = request.Description,
        };
        _context.Menus.Add(menu);
        var products = await _context.Products.ToListAsync();
        foreach (var item in products)
        {
            var productInMenu = new ProductInMenu()
            {
                IsActive = true,
                Product = item,
                ProductID = item.ID,
                Menu = menu,
                MenuID = menu.ID
            };
            _context.ProductInMenus.Add(productInMenu);
        }
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Thành công",
            Result = 1,
        };
    }
    public async Task<Respond<List<Menu>>> GetMenu(string branchID)
    {
        var menu = await _context.Menus.Where(x => x.BranchID == branchID).ToListAsync();
        if (menu == null)
        {
            return new Respond<List<Menu>>()
            {
                Message = "Không tồn tại",
                Result = 0,
                Data = null,
            };
        }
        return new Respond<List<Menu>>()
        {
            Message = "Thành công",
            Result = 1,
            Data = menu,
        };
    }
    public async Task<MessageResult> Update(string menuID, MenuRequest request)
    {
        var menu = await _context.Menus.FindAsync(menuID);
        if (menu == null)
        {
            return new MessageResult()
            {
                Message = "Không tồn tại",
                Result = 0,
            };
        }
        menu.Name = request.Name;
        menu.UpdateDate = DateTime.Now;
        _context.Menus.Update(menu);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Message = "Thành công",
            Result = 1,
        };
    }
    public async Task<MessageResult> DeactiveProductInMenu(string menuID, string productID)
    {
        var entity = await _context.ProductInMenus.FirstOrDefaultAsync(x => x.MenuID == menuID && x.ProductID == productID);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        entity.IsActive = false;
        _context.ProductInMenus.Update(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> ActiveProductInMenu(string menuID, string productID)
    {
        var entity = await _context.ProductInMenus.FirstOrDefaultAsync(x => x.MenuID == menuID && x.ProductID == productID);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        entity.IsActive = true;
        _context.ProductInMenus.Update(entity);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> Delete(string id)
    {
        var entity = await _context.Menus.FindAsync(id);
        if (entity == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        var productInMenus = await _context.ProductInMenus.Where(x => x.MenuID == id).ToListAsync();
        foreach (var menu in productInMenus)
        {
            _context.ProductInMenus.Remove(menu);
        }
        _context.Menus.Remove(entity);
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
}
