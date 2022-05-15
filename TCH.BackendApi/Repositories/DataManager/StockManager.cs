using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.Utilities.Claims;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class StockManager : IDisposable, IStockRepository
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private readonly string _accessToken;
    private readonly IMapper _mapper;
    private const string Upload = "branch";
    private readonly IStorageService _storageService;

    public StockManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContext, IStorageService storageService)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _storageService = storageService;
        _mapper = mapper;
        UserID = httpContext != null ? httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value : "";
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }
    public async Task<Respond<PagedList<StockVm>>> GetAllStockByBranchID(string branchID, Search request)
    {
        var query = from c in _context.StockMaterials
                    join b in _context.Branches on c.BranchID equals b.ID
                    join m in _context.Materials on c.MaterialID equals m.ID
                    where c.BranchID == branchID
                    select new { c, b, m };
        if (request.StartDate != null)
            query = query.Where(x => DateTime.Compare(x.c.BeginDate, (DateTime)request.StartDate) < 0);

        //paging
        int totalRow = await query.CountAsync();
        List<StockVm> data = new List<StockVm>();
        if (request.IsPging == true)
        {
            data = await query.Select(x =>
                 new StockVm()
                 {
                     BranchName = x.c.Branch.Name,
                     MaterialName = x.m.Name,
                     Quantity = x.c.Quantity,
                     BeginDate = x.c.BeginDate,
                     ExpirationDate = x.c.ExpirationDate,
                     Status = x.c.Status,
                     PriceOfUnit = x.c.PriceOfUnit,
                     Unit = x.c.Unit,
                     StandardUnit = x.c.StandardUnit,
                     Description = x.c.Description,
                 }
            )
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .ToListAsync();
        }
        else
            data = await query.Select(x =>
                 new StockVm()
                 {
                     BranchName = x.c.Branch.Name,
                     MaterialName = x.m.Name,
                     Quantity = x.c.Quantity,
                     BeginDate = x.c.BeginDate,
                     ExpirationDate = x.c.ExpirationDate,
                     Status = x.c.Status,
                     PriceOfUnit = x.c.PriceOfUnit,
                     Unit = x.c.Unit,
                     StandardUnit = x.c.StandardUnit,
                     Description = x.c.Description,
                 }
            ).ToListAsync();

        // select
        var pagedResult = new PagedList<StockVm>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockVm>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }


    private async Task<string> SaveFileIFormFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), Upload + "/" + fileName);
        return fileName;
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }

}
