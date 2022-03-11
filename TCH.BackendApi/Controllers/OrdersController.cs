//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TCH.BackendApi.Service.Catalog;
//using TCH.ViewModel.Catalog;
//using TCH.ViewModel.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TCH.BackendApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class OrdersController : ControllerBase
//    {
//        private readonly IOrderService _orderService;

//        public OrdersController(IOrderService orderService)
//        {
//            _orderService = orderService;
//        }
//        [HttpGet("paging")]
//        public async Task<IActionResult> GetAllPaging([FromQuery]PagingRequest request)
//        {
//            var orders = await _orderService.GetAllPaging(request);
//            return Ok(orders);
//        }

//        [HttpGet("{orderId}")]
//        public async Task<IActionResult> GetById(int orderId)
//        {
//            var order = await _orderService.GetById(orderId);
//            if (order == null)
//                return BadRequest("Can't not find order");
//            return Ok(order);
//        }
//        [HttpGet("{userId}/paging")]
//        public async Task<IActionResult> GetByUser(Guid userId, [FromQuery] PagingRequest request)
//        {
//            var orders = await _orderService.GetByUser(userId, request);
//            if (orders == null)
//                return BadRequest("Can't not find order");
//            return Ok(orders);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody]OrderRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var productId = await _orderService.Create(request);
//            if (!productId)
//                return BadRequest();

//            //var product = await _orderService.GetById(productId);
//            return Ok();
//            //return CreatedAtAction(nameof(GetById), new { id = productId }, product);
//        }

//        [HttpPut("{orderId}")]
//        //call api with httpClient thi dung FromBody
//        public async Task<IActionResult> Update([FromBody]OrderRequest request)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var affectedResult = await _orderService.Update(request);
//            if (!affectedResult)
//                return BadRequest();

//            return Ok();
//        }
//    }
//}
