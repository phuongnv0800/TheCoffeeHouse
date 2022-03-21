using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCH.BackendApi.ViewModels
{
    public class ProductImageRequest
    {
        public bool IsShowHome { get; set; }
        public string Caption { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
