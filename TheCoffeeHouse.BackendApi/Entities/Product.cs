using TheCoffeeHouse.Utilities.Enum;

namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Product
    {
        public string ID { get; set; }
        public string CategoryID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public TypeProduct TypeProduct { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public string Formula { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
    }
}
