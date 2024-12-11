using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.UserModels.UserDtos;

namespace Pets_Project_Backend.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<UserViewDTO>> ListUsers()
        {
            try
            {
                var users=await _context.Users
                    .Where(c => c.Role != "Admin")
                    .Select(a=> new UserViewDTO
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        UserEmail=a.UserEmail,
                        Role=a.Role,
                        isBlocked=a.isBlocked,
                    })
                    
                    .ToListAsync();
              
                
                  return users;               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<UserViewDTO> GetUser(int id)
        {
            try
            {
                var user=await _context.Users
                    .Where(c => c.Role != "Admin")
                    .Select(a=> new UserViewDTO
                    {
                        Id=a.Id,
                        UserName=a.UserName,
                        UserEmail=a.UserEmail,
                        Role=a.Role,
                        isBlocked=a.isBlocked,
                    })
                    .FirstOrDefaultAsync(a => a.Id == id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BlockUnblockRes> BlockUnblockUser(int id)
        {
            var user=await _context.Users.FirstOrDefaultAsync(a=> a.Id == id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.isBlocked=!user.isBlocked;
            await _context.SaveChangesAsync();

            return new BlockUnblockRes
            {
                isBlocked=user.isBlocked==true? true : false,
                Msg=user.isBlocked==true? "User is blocked" : "User  unblocked"
            };
        }
    }
}
