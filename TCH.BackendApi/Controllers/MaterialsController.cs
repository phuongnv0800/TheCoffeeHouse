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
public class MaterialsController : ControllerBase
{
    private readonly IMaterialRepository _repository;
    private readonly ILogger<MaterialsController> _logger;

    public MaterialsController(IMaterialRepository repository, ILogger<MaterialsController> logger)
    {
        this._repository = repository;
        this._logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllMaterial(search);
            return Ok(respond);
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
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MaterialRequest request)
    {
        try
        {
            var respond = await _repository.CreateMaterial(request);
            return Ok(respond);
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] MaterialRequest request)
    {
        try
        {
            var respond = await _repository.UpdateMaterial(id, request);
            return Ok(respond);
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
            var respond = await _repository.DeleteMaterial(id);
            return Ok(respond);
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
    [HttpGet("type")]
    public async Task<IActionResult> GetAllType([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllMaterialType(search);
            return Ok(respond);
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
    [HttpPost("type")]
    public async Task<IActionResult> CreateType([FromBody] MaterialTypeRequest request)
    {
        try
        {
            var respond = await _repository.CreateMaterialType(request);
            return Ok(respond);
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
    [HttpPut("type/{id}")]
    public async Task<IActionResult> UpdateType(string id, [FromBody] MaterialTypeRequest request)
    {
        try
        {
            var respond = await _repository.UpdateMaterialType(id, request);
            return Ok(respond);
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
    [HttpDelete("type/{id}")]
    public async Task<IActionResult> DeleteType(string id)
    {
        try
        {
            var respond = await _repository.DeleteMaterialType(id);
            return Ok(respond);
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
