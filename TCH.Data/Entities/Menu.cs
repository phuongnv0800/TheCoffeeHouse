using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Data.Entities;

public class Menu
{
    [Key]
    public string MenuID { get; set; }
    [Required, MaxLength(255)]
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }

    [ForeignKey("Branch")]
    public string BranchID { get; set; }
    public Branch Branch { get; set; }

    public virtual IEnumerable<Category> Categories { get; set; }
    //public ICollection<ProductInMenu> ProductInMenus { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
