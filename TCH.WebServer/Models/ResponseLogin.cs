namespace TCH.WebServer.Models
{
    public class ResponseLogin<T> where T : class
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
