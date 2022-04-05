using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Enum;
using TCH.BackendApi.Models.Error;
using TCH.BackendApi.Models.Roles;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger)
    {
        _repository = orderRepository;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]Search request)
    {
        try
        {
            var orders = await _repository.GetAll(request);
            return Ok(orders);
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
    
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(string orderId)
    {
        try
        {
            var order = await _repository.GetById(orderId);
            return Ok(order);
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
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserID(string userId, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetByUser(userId, request);
            return Ok(orders);
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
    [HttpGet("branch/{userId}")]
    public async Task<IActionResult> GetByBranchID(string branchID, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetByBranhID(branchID, request);
            return Ok(orders);
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
    public async Task<IActionResult> Create([FromBody] OrderRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
    [HttpPost("status/{orderID}/{status}")]
    public async Task<IActionResult> UpdateStatus(string orderID, OrderStatus status)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.UpdateStatus(orderID, status);
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
    [HttpDelete("{orderID}")]
    public async Task<IActionResult> Delete(string orderID)
    {
        try
        {
            var result = await _repository.Delete(orderID);
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

    [HttpPut("{orderId}")]
    //call api with httpClient thi dung FromBody
    public async Task<IActionResult> Update([FromBody] OrderRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affectedResult = await _repository.Update(request);

            return Ok();
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
