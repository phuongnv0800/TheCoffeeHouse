using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels
{
    public class SizeVm
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SubPrice { get; set; }
        public SizeType SizeType { get; set; }
    }
}
