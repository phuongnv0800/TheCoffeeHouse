﻿
using TCH.WebServer.Models.Enum;

namespace TCH.WebServer.Models
{
    public class Size
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SubPrice { get; set; }
        public SizeType SizeType { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
