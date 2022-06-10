namespace TCH.ViewModel.SubModels;

public class MoneyByDay
{
    public List<MoneyByDayDetail> MoneyByDayDetails { get; set; }
    public BranchVm? Branch { get; set; }
    public double TotalAmount { get; set; }
}