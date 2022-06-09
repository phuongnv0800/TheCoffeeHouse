namespace TCH.Data.Entities;

public class Branch
{
    public string BranchID { get; set; }
    public string Name { get; set; }
    public string City { get; set; } = "Hải Phòng";
    public string District { get; set; }
    public string Adderss { get; set; }
    public string? Email { get; set; }
    public string? LinkImage { get; set; }
    //invoice 
    //public IFormFile ImageUpload { get; set; }
    public string? LogoInvoice { get; set; }
    public string? RestaurantName { get; set; }
    public string? TitleInvoice { get; set; }
    public string? TitleInventory { get; set; }
    public string Phone { get; set; }
    public string? Headercontent { get; set; }
    public int LayoutType { get; set; } = 0;
    public string? Footercontent { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }

    public virtual ICollection<Menu> Menus { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

    public virtual ICollection<AppUser> Users { get; set; }

    public virtual ICollection<Report> Reports { get; set; }

    public virtual ICollection<StockMaterial> StockMaterials{ get; set; }
}
