using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class RoleManager : IRoleRepository, IDisposable
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleManager(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
    public async Task<Respond<PagedList<RoleVm>>> GetAll(Search request)
    {

        var query = _roleManager.Roles;
        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }
        //paging
        int totalRow = await query.CountAsync();
        if (request.IsPging == true)
        {
            var data = await query
            .Select(
                x => new RoleVm()
                {
                    ID = x.Id,
                    Name = x.Name,
                    Description = x.Description ?? "",
                }
            )
            //.OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<RoleVm>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRow / request.PageSize),
                Items = data,
            };
            return new Respond<PagedList<RoleVm>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
        else
        {
            var data = await query
            .Select(
                x => new RoleVm()
                {
                    ID = x.Id,
                    Name = x.Name,
                    Description = x.Description ?? "",
                }
            )
            //.OrderBy(x => x.Id)
            .ToListAsync();
            // select
            var pagedResult = new PagedList<RoleVm>()
            {
                TotalRecord = totalRow,
                PageSize = totalRow,
                CurrentPage = 1,
                TotalPages = 1,
                Items = data,
            };
            return new Respond<PagedList<RoleVm>>()
            {
                Data = pagedResult,
                Result = 1,
                Message = "Thành công",
            };
        }
    }
}
