using System.ComponentModel.DataAnnotations;
using TCH.BackendApi.Models.Enum;

namespace TCH.ViewModel.Catalog
{
    public class OrderRequest
    {
        public Guid UserId { get; set; }
        public int Id { set; get; }
        [Required(ErrorMessage ="Nhập thông tin người nhận")]
        public string ShipName { set; get; }

        [Required(ErrorMessage = "Nhập địa chỉ người nhận")]
        public string ShipAddress { set; get; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string ShipPhone { set; get; }
        public OrderStatus Status { set; get; }
    }
}
