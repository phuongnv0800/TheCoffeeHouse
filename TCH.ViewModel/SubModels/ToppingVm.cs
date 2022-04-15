using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCH.ViewModel.SubModels;

public class ToppingVm
{
    public string ID { get; set; }
    public string Name { get; set; }
    public double SubPrice { get; set; }
    public string? Description { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
