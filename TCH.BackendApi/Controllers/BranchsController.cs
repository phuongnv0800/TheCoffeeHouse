using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BranchsController: ControllerBase
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<BranchsController> _logger;

    public BranchsController(IBranchRepository repository, ILogger<BranchsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
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
    public async Task<IActionResult> Create([FromBody] BranchRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Create(request);
            if (result.Result != 1)
                BadRequest();
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
    [HttpPost("add-user/{branchID}")]
    public async Task<IActionResult> AddUserToBranch(string branchID,[FromBody] string userID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
    [HttpPost("remove-user/{branchID}")]
    public async Task<IActionResult> RemoveUserToBranch(string branchID, [FromBody] string userID)
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
    public async Task<IActionResult> Update(string id, [FromBody] BranchRequest name)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Update(id, name);
            if (result.Result != 1)
                BadRequest();
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
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await _repository.Delete(id);
            if (result.Result != 1)
                BadRequest();
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
