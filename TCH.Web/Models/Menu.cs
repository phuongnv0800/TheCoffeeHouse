using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Web.Models
{
    public class Menu
    {
        public string ID { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        [ForeignKey("UserCreateID")]
        public string? UserCreateID { get; set; }
        [ForeignKey("UserUpdateID")]
        public string? UserUpdateID { get; set; }
        [ForeignKey("BranchID")]
        public string BranchID { get; set; }
        public Branch Branch { get; set; }
        //public IEnumerable<Category> Categories{ get; set; }

        public ICollection<ProductInMenu> ProductInMenus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
