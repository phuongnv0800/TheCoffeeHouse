using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.System.Roles;

namespace TCH.BackendApi.Service.System
{
    public class RoleManager : IRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
       
        public async Task<List<RoleVm>> GetAll()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleVm()
            {
                ID = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
            return roles;
        }
    }
}
