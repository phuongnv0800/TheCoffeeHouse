using Microsoft.AspNetCore.Http;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels
{
    public class ProductVm
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; } = ProductType.Drink;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsSale { get; set; } = false;
        public double PriceSale { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string LinkImage { get; set; }
        public string CategoryID { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Size> Sizes { get; set; }
        public List<Topping> Toppings { get; set; }
    }
}