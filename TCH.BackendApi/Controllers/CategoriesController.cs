//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class CategoriesController : ControllerBase
//    {
//        private readonly ICategoryService _categoryService;

//        public CategoriesController(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll()
//        {
//            var result = await _categoryService.GetAll();
//            if (result == null)
//                return BadRequest("Can't not find category");
//            return Ok(result);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] string name)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            var result = await _categoryService.Create(name);
//            if (result == 0)
//                BadRequest();
//            return Ok(result);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] string name)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            var result = await _categoryService.Update(id, name);
//            if (result == 0)
//                BadRequest();
//            return Ok(result);
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        { 
//            var result = await _categoryService.Delete(id);
//            if (result == 0)
//                BadRequest();
//            return Ok(result);
//        }
//    }
//}
