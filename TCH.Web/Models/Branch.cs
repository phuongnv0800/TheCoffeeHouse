using System.Collections.Generic;

namespace TCH.Web.Models
{
    public class Branch
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string? Area { get; set; }
        public string City { get; set; }
        public string? Email { get; set; }
        public string District { get; set; }
        public string Adderss { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? UserCreateID { get; set; }
        public string? UserUpdateID { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<UserBranch> UserBranches { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
