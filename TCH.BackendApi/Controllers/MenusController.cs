using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Roles;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MenusController : ControllerBase
{
    private readonly IMenuRepository _repository;
    private readonly ILogger<MenusController> _logger;

    public MenusController(IMenuRepository repository, ILogger<MenusController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [AllowAnonymous]
    [HttpGet("{branchID}")]
    public async Task<IActionResult> GetAll(string branchID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.GetMenu(branchID);
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
    [HttpPost("{branchID}")]
    public async Task<IActionResult> Create(string branchID, [FromBody] MenuRequest name)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Create(branchID, name);
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
    [HttpPost("active-product/{menuID}/{productID}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> ActiveProduct(string menuID, string productID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.ActiveProductInMenu(menuID, productID);
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
    [HttpPost("deactive-product/{menuID}/{productID}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> DeactiveProduct(string menuID, string productID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.DeactiveProductInMenu(menuID, productID);
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
    [HttpPut("{menuID}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> Update(string menuID, [FromBody] MenuRequest name)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Update(menuID, name);
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
    [HttpDelete("{menuID}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> Delete(string menuID)
    {
        try
        {
            var result = await _repository.Delete(menuID);
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
