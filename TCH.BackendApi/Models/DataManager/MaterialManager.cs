using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Config;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataRepository;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Models.DataManager;

public class MaterialManager : IMaterialRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? UserID;

    public MaterialManager(APIContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
    }
    public async Task<MessageResult> CreateMaterial(MaterialRequest request)
    {
        var material = new Material()
        {
            ID = Guid.NewGuid().ToString(),
            Name = request.Name,
            Description = request.Description,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            MaterialTypeID = request.MaterialTypeID,
        };
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> CreateMaterialType(MaterialTypeRequest request)
    {
        var materialType = new MaterialType()
        {
            ID = Guid.NewGuid().ToString(),
            Name = request.Name,
            Description = request.Description,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };
        _context.MaterialTypes.Add(materialType);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Tạo thành công",
        };
    }

    public async Task<MessageResult> DeleteMaterial(string id)
    {
        var category = await _context.Materials.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.Materials.Remove(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<MessageResult> DeleteMaterialType(string id)
    {
        var category = await _context.MaterialTypes.FindAsync(id);
        if (category == null)
        {
            return new MessageResult()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };
        }
        _context.MaterialTypes.Remove(category);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Xoá thành công",
        };
    }

    public async Task<Respond<PagedList<Material>>> GetAllMaterial(Search request)
    {
        var query = from c in _context.Materials select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<Material>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<Material>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(x => x)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<Material>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<Material>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }

    public async Task<Respond<PagedList<MaterialType>>> GetAllMaterialType(Search request)
    {
        var query = from c in _context.MaterialTypes select c;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(x => x)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<MaterialType>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<MaterialType>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(x => x)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<MaterialType>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<MaterialType>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }

    public async Task<Respond<Material>> GetMaterialByID(string id)
    {
        var product = await _context.Materials.FirstOrDefaultAsync(x => x.ID == id);
        if (product == null)
            return new Respond<Material>()
            {
                Result = 0,
                Message = "Không tìm thấy",
            };

        return new Respond<Material>()
        {
            Result = 1,
            Message = "Thành công",
            Data = product,
        };
    }

    public async Task<Respond<MaterialType>> GetMaterialTypeByID(string id)
    {
        var product = await _context.MaterialTypes.FirstOrDefaultAsync(x => x.ID == id);
        if (product == null)
            return new Respond<MaterialType>()
            {
                Result = -1,
                Message = "Không tìm thấy",
            };

        return new Respond<MaterialType>()
        {
            Result = 1,
            Message = "Thành công",
            Data = product,
        };
    }

    public async Task<MessageResult> UpdateMaterial(string id, MaterialRequest request)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material == null)
            return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy  ",
            };
        material.Name = request.Name;
        material.Description = request.Description;
        material.UpdateDate = DateTime.Now;
        material.Description = request.Description;
        material.MaterialTypeID = request.MaterialTypeID;
        _context.Materials.Update(material);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }

    public async Task<MessageResult> UpdateMaterialType(string id, MaterialTypeRequest request)
    {
        var materialType = await _context.MaterialTypes.FindAsync(id);
        if (materialType == null)
           return new MessageResult()
            {
                Result = -1,
                Message = "Không tìm thấy ",
            };
        materialType.Name = request.Name;
        materialType.Description = request.Description;
        materialType.UpdateDate = DateTime.Now;
        _context.MaterialTypes.Update(materialType);
        await _context.SaveChangesAsync();
        return new MessageResult()
        {
            Result = 1,
            Message = "Cập nhật thành công",
        };
    }
    
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}
