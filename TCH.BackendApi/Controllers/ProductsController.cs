using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productService, ILogger<ProductsController> logger)
        {
            _productRepository = productService;
            this._logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] Search request)
        {
            try
            {
                var products = await _productRepository.GetAll(request);
                return Ok(products);
            }
            catch (CustomException e)
            {
                return BadRequest(new { result = -1, message = e.Message });
            }
            catch (Exception e)
            {
                SQLExceptionFilter.AddFileCheckSQL(e);
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }

        //https://localhost:port/product/1
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }

        [HttpPut("{productID}")]
        public async Task<IActionResult> Update(string productID, [FromBody] ProductVm request)
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }

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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }

        [HttpPut("{id}/categories")]
        public async Task<IActionResult> CategoryAssign(string id, [FromBody] string categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _productRepository.CategoryAssign(id, categoryId);
                return Ok(result);
            }
            catch (CustomException e)
            {
                return BadRequest(new { result = -1, message = e.Message });
            }
            catch (Exception e)
            {
                SQLExceptionFilter.AddFileCheckSQL(e);
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
                SQLExceptionFilter.AddFileCheckSQL(e);
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }

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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }
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
                SQLExceptionFilter.AddFileCheckSQL(e);
                _logger.LogError(e.ToString());
                return BadRequest(new { result = -2, message = e.Message });
            }
        }
    }
}
