using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.Utilities.Error;
using TCH.Utilities.Searchs;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Roles;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleService;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IRoleRepository roleService, ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Search search)
    {
        try
        {
            var roles = await _roleService.GetAll(search);
            return Ok(roles);
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
