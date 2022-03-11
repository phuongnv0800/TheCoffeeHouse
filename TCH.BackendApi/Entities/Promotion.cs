using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities
{
    public class Promotion
    {
        public string ID { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public PromotionType TypePromotion { get; set; }
        public PromotionObject PromotionObject { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string BranchID { get; set; }
        public Branch Branch { get; set; }
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}
