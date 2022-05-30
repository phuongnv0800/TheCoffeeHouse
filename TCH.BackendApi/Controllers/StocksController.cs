using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Roles;
using TCH.Utilities.Searchs;
using TCH.ViewModel.RequestModel;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockRepository _repository;
    private readonly ILogger<StocksController> _logger;

    public StocksController(IStockRepository repository, ILogger<StocksController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpGet("get-by-branch/{branchId}")]
    public async Task<IActionResult> GetAllStockByBranchID(string branchId, [FromQuery] Search search)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.GetAllStockByBranchID(branchId, search);
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
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll([FromQuery] Search search)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.GetAllStock(search);
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
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpPost("create/{branchId}")]
    public async Task<IActionResult> Create(string branchId, [FromBody] StockRequest search)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.CreateStockMaterial(search);
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
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpPut("{branchId}")]
    public async Task<IActionResult> Update(string branchId, [FromBody] StockRequest search)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.UpdateStockMaterial(search);
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
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpDelete("{branchId}/{materialID}")]
    public async Task<IActionResult> Delete(string branchId, string materialID)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.DeleteStockMaterial(branchId, materialID);
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
