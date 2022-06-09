using Microsoft.AspNetCore.Http;

namespace TCH.ViewModel.RequestModel;

public class ProductImageRequest
{
    public bool IsShowHome { get; set; }
    public string Caption { get; set; }
    public IFormFile ImageFile { get; set; }
}
