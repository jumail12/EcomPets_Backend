using Pets_Project_Backend.Data.Models.UserModels.UserDtos;

namespace Pets_Project_Backend.Services.Auth_Services
{
    public interface IAuthServices
    {
        Task<bool> Register(UserRegistration_Dto userRegistration_Dto);
        Task<UserResponse_Dto> Login(UserLogin_Dto userLogin_Dto);
    }
}
