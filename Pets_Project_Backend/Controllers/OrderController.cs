using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.ApiResponse;
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




        [Authorize]
        [HttpPost("individual-pro-buy/{pro_id}")]
        public async Task<IActionResult> individual_probuy(int pro_id, [FromBody]CreateOrder_Dto dto)
        {
            try
            {
                if(dto == null)
                {
                    return BadRequest("Order details are required.");
                }

                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _orderService.indvidual_ProductBuy(user_id, pro_id, dto);

                //return Ok("Product purchased successfully.");
                return Ok(new ApiResponse<string>(true, "Product purchased successfully.", null, null));
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Place-order")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(CreateOrder_Dto createOrder_Dto)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res=await _orderService.CreateOrder_CheckOut(user_id, createOrder_Dto);
                //return Ok(res + "Order placed");
                return Ok(new ApiResponse<string>(true, " successfully.", null, null));

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
                return Ok(new ApiResponse<IEnumerable<OrderView_Dto>>(true, " successfully.", res, null));

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-order-details-admin")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetOrderDetailsAdmin()
        {
            try
            {
                var res= await _orderService.GetOrderDetailsAdmin();
                if (res.Count < 0)
                {
                    return BadRequest(new ApiResponse<string>(false, "no order found", null, null));
                }

                return Ok(new ApiResponse<IEnumerable<OrderAdminViewDto>>(true, " successfully.", res, null));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Total Revenue")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
               var res= await _orderService.TotalRevenue();
                return Ok(new ApiResponse<decimal>(true, " successfully.",res , null));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Total-Products-Saled")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> TotalProductsPurchased()
        {
            try
            {
                var res= await _orderService.TotalProductsPurchased();
                return Ok(new ApiResponse<int>(true, " successfully.",res , null));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrderDetailsAdmin_byuserId/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetOrderDetailsAdmin_byuserId(int id)
        {
            try
            {
                var orderDetails = await _orderService.GetOrderDetailsAdmin_byuserId(id);
                if(orderDetails == null)
                {
                    return NotFound("User not found");
                }
               
                return Ok(new ApiResponse<IEnumerable<OrderView_Dto>>(true,"done",orderDetails,null));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
