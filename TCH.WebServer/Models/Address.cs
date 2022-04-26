namespace TCH.WebServer.Models
{
    public class Address
    {
        public string ID { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
