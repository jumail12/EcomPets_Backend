using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.Data.Models.OrderModel.Order_Dto;
using Pets_Project_Backend.Services.Order_Services;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Place-order")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(CreateOrder_Dto createOrder_Dto)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res=await _orderService.CreateOrder_CheckOut(user_id, createOrder_Dto);
                return Ok(res + "Order placed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrder-Details")]
        public async Task<IActionResult> GetorderDetails()
        {
            try
            {
                var u_id=Convert.ToInt32(HttpContext.Items["Id"]);
                var res=await _orderService.GetOrderDetails(u_id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
