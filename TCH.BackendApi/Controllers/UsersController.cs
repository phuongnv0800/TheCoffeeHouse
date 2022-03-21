using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _repository.Authenicate(request);
                if (result.Result != 1)
                    return BadRequest(result);
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

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _repository.Register(request);
                if (result.Result != 1)
                    return BadRequest(result);
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

        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] Search request)
        {
            try
            {
                var products = await _repository.GetAll(request);
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

        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _repository.Update(id, request);
                if (result.Result != 1)
                {
                    return BadRequest(result);
                }
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var user = await _repository.GetById(id);
                return Ok(user);
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
        [HttpGet("name/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            try
            {
                var user = await _repository.GetByUserName(userName);
                return Ok(user);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _repository.Delete(id);
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

        [HttpPut("assign-roles/{id}")]
        public async Task<IActionResult> RoleAssign(string id, [FromBody] RoleAssignRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _repository.RoleAssign(id, request);
                if (result.Result != 1)
                {
                    return BadRequest(result);
                }
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
        [HttpPut("password-change")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassword request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result =await _repository.ChangePasword(req: request);


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
