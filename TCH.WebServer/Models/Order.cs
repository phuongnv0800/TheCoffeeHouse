﻿

using TCH.WebServer.Models.Enum;

namespace TCH.WebServer.Models
{
    public class Order
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public double SubAmount { get; set; }
        public double TotalAmount { get; set; }
        public double Vat { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType OrderType{ get; set; }
        public double ReducePromotion { get; set; }
        public double ReduceAmount { get; set; }
        public double CustomerPut { get; set; }
        public double CustomerReceive { get; set; }
        public double ShippingFee { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Description { get; set; } 
        public string? CancellationReason { get; set; }
        public string? UserCreateID { get; set; }
        //public AppUser? AppUser { get; set; }
        public PaymentType PaymentType { get; set; }
        public string? CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public string BranchID { get; set; }
        public Branch Branch { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}