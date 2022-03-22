using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels
{
    public class OrderVm
    {
        public Guid UserId { get; set; }
        public int Id { set; get; }
        public DateTime DateCreated { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipPhone { set; get; }
        public OrderStatus Status { set; get; }

        public List<OrderList> OrderLists { get; set; }
    }
}
