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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [AllowAnonymous]
    [HttpGet("{materialId}")]
    public async Task<IActionResult> GetByID(string materialId)
    {
        try
        {
            var result = await _repository.GetMaterialByID(materialId);
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
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> Create([FromForm] MaterialRequest request)
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpPut("{id}")]
    [Authorize(Roles =Permission.Branch + "," + Permission.Manage)]
    public async Task<IActionResult> Update(string id, [FromForm] MaterialRequest request)
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [AllowAnonymous]
    [HttpGet("type/{materialId}")]
    public async Task<IActionResult> GetTypeByID(string materialId)
    {
        try
        {
            var result = await _repository.GetMaterialTypeByID(materialId);
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
    [HttpPost("type")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpPut("type/{id}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
    [HttpDelete("type/{id}")]
    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
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
            _logger.LogError(e.ToString());
            return BadRequest(new { result = -2, message = e.Message });
        }
    }
}
