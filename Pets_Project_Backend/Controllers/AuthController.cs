using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.UserModels.UserDtos;
using Pets_Project_Backend.Services.Auth_Services;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
       

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
          
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration_Dto newUser)
        {
            try
            {
                bool isDone=await _authServices.Register(newUser);
                if (!isDone)
                {
                    return BadRequest(new ApiResponse<string>(false, "User alredy exists","[]",null));
                }

            
                return Ok(new ApiResponse<string>(true, "User registered succesfully", "[done]", null));


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin_Dto login)
        {
            try
            {
                var res=await _authServices.Login(login);
                

                if(res.Error=="Not Found")
                {
                    return NotFound("Email is not verified");
                }

                if(res.Error== "Invalid password")
                {
                    return BadRequest(res.Error);
                }

                if (res.Error== "User Blocked")
                {
                    return StatusCode(403, "User is blocked by admin!");
                }

                return Ok(new UserResponse_Dto { Id=res.Id, UserName=res.UserName,Token=res.Token,UserEmail=res.UserEmail,Role=res.Role});
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message); 

            }
        }
    }
}
