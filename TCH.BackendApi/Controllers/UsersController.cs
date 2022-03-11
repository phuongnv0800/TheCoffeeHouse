using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.ViewModel.System.Users;
using System;
using System.Threading.Tasks;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Common;

namespace TCH.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userService;
        public UsersController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Authenicate(request);
            if (result.Result != 1)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (result.Result != 1)
                return BadRequest(result);
            return Ok(result);
        }

        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingRequest request)
        {
            var products = await _userService.GetUsersPaging(request);
            return Ok(products);
        }

        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, request);
            if (result.Result != 1)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
        [HttpGet("name/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(string id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);
            if (result.Result != 1)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            return Ok();
        }
    }
}
