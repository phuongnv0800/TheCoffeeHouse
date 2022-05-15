using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Error;
using TCH.Utilities.Searchs;

namespace TCH.BackendApi.Controllers
{
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
        [Authorize]
        [HttpGet("get-by-branch/{branchId}")]
        public async Task<IActionResult> GetAll(string branchId,[FromQuery] Search search)
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
    }
}
