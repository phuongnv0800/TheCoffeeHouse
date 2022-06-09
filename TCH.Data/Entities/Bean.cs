using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class Bean
{
    public string BeanID { get; set; }
    public string Name { get; set; }
    public BeanType Code { get; set; }
    public int MinPoint { get; set; }
    public int MaxPoint { get; set; }
    public double ConversationMoney { get; set; }
    public int ConversationPoint { get; set; }
    public string? ConversionForm { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }
}
