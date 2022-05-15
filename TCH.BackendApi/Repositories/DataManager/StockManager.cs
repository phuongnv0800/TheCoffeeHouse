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
    public async Task<Respond<PagedList<StockMaterial>>> GetAllStockByBranchID(string branchID, Search request)
    {
        var query = from c in _context.StockMaterials where c.BranchID == branchID select c;
        if (request.StartDate != null)
            query = query.Where(x => DateTime.Compare(x.BeginDate, (DateTime)request.StartDate) < 0);

        //paging
        int totalRow = await query.CountAsync();
        List<StockMaterial> data = new List<StockMaterial>();
        if (request.IsPging == true)
        {
            data = await query
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .ToListAsync();
        }
        else
            data = await query.ToListAsync();

        // select
        var pagedResult = new PagedList<StockMaterial>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<StockMaterial>>()
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
