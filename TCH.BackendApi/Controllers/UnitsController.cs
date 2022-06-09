using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Roles;
using TCH.Utilities.Searchs;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitsController : ControllerBase
{
    private readonly IUnitRepository _repository;
    private readonly ILogger<UnitsController> _logger;

    public UnitsController(IUnitRepository repository, ILogger<UnitsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [HttpGet]
    [AllowAnonymous]
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
    //[HttpGet("unit-exchange")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GetAllUnitExchange()
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var result = await _repository.GetAllExchangeUnit();
    //        return Ok(result);
    //    }
    //    catch (CustomException e)
    //    {
    //        return BadRequest(new { result = -1, message = e.Message });
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e.ToString());
    //        return BadRequest(new { result = -2, message = e.Message });
    //    }
    //}
    [HttpPost]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Create([FromBody] UnitRequest name)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Create(name);
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
    //[HttpPost("exchange-unit")]
    //[Authorize(Roles = Permission.Branch)]
    //public async Task<IActionResult> CreateExchangeUnit([FromBody] ExchangeUnitRequest name)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var result = await _repository.CreateExchangeUnit(name);
    //        return Ok(result);
    //    }
    //    catch (CustomException e)
    //    {
    //        return BadRequest(new { result = -1, message = e.Message });
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e.ToString());
    //        return BadRequest(new { result = -2, message = e.Message });
    //    }
    //}
    [HttpPut("{id}")]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Update(string id, [FromBody] UnitRequest name)
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
    //[HttpPut("exchange-unit")]
    //[Authorize(Roles = Permission.Branch)]
    //public async Task<IActionResult> UpdateExchangeUnit([FromBody] ExchangeUnitRequest name)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var result = await _repository.UpdateExchangeUnit(name);
    //        return Ok(result);
    //    }
    //    catch (CustomException e)
    //    {
    //        return BadRequest(new { result = -1, message = e.Message });
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e.ToString());
    //        return BadRequest(new { result = -2, message = e.Message });
    //    }
    //}
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
