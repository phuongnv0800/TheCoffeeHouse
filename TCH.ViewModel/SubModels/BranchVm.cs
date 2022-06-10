namespace TCH.ViewModel.SubModels;

public class BranchVm
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string City { get; set; } = "Hải Phòng";
    public string District { get; set; }
    public string Adderss { get; set; }
    public string? Email { get; set; }
    public string? LinkImage { get; set; }
    public string Phone { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}