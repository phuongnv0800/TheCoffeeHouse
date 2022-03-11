//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using TCH.BackendApi.Service.Catalog;
//using TCH.ViewModel.Catalog;
//using TCH.ViewModel.Common;
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IProductService _productService;

//        public ProductsController(IProductService productService)
//        {
//            _productService = productService;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll([FromQuery] PagingRequest request)
//        {
//            var products = await _productService.GetAll(request);
//            return Ok(products);
//        }

//        //https://localhost:port/product/1
//        [HttpGet("{productId}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetById(int productId)
//        {
//            var product = await _productService.GetById(productId);
//            if (product == null)
//                return BadRequest("Can't not find product");
//            return Ok(product);
//        }

//        //https://localhost:port/product/1
//        [HttpPost]
//        [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
//        public async Task<IActionResult> Create([FromForm] ProductRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var productId = await _productService.Create(request);
//            if (productId == 0)
//                return BadRequest();
//            return Ok();
//        }

//        [HttpPut("{productId}")]
//        public async Task<IActionResult> Update([FromBody] ProductRequest request)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _productService.Update(request);
//            if (result == 0)
//                return BadRequest();

//            return Ok();
//        }

//        [HttpDelete("{productId}")]
//        public async Task<IActionResult> Delete(int productId)
//        {
//            var result = await _productService.Delete(productId);
//            if (result == 0)
//                return BadRequest();
//            return Ok();
//        }

//        [HttpPut("{id}/categories")]
//        public async Task<IActionResult> CategoryAssign(int id, [FromBody] int categoryId)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _productService.CategoryAssign(id, categoryId);
//            if (!result)
//            {
//                return BadRequest(result);
//            }
//            return Ok();
//        }

//        [HttpGet("{productId}/images")]
//        public async Task<ActionResult> GetAllImage(int productId)
//        {
//            var images = await _productService.GetAllImages(productId);
//            if (images == null)
//                return BadRequest("Can't not find images");
//            return Ok(images);
//        }
//        [HttpGet("image/{id}")]
//        public async Task<ActionResult> GetImage(int id)
//        {
//            var image = await _productService.GetImageById(id);
//            if (image == null)
//                return BadRequest("Can't not find image");
//            return Ok(image);
//        }

//        [HttpPost("{productId}/image")]
//        [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
//        public async Task<IActionResult> CreateImage(int productId,[FromForm] ProductImageRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var imageId = await _productService.AddImage(productId, request);
//            if (imageId == 0)
//                return BadRequest();
//            return Ok();
//        }
//        [HttpPut("{productId}/image")]
//        [Consumes("multipart/form-data")]//nhận kiểu dữ liệu truyền lên là form data
//        public async Task<IActionResult> UpdateImage(int productId, [FromForm] ProductImageRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var imageId = await _productService.UpdateImage(productId, request);
//            if (imageId == 0)
//                return BadRequest();
//            return Ok();
//        }
//        [HttpDelete("image/{id}")]
//        public async Task<IActionResult> DeleteImage(int id)
//        {
//            var result = await _productService.RemoveImage(id);
//            if (result == 0)
//                return BadRequest();
//            return Ok();
//        }
//    }
//}
