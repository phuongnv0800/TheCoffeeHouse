using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.Data.Entities;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Searchs;
using TCH.ViewModel.SubModels;
using TCH.Utilities.Roles;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productService, ILogger<ProductsController> logger)
    {
        _productRepository = productService;
        _logger = logger;
    }
    [HttpGet("branch/{branchID}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllByBranchID(string branchID, [FromQuery] Search request)
    {
        try
        {
            var products = await _productRepository.GetAllByBranchID(branchID, request);
            return Ok(products);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] Search request)
    {
        try
        {
            var products = await _productRepository.GetAll(request: request);
            return Ok(products);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpGet("category/{categoryID}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(string categoryID, [FromQuery] Search request)
    {
        try
        {
            var products = await _productRepository.GetProductByCategoryID(categoryID: categoryID, request: request);
            return Ok(products);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    //https://localhost:port/product/1
    [HttpGet("{productId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(string productId)
    {
        try
        {
            var product = await _productRepository.GetById(productId);
            return Ok(product);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    //https://localhost:port/product/1
    [Authorize(Roles = Permission.Branch)]
    [HttpPost]
    [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
    public async Task<IActionResult> Create([FromForm] ProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productRepository.Create(request);
            return Ok(productId);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPut("{productID}")]
    public async Task<IActionResult> Update(string productID, [FromForm] ProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productRepository.Update(productID, request);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(string productId)
    {
        try
        {
            var result = await _productRepository.Delete(productId);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPut("add-product-to-category/{productID}/{categoryId}")]
    public async Task<IActionResult> CategoryAssign(string productID, string categoryId)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productRepository.CategoryAssign(productID, categoryId);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [HttpGet("{productId}/images")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllImage(string productId)
    {
        try
        {
            var images = await _productRepository.GetAllImages(productId);
            if (images == null)
                return BadRequest("Can't not find images");
            return Ok(images);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpGet("history/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetHistoryByID(string id, Search search)
    {
        try
        {
            var images = await _productRepository.GetHistoryPriceByID(id, search);
            return Ok(images);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpGet("image/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetImage(string id)
    {
        try
        {
            var image = await _productRepository.GetImageById(id);
            if (image == null)
                return BadRequest("Can't not find image");
            return Ok(image);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPost("{productID}/image")]
    [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
    public async Task<IActionResult> CreateImage(string productID, [FromForm] ProductImageRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productRepository.AddImage(productID, request);
            return Ok(imageId);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPut("{productID}/image")]
    [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
    public async Task<IActionResult> UpdateImage(string productID, [FromForm] ProductImageRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productRepository.UpdateImage(productID, request);
            return Ok(imageId);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [Authorize(Roles = Permission.Branch)]
    [HttpDelete("image/{id}")]
    public async Task<IActionResult> DeleteImage(string id)
    {
        try
        {
            var result = await _productRepository.RemoveImage(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpGet("size")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllSize()
    {
        try
        {
            var result = await _productRepository.GetAllSize();
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [Authorize(Roles = Permission.Branch)]
    [HttpPost("size")]
    public async Task<IActionResult> CreateSize(Size request)
    {
        try
        {
            var result = await _productRepository.CreateSize(request);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPut("size/{id}")]
    public async Task<IActionResult> UpdateSize(string id, Size size)
    {
        try
        {
            var result = await _productRepository.UpdateSize(id, size);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpDelete("size/{id}")]
    public async Task<IActionResult> DeleteSize(string id)
    {
        try
        {
            var result = await _productRepository.DeleteSize(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("topping")]
    public async Task<IActionResult> GetAllTopping(string id)
    {
        try
        {
            var result = await _productRepository.GetAllTopping();
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPost("topping")]
    public async Task<IActionResult> CreateTopping(Topping request)
    {
        try
        {
            var result = await _productRepository.CreateTopping(request);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpPut("topping/{id}")]
    public async Task<IActionResult> UpdateTopping(string id, Topping size)
    {
        try
        {
            var result = await _productRepository.UpdateTopping(id, size);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpDelete("topping/{id}")]
    public async Task<IActionResult> DeleteTopping(string id)
    {
        try
        {
            var result = await _productRepository.DeleteTopping(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { result = -1, message = e.Message });
        }
        catch (Exception e)
        {

            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
}
