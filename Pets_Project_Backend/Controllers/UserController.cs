using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.UserModels;
using Pets_Project_Backend.Data.Models.UserModels.UserDtos;
using Pets_Project_Backend.Services.UserServices;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService? _service;
        public UserController(IUserService? service)
        {
            _service = service;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("gett-all-users")]
        public async Task<IActionResult> getUsers()
        {
            try
            {
                var users = await _service.ListUsers();

                if (users == null)
                {
                    return NotFound(new ApiResponse<string>(false,"no users in the list","",null));
                }

                return Ok(new ApiResponse<IEnumerable<UserViewDTO>>(true,"done",users,null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("userbyId/{id}")]
        public async Task<IActionResult> userByid(int id)
        {
            try
            {
                var user=await _service.GetUser(id);

                if (user == null)
                {
                    return NotFound(new ApiResponse<string>(false, "no match users in the list", "", null));
                }

                return Ok(new ApiResponse<UserViewDTO>(true, "done", user, null));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("block/unblock{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> block_unb(int id)
        {
            try
            {
                var res=await _service.BlockUnblockUser(id);

                return Ok(new ApiResponse<BlockUnblockRes>(true,"updated",res,null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
