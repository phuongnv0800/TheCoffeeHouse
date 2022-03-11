using Microsoft.AspNetCore.Identity;

namespace TCH.BackendApi.Entities
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
