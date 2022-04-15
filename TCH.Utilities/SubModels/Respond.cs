namespace TCH.Utilities.SubModels;

public class Respond<T> where T : class
{
    public int Result { get; set; }
    public string Message { get; set; } = "";
    public T Data { get; set; }
}

