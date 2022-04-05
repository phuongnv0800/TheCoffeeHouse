﻿namespace TCH.BackendApi.Entities;

public class Branch
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string City { get; set; } = "Hải Phòng";
    public string? Email { get; set; }
    public string District { get; set; }
    public string Adderss { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public ICollection<Menu> Menus { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<AppUser> Users { get; set; }
    public Stock Stock { get; set; }
}
