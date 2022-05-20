namespace TCH.ViewModel.SubModels;

public class MemberTypeRequest
{
    public string Name { get; set; }
    public int MinPoint { get; set; }
    public int MaxPoint { get; set; }
    public double ConversationMoney { get; set; }
    public int ConversationPoint { get; set; }
    public string? ConversionForm { get; set; }
    public string? Description { get; set; }
}