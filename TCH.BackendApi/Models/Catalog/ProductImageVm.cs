using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCH.ViewModel.Catalog
{
    public class ProductImageVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool IsShowHome { get; set; }
        public long Size { get; set; }
        public DateTime DateCreated { get; set; }
        public string Caption { get; set; }
        public int ProductId { get; set; }
    }
}
