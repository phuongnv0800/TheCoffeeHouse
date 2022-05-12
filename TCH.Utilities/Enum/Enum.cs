namespace TCH.Utilities.Enum;

public enum Status
{
    Deactivate,
    Active,
}

public enum ReportType
{
    Import = 0,
    Export = 1,
    Liquidation = 2,
}
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
    Canceled = 0,
    Open = 1,
    Pending = 2,
    Finished = 3,
    Paid = 4,
}
public enum HistoryType
{
    Product = 1,
    Size = 2,
    Topping = 3,
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
    InPlace,//Tại chỗ
    BringBack,// Mang về
    Shipping, // Vận chuyển
}
