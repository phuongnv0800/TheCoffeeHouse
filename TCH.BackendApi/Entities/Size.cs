using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities
{
    public class Size
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SubPrice { get; set; }
        public SizeType SizeType { get; set; }
    }
}
