using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Roles;
using TCH.Utilities.Searchs;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchsController: ControllerBase
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<BranchsController> _logger;

    public BranchsController(IBranchRepository repository, ILogger<BranchsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Search search)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.GetAll(search);
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
    [HttpGet("{branchID}")]
    public async Task<IActionResult> GetByID(string branchID)
    {
        try
        {
            var result = await _repository.GetByID(branchID);
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
    [HttpPost]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Create([FromForm] BranchRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Create(request);
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

    [HttpPost("add-user/{branchID}/{userID}")]
    
    [Authorize(Roles = Permission.Branch+","+ Permission.Manage)]
    public async Task<IActionResult> AddUserToBranch(string branchID,string userID)
    {
        try
        {
            var result = await _repository.AddUserToBranch(userID, branchID);
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

    [HttpPost("remove-user/{branchID}/{userID}")]

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> RemoveUserToBranch(string branchID,string userID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.RemoveUserToBranch(userID, branchID);
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

    [HttpPut("{id}")]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Update(string id, [FromForm] BranchRequest name)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Update(id, name);
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

    [HttpDelete("{id}")]
    [Authorize(Roles = Permission.Branch)]
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
}
