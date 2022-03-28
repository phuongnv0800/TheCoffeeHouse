using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderRepository orderService, ILogger<OrdersController> logger)
    {
        _orderRepository = orderService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]Search request)
    {
        var orders = await _orderRepository.GetAll(request);
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(string orderId)
    {
        var order = await _orderRepository.GetById(orderId);
        if (order == null)
            return BadRequest("Can't not find order");
        return Ok(order);
    }
    [HttpGet("{userId}/paging")]
    public async Task<IActionResult> GetByUser(Guid userId, [FromQuery] Search request)
    {
        var orders = await _orderRepository.GetByUser(userId, request);
        if (orders == null)
            return BadRequest("Can't not find order");
        return Ok(orders);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var productId = await _orderRepository.Create(request);

        return Ok();
    }

    [HttpPut("{orderId}")]
    //call api with httpClient thi dung FromBody
    public async Task<IActionResult> Update([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var affectedResult = await _orderRepository.Update(request);

        return Ok();
    }
}
