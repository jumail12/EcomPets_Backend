using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.WhishListModel;
using Pets_Project_Backend.Data.Models.WhishListModel.WhishList_Dto;
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

                //return Ok(res);
                if(res.Count>0)
                {
                    return Ok(new ApiResponse<IEnumerable<WhishList_View_Dto>>(true,"whislist fetched",res,null));
                }

                return Ok(new ApiResponse<IEnumerable<WhishList_View_Dto>>(true, "no items in whislist ", res, null));

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
                var res = await _service.AddOrRemove(u_id, pro_id);

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remove/{pro_id}")]
        [Authorize]
        public async Task<IActionResult> remove(int pro_id)
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _service.RemovefromWishlist(u_id, pro_id);

                if (res.IsSuccess)
                {
                    return Ok(res);
                }

                return BadRequest(res); 
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
