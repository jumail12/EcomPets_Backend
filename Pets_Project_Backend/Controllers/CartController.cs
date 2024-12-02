using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.Services.CartServices;
using System.Security.Claims;

namespace Pets_Project_Backend.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("cartitems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                //var user_id=Convert.ToInt32(HttpContext.Items["Id"]);
                var userid=User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
                var items = await _cartService.GetAllCartItems(int.Parse(userid));

                if (items == null)
                {
                    return NotFound("no user");
                }

                if(items.Count == 0)
                {
                    return BadRequest("no item in cart");
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddToCart/{pro_id}")]
        public async Task<IActionResult> AddtoCart(int pro_id)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res=await _cartService.AddToCart(user_id, pro_id);

                if (res)
                {
                    return Ok("Product added to the cart");
                }

                return BadRequest("Item already in your cart!");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteItemFromCart/{pro_id}")]
        public async Task<IActionResult> Remove_FromCart(int pro_id)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
               var data = await _cartService.RemoveFromCart(user_id,pro_id);

                if(data)
                {
                    return Ok("Item removed from cart");
                }

                return NotFound("No product found");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("increment product qty/{pro_id}")]
        public async Task<IActionResult> INR_ProQty(int pro_id)
        {
            try
            {
                var user_id= Convert.ToInt32(HttpContext.Items["Id"]);
              var data=  await _cartService.IncreaseQuantity(user_id,pro_id);

                if (data)
                {
                    return Ok("Qunatity icreased");
                }

                return BadRequest("No product found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("decrement product qty/{pro_id}")]
        public async Task<IActionResult> DCR_ProQty(int pro_id)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var data = await _cartService.DecreaseQuantity(user_id, pro_id);

                if (data)
                {
                    return Ok("Qunatity decreased");
                }

                return BadRequest("No product found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
