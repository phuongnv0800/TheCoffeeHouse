using Microsoft.AspNetCore.Http;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public string? Formula { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string Unit { get; set; }
        public string CategoryID { get; set; }

        //public IFormFile ImagePath { get; set; }
    }
}
