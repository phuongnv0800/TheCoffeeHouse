using Microsoft.AspNetCore.Http;

namespace TCH.ViewModel.RequestModel;

public class BranchRequest
{
    public string Name { get; set; }
    public string? Area { get; set; }
    public string City { get; set; }
    public string? Email { get; set; }
    public string District { get; set; }
    public string Adderss { get; set; }
    public IFormFile? ImageUpload { get; set; }
}
