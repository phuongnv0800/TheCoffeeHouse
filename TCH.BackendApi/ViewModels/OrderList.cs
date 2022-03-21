using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCH.BackendApi.ViewModels
{
    public class OrderList
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public decimal SubTotal { get; set; }
    }
}
