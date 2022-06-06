using System.ComponentModel;

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

public enum MeasureType
{
    Mass,//Khối lượng g, kg,...
    Volume,// thể tích ml, l,...
}
public enum StandardUnitType
{
    ml = 1,
    g = 2,
}
public enum BeanType
{
    New = 1,
    Bronze = 2,
    Silver = 3,
    Gold = 4,
    Diamond = 5,
}
public enum Gender
{
    Male,
    Female,
}

public enum SugarType
{
    [Description("0%")]
    ZeroPercent = 1,
    [Description("25%")]
    TwentyFivePercent = 2,
    [Description("50%")]
    FiftyPercent = 3,
    [Description("75%")]
    SeventyFivePercent = 4,
    [Description("100%")]
    OneHundredPercent = 5,
}

public enum IcedType
{
    [Description("0%")]
    ZeroPercent = 1,
    [Description("25%")]
    TwentyFivePercent = 2,
    [Description("50%")]
    FiftyPercent = 3,
    [Description("75%")]
    SeventyFivePercent = 4,
    [Description("100%")]
    OneHundredPercent = 5,
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
    Drink,//đồ uống
    Food,// đồ ăn
    InDay,// đồ dùng trong ngày
    Other
}

public enum OrderType
{
    InPlace,//Tại chỗ
    TakeAway,// Mang về
    Shipping, // Vận chuyển
}
