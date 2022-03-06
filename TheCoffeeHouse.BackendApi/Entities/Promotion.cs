using TheCoffeeHouse.Utilities.Enum;

namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Promotion
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string PromotionFoodAmountID { get; set; }
        public string PromotionFoodPercentID { get; set; }
        public string PromotionSumAmountID { get; set; }
        public string PromotionSumPercentID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public TypePromotion TypePromotion { get; set; }
        public ObjectPromotion ObjectPromotion { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
    }
}
