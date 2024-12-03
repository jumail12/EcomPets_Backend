using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.Services.WhishList_Service;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWhishListService _service;
        public WishListController(IWhishListService service)
        {
            _service = service;
        }

        [HttpGet("GetWhishList")]
        [Authorize]
        public async Task<IActionResult> GetWishList()
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res= await _service.GetAllWishItems(u_id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrRemove/{pro_id}")]
        [Authorize]
        public async Task<IActionResult> AddOrRemove(int pro_id)
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                string res=await _service.AddOrRemove(u_id, pro_id);

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
