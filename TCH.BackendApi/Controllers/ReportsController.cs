using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportRepository _repository;
    private readonly ILogger<RolesController> _logger;

    public ReportsController(IReportRepository roleService, ILogger<RolesController> logger)
    {
        _repository = roleService;
        _logger = logger;
    }

    [HttpGet("import")]
    public async Task<IActionResult> GetAllImport([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllImportReport(search);
            return Ok(respond);
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
    [HttpGet("export")]
    public async Task<IActionResult> GetAllExport([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllExportReport(search);
            return Ok(respond);
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
    [HttpGet("liquidation")]
    public async Task<IActionResult> GetAllLiquidation([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllLiquidationReport(search);
            return Ok(respond);
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
    [HttpGet("import/{id}")]
    public async Task<IActionResult> GetImportByID(string id)
    {
        try
        {
            var respond = await _repository.GetImportReportByID(id);
            return Ok(respond);
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
    [HttpGet("export/{id}")]
    public async Task<IActionResult> GetExportByID(string id)
    {
        try
        {
            var respond = await _repository.GetExportReportByID(id);
            return Ok(respond);
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
    [HttpGet("liquidation/{id}")]
    public async Task<IActionResult> GetLiquidationByID(string id)
    {
        try
        {
            var respond = await _repository.GetLiquidationReportByID(id);
            return Ok(respond);
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
    [HttpPost("import")]
    public async Task<IActionResult> CreateImport([FromBody] Import search)
    {
        try
        {
            var respond = await _repository.CreateImportReport(search);
            return Ok(respond);
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
    [HttpPost("export")]
    public async Task<IActionResult> CreateExport([FromBody] Export search)
    {
        try
        {
            var respond = await _repository.CreateExportReport(search);
            return Ok(respond);
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
    [HttpPost("liquidation")]
    public async Task<IActionResult> CreateLiquidation([FromBody] Liquidation search)
    {
        try
        {
            var respond = await _repository.CreateLiquidationReport(search);
            return Ok(respond);
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
