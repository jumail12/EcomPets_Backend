using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
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

        [Authorize(Roles ="Admin")]
        [HttpPatch("update-order-status/{oid}")]
        public async Task<IActionResult> OrderStatusU(int oid)
        {
            try
            {
                var res=await _orderService.UpdateOrderStatus(oid);
                return Ok(new ApiResponse<string>(true, "updated", res, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }


        [Authorize]
        [HttpPost("razor-order-create")]
        public async Task<IActionResult> Razor_orderCreate(long price)
        {
            try
            {
                if (price <= 0)
                {
                    return BadRequest("enter a valid money ");
                }

                var orderId= await _orderService.RazorPayOrderCreate(price);
                return Ok(new ApiResponse<string>(true, "Order created", orderId, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);  
            }
        }

        [Authorize]
        [HttpPost("razor-payment-verify")]
        public async Task<IActionResult> RazorPaymentVerify([FromBody] PaymentDto razorpay)
        {
            try
            {
                if (razorpay == null)
                {
                    return BadRequest(new ApiResponse<string>(false, "razorpay details must not null here",null,null));
                }
                var res=await _orderService.RazorPayment(razorpay);
                if (!res)
                {
                    return BadRequest(new ApiResponse<string>(false, "Error in payment", "", "check payment details"));
                }
                return Ok(new ApiResponse<string>(true, "done", "Success", null));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException?.Message);  
            }
        }



        [Authorize]
        [HttpPost("individual-pro-buy/{pro_id}")]
        public async Task<IActionResult> individual_probuy(int pro_id, CreateOrder_Dto dto)
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
                return Ok(new ApiResponse<string>(true, "Product purchased successfully.", "done", null));
                
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
                return Ok(new ApiResponse<string>(true, " successfully.", "done", null));

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


        [HttpPost("SearchOrders")]
        public async Task<IActionResult> SearchOrders([FromBody] OrderSearchDto orderSearchDto)
        {
            if (orderSearchDto == null)
            {
                return BadRequest("Invalid request body.");
            }

            try
            {
                var u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _orderService.SearchOrder(
                    orderSearchDto
                );

                if (res == null)
                {
                    return NoContent();
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }


    }
}
