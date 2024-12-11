using Pets_Project_Backend.Data.Models.UserModels.UserDtos;

namespace Pets_Project_Backend.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserViewDTO>> ListUsers();
        Task<UserViewDTO> GetUser(int id);
        Task<BlockUnblockRes> BlockUnblockUser(int id);
    }
}
