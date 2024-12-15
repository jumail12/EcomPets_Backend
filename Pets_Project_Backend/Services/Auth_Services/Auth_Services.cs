using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.UserModels;
using Pets_Project_Backend.Data.Models.UserModels.UserDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pets_Project_Backend.Services.Auth_Services
{
    public class Auth_Services : IAuthServices
    {
        
        private readonly ApplicationContext context;
     
        private readonly IConfiguration configuration; //for token generation

        public Auth_Services( ApplicationContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }


        //register
        public async Task<bool> Register(UserRegistration_Dto userRegistration_Dto)
        {
            try
            {
                var isExists = await context.Users.FirstOrDefaultAsync(a => userRegistration_Dto.UserEmail == a.UserEmail);
                if(isExists != null)
                {
                    return false;
                }

                //password hashing
                var HashPassword=BCrypt.Net.BCrypt.HashPassword(userRegistration_Dto.Password);
                userRegistration_Dto.Password = HashPassword;

                var u = new User
                {
                    UserName = userRegistration_Dto.UserName,
                    UserEmail = userRegistration_Dto.UserEmail,
                    Password = HashPassword
                };
                context.Users.Add(u);
                await context.SaveChangesAsync();
               

                return true;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //login 
        public async Task<UserResponse_Dto> Login(UserLogin_Dto userLogin_Dto)
        {
            try
            {
                var u= await context.Users.FirstOrDefaultAsync(a=>a.UserEmail == userLogin_Dto.UserEmail);
                if(u == null)
                {
                 
                    return new UserResponse_Dto{ Error="Not Found"};
                }

                //validating password
                var pass = ValidatePassword(userLogin_Dto.Password,u.Password);
                //password checking
                if (!pass)
                {
                  
                    return new UserResponse_Dto { Error ="Invalid password"};
                }

                //check the user Blocked or not
                if (u.isBlocked == true)
                {
                   
                    return new UserResponse_Dto { Error = "User Blocked" };
                }

                //generate token
                var token = Generate_Token(u);

                return new UserResponse_Dto
                {
                    Token = token,
                    Role = u.Role,
                    UserEmail = userLogin_Dto.UserEmail,
                    Id=u.Id,
                    UserName=u.UserName
                };
            }
            catch(Exception ex)
            {
             
                throw new Exception(ex.Message);
            }
        }


        //Token generation
        private string Generate_Token(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName ),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.UserEmail)
            };

            var token = new JwtSecurityToken(
                claims: claim,
                signingCredentials: credentails,
                expires: DateTime.UtcNow.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //Bcrypt password verification
        private bool ValidatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }



    }
}
