using System;

namespace TCH.BackendApi.ViewModels
{
    public class CartRequest
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { set; get; }
    }
}
