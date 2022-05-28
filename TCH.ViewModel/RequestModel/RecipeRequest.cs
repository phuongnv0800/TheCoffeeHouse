using TCH.Utilities.Enum;

namespace TCH.ViewModel.RequestModel;

public  class RecipeRequest
{
    public string ProductID { get; set; }
    
    public string SizeID { get; set; }

    public string MaterialID { get; set; }

    public double Weight { get; set; }
    // tên đơn vị (gam, mililit)
    public string Unit { get; set; }
    //kiểu đơn vị (g, ml)
    public StandardUnitType StandardUnitType { get; set; }
}
