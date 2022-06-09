namespace TCH.Data.Entities;

public class Topping
{
    public string ToppingID { get; set; }
    public string Name { get; set; }
    public double SubPrice { get; set; }
    public string? Description { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }
    public virtual ICollection<ToppingInProduct> ToppingInProducts { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public virtual ICollection<HistoryPriceUpdate> HistoryPriceUpdates { get; set; }
}
