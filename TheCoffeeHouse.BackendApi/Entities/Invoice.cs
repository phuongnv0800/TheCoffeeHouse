using TheCoffeeHouse.Utilities.Enum;

namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Invoice
    {
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public string StoreID { get; set; }
        public string OrderID { get; set; }
        public string Code { get; set; }
        public double SubAmount { get; set; }
        public double TotalAmount { get; set; }
        public double Vat { get; set; }
        public double ReducePromotion { get; set; }
        public double ReduceAmount { get; set; }
        public double CustomerPut { get; set; }
        public double CustomerReceive { get; set; }
        public double ShippingFee { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public string UserCreateID { get; set; }
        public TypePayment TypePayment { get; set; }
    }
}
