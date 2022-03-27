namespace TCH.BackendApi.Entities;

public class MemberType
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int MinPoint { get; set; }
    public int MaxPoint { get; set; }
    public double ConversationMoney { get; set; }
    public int ConversationPoint { get; set; }
    public string? ConversionForm { get; set; }
    public string? Description { get; set; }
    public string Unit { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }

    //public string UserCreateID { get; set; }
    //public string UserUpdateID { get; set; }

}
