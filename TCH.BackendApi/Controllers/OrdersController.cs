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
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;


namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetAll([FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetAll(request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [AllowAnonymous]
    [HttpGet("get-revenue")]
    public async Task<IActionResult> GetChartMoney([FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetChartMoney(request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [AllowAnonymous]
    [HttpGet("get-revenue-branch/{branchId}")]
    public async Task<IActionResult> GetChartMoneyByBranchId(string branchId, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetChartMoneyByBranchId(branchId, request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [AllowAnonymous]
    [HttpGet("get-product-branch-all")]
    public async Task<IActionResult> GetProductInOrderAllBranch([FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetProductInOrderAllBranch(request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [AllowAnonymous]
    [HttpGet("get-product-branch/{branchId}")]
    public async Task<IActionResult> GetProductInOrder(string branchId, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetProductInOrder(branchId, request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpGet("all-money")]
    public async Task<IActionResult> GetMoneyAll([FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetAllMoney(request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch)]
    [HttpGet("excel-all")]
    public async Task<IActionResult> ExcelAllOrder([FromQuery] Search request)
    {
        try
        {
            string newPath = await _repository.ExcelAllOrder(request);
            if (newPath == null)
            {
                return BadRequest(new {result = -1, message = "Không thấy"});
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpGet("excel-by-branchId/{branchID}")]
    public async Task<IActionResult> ExcelAllOrderByBranchID(string branchID, [FromQuery] Search request)
    {
        try
        {
            string newPath = await _repository.ExcelAllOrderByBranchID(branchID, request);
            if (newPath == null)
            {
                return BadRequest(new {result = -1, message = "Không thấy"});
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpGet("all-money/{branchID}")]
    public async Task<IActionResult> GetMoneyAllByBranchID(string branchID, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetAllMoneyByBranchId(branchID, request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [AllowAnonymous]
    [HttpGet("branch/{branchID}")]
    public async Task<IActionResult> GetByBranchID(string branchID, [FromQuery] Search request)
    {
        try
        {
            var orders = await _repository.GetByBranhID(branchID, request);
            return Ok(orders);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }


    [AllowAnonymous]
    [HttpGet("print/{ID}")]
    public async Task<IActionResult> Print(string ID)
    {
        try
        {
            string invoiceHtml = await _repository.PrintInvoicePaymented(ID);
            if (string.IsNullOrEmpty(invoiceHtml))
            {
                return BadRequest(new Respond<string>
                    {Result = 0, Message = "Failed", Data = "Không tìm thấy dữ liệu"});
            }

            return base.Content(invoiceHtml, "text/html", Encoding.UTF8);
        }
        catch (CustomException e)
        {
            return BadRequest(new Respond<string> {Result = -1, Message = "Failed", Data = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new Respond<string> {Result = -2, Message = "Failed", Data = e.Message});
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
                return BadRequest(new Respond<string>
                    {Result = 0, Message = "Failed", Data = "Không tìm thấy dữ liệu"});
            }

            HtmlConverter converter = new HtmlConverter();
            var bytes = converter.FromHtmlString(invoiceHtml.ToString());
            return Ok(Convert.ToBase64String(bytes));
            //return File(bytes, "image/png");
        }
        catch (CustomException e)
        {
            return BadRequest(new Respond<string> {Result = -1, Message = "Failed", Data = e.Message});
        }
    }
    [AllowAnonymous]
    [HttpGet("compare/{branchId}")]
    public async Task<IActionResult> CompareOrderUserInBranch(string branchId,[FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.CompareOrderUserInBranch(branchId, search);
            return Ok(respond);
            //return File(bytes, "image/png");
        }
        catch (CustomException e)
        {
            return BadRequest(new Respond<string> {Result = -1, Message = "Failed", Data = e.Message});
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize]
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }


    [Authorize(Roles = Permission.Branch +","+ Permission.Manage)]
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch +","+ Permission.Manage)]
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
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
}