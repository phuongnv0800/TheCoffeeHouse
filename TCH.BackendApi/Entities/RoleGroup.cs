using System.ComponentModel.DataAnnotations;

namespace TCH.BackendApi.Entities;

public class RoleGroup
{
    [Key]
    public string ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<AppRole> Roles { get; set; }
}
