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
public class RecipesController : ControllerBase
{
    private readonly IRecipeRepository _repository;
    private readonly ILogger<RecipesController> _logger;

    public RecipesController(IRecipeRepository repository, ILogger<RecipesController> logger)
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
    [HttpGet("recipe-product/{producId}")]
    public async Task<IActionResult> GetByID(string producId)
    {
        try
        {
            var result = await _repository.GetRecipeByProductID(producId);
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
    [Authorize(Roles = Permission.Branch+Permission.Manage)]
    [HttpGet("recipe-product/{productId}/{sizeId}")]
    public async Task<IActionResult> GetRecipe(string productId, string sizeId)
    {
        try
        {
            var result = await _repository.GetRecipeByProductSize(productId, sizeId);
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
    public async Task<IActionResult> Create([FromForm] IEnumerable<RecipeRequest> request)
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
    
    [HttpPut]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Update( [FromForm] RecipeRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Update(request);
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

    [HttpDelete("recipe-product/{productID}")]
    [Authorize(Roles = Permission.Branch)]
    public async Task<IActionResult> Delete(string productID)
    {
        try
        {
            var result = await _repository.Delete(productID);
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
