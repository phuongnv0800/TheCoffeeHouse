namespace TCH.BackendApi.Entities;

public class Branch
{
    public string ID { get; set; }

    public string Name { get; set; }

    public string City { get; set; } = "Hải Phòng";

    public string District { get; set; }

    public string Adderss { get; set; }

    public string? Email { get; set; }
    //invoice 
    //public IFormFile ImageUpload { get; set; }
    public string? LogoInvoice { get; set; }
    public string? RestaurantName { get; set; }
    public string? TitleInvoice { get; set; }
    public string? TitleInventory { get; set; }
    public string Phone { get; set; }
    public string? Headercontent { get; set; }
    public int LayoutType { get; set; }
    public string? Footercontent { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }

    public ICollection<Menu> Menus { get; set; }

    public ICollection<Order> Orders { get; set; }

    public ICollection<AppUser> Users { get; set; }

    public ICollection<Report> Reports { get; set; }

    public ICollection<StockMaterial> StockMaterials{ get; set; }
}
