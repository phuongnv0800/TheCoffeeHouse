using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.Utilities.Claims;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Models.DataManager;

public class CustomerManager : IDisposable, ICustomerRepository
{
    private readonly APIContext _context;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;
    private const string USER_CONTENT_FOLDER_NAME = "customers";
    public CustomerManager(APIContext context, 
        IMapper mapper, 
        IHttpContextAccessor httpContextAccessor,
        IStorageService storageService)
    {
        _context = context;
        _mapper = mapper;
        _storageService = storageService;
        _httpContextAccessor = httpContextAccessor;
        UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
    }

    public async Task<MessageResult> Create(CustomerRequest request)
    {
        var customer = new Customer()
        {
            ID = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            Gender = request.Gender,
            DateOfBirth  = request.DateOfBirth,
            MemberID = "",
            Point  = 0,
            BeanID = request.MemberTypeID,
        };
        if (request.File != null)
        {
            customer.Avatar = await SaveFileIFormFile(request.File);
        }
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }
    public async Task<MessageResult> Delete(string id)
    {
        var category = await _context.Customers.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.Customers.Remove(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }
    public async Task<Respond<Customer>> GetByID(string id)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(x => x.ID == id);
        if (result == null)
            return new Respond<Customer>()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };

        return new Respond<Customer>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }
    public async Task<MessageResult> Update(string id, CustomerRequest request)
    {
        var result = await _context.Customers.FindAsync(id);
        if (result == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };
        result.UpdateDate = DateTime.Now;
        result.FullName = request.FullName ?? result.FullName;
        result.Email = request.Email ?? result.Email;
        result.Address = request.Address ?? result.Address;
        result.Gender = request.Gender;
        result.DateOfBirth = request.DateOfBirth;
        result.BeanID = request.MemberTypeID;
        _context.Customers.Update(result);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }
    public async Task<Respond<PagedList<Customer>>> GetAll(Search request)
    {
        var query = from c in _context.Customers select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.FullName.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<Customer>();
        if (request.IsPging)
            data = await query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        else
             data = await query.Select(x => x).ToListAsync();
            
        var pagedResult = new PagedList<Customer>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Customer>>()
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }
    public async Task<MessageResult> CreateMemberType(MemberTypeRequest request)
    {
        var customer = new Bean()
        {
            ID = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Name = request.Name,
            MinPoint = request.MinPoint,
            MaxPoint = request.MaxPoint,
            ConversationMoney = request.ConversationMoney,
            ConversationPoint = request.ConversationPoint,
            ConversionForm = request.ConversionForm,
            Description = request.Description,
        };
        _context.Beans.Add(customer);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }
    public async Task<MessageResult> DeleteMemberType(string id)
    {
        var category = await _context.Beans.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.Beans.Remove(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }
    public async Task<Respond<Bean>> GetMemberTypeByID(string id)
    {
        var result = await _context.Beans.FirstOrDefaultAsync(x => x.ID == id);
        if (result == null)
            return new Respond<Bean>()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };

        return new Respond<Bean>()
        {
            Result = 1,
            Message = "Thành công",
            Data = result,
        };
    }
    public async Task<MessageResult> UpdateMemberType(string id, MemberTypeRequest request)
    {
        var result = await _context.Beans.FindAsync(id);
        if (result == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };
        result.UpdateDate = DateTime.Now;
        result.Name = request.Name;
        result.MinPoint = request.MinPoint;
        result.MaxPoint = request.MaxPoint;
        result.ConversationMoney = request.ConversationMoney;
        result.ConversationPoint = request.ConversationPoint;
        result.ConversionForm = request.ConversionForm;
        result.Description = request.Description;
        _context.Beans.Update(result);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }
    public async Task<Respond<PagedList<Bean>>> GetAllMemberType(Search request)
    {
        var query = from c in _context.Beans select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        var data = new List<Bean>();
        if (request.IsPging)
            data = await query
                .Select(x => x)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        else
             data = await query.Select(x => x).ToListAsync();
            
        var pagedResult = new PagedList<Bean>()
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Bean>>()
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
        await _storageService.SaveFileAsync(file.OpenReadStream(), USER_CONTENT_FOLDER_NAME + "/" + fileName);
        return fileName;
    }
    
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}