﻿namespace TCH.WebServer.Models
{
    public class ImportReport
    {
        public string ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Supplier { get; set; }
        public string StockName { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<ImportMaterial> ImportMaterials { get; set; }
    }
}