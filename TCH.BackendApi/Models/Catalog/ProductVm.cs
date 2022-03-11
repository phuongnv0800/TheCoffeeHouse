using Microsoft.AspNetCore.Http;

namespace TCH.ViewModel.Catalog
{
    public class ProductVm
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public decimal Price { set; get; }
        public int Quantity { get; set; }

        public float Rating { get; set; }
        public int RatingCount { get; set; }
        public int Discount { get; set; }
        
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}