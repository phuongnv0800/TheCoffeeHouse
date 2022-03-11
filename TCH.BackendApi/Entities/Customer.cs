using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities
{
    public class Customer
    {
        public string ID { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MemberID { get; set; }
        public int Point { get; set; } = 0;
        public string ConversionRate { get; set; }
        public string Avatar { get; set; }

        public string MemberTypeID { get; set; }
        public MemberType MemberType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
