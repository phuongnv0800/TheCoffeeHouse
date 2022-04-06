using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TCH.BackendApi.Entities;

public class InvoiceLayout
{
    [Key]
    public string ID { get; set; }
    [NotMapped, JsonIgnore]
    public IFormFile ImageUpload { get; set; }
    public string? LogoInvoice { get; set; }
    public string? RestaurantName { get; set; }
    public string? TitleInvoice { get; set; }
    public string? TitleInventory { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? Headercontent { get; set; }
    public int LayoutType { get; set; }
    public string? Footercontent { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UserCreateID { get; set; }
    public string UserUpdateID { get; set; }
    [Required]
    public string BranchID { get; set; }
}