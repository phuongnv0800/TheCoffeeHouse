namespace TCH.BackendApi.Models.Enum
{
    public enum Gender
    {
        Male,
        Female,
    }
    public enum SugarType
    {
        ZeroPercent = 1,
        FiftyPercent = 2,
        SeventyFivePercent = 3,
        OneHundredPercent = 4,
    }
    public enum PromotionObject
    {
        Food = 1, 
        Invoice = 2
    }
    public enum PromotionType
    {
        Percent = 1, 
        Amount = 2,
        SumAmount = 3, 
        SumPercent = 4,
        Gift = 5,
    }
    public enum OrderStatus
    {
        Cancel,
        Pending,
        Finish,
    }
    public enum SizeType
    {
        S,
        M,
        L,
    }
    public enum PaymentType
    {
        Cash,
        Credit,
    }
    public enum ProductType
    {
        Drink,
        Food,
        Other
    }
    public enum OrderType
    {
        CarriedAway,
        ForTopicalUse,
    }
}
