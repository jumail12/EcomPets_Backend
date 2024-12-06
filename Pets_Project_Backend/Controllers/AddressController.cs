using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.Data.Models.AddressModel.Address_Dtos;
using Pets_Project_Backend.Services.AddressServices;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Add-new-Address")]
        [Authorize]
        public async Task<IActionResult> Add_newAdd([FromBody] AddNewAddress_dto _dto)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _addressService.AddnewAddress(user_id, _dto);
                return Ok(new { message = "Address added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetAddresses")]
        [Authorize]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var user_id= Convert.ToInt32(HttpContext.Items["Id"]);

                var res = await _addressService.GetAddress(user_id);
                if (res == null || !res.Any())
                {
                    return NotFound(new { message = "No addresses found for this user." });
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
