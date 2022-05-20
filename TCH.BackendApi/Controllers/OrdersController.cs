using System.Text;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Utilities.Enum;
using TCH.Utilities.Error;
using TCH.Utilities.Roles;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;


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
    
    [AllowAnonymous]
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
    
    [AllowAnonymous]
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
    
    [AllowAnonymous]
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
    
    [AllowAnonymous]
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


    [HttpGet("print/{ID}")]
    public async Task<IActionResult> Print(string ID)
    {
        try
        {
            string invoiceHtml = await _repository.PrintInvoicePaymented(ID);
            if (string.IsNullOrEmpty(invoiceHtml))
            {
                return BadRequest(new Respond<string> { Result = 0, Message = "Failed", Data = "Không tìm thấy dữ liệu" });
            }
            return base.Content(invoiceHtml, "text/html", Encoding.UTF8);
        }
        catch (CustomException e)
        {
            return BadRequest(new Respond<string> { Result = -1, Message = "Failed", Data = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new Respond<string> { Result = -2, Message = "Failed", Data = e.Message });
        }
    }
    
    [AllowAnonymous]
    [HttpGet("print-img/{ID}")]
    public async Task<IActionResult> PrintImg(string ID)
    {
        try
        {
            string invoiceHtml = await _repository.PrintInvoicePaymented(ID);
            if (string.IsNullOrEmpty(invoiceHtml))
            {
                return BadRequest(new Respond<string> { Result = 0, Message = "Failed", Data = "Không tìm thấy dữ liệu" });
            }
            HtmlConverter converter = new HtmlConverter();
            var bytes = converter.FromHtmlString(invoiceHtml.ToString());          
            return Ok(Convert.ToBase64String(bytes));
            //return File(bytes, "image/png");
        }
        catch (CustomException e)
        {
            return BadRequest(new Respond<string> { Result = -1, Message = "Failed", Data = e.Message });
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
