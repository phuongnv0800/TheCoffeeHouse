namespace TCH.ViewModel.SubModels;

public class ProductImageVm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public bool IsShowHome { get; set; }
    public long Size { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? Caption { get; set; }
    public int ProductId { get; set; }
}
