using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.AddressModel;
using Pets_Project_Backend.Data.Models.AddressModel.Address_Dtos;

namespace Pets_Project_Backend.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationContext _context;
        public AddressService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AddnewAddress(int userId, AddNewAddress_dto address)
        {
            try
            {
                if(address == null)
                {
                    throw new ArgumentNullException(nameof(address), "Address information is required.");
                }

                var user= await _context.Users.FindAsync(userId);
                if(user == null)
                {
                    throw new Exception("User  not found.");
                }

                var newAdd = new UserAddress
                {
                    CustomerName = address.CustomerName,
                    userId = userId,
                    StreetName = address.StreetName,
                    City = address.City,
                    HomeAddress = address.HomeAddress,
                    CustomerPhone = address.CustomerPhone,
                    PostalCode = address.PostalCode,
                };

                _context.UserAddress.Add(newAdd);
                await _context.SaveChangesAsync();
                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");

            }
        }

        public async Task<List<GetAddress_dto>> GetAddress(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new Exception("User  not found.");
                }

                var address=await _context.UserAddress.Where(a=>a.userId == userId)
                    .Select(b=> new GetAddress_dto
                    {
                        CustomerName=b.CustomerName,
                        StreetName=b.StreetName,
                        City=b.City,
                        HomeAddress=b.HomeAddress,
                        CustomerPhone=b.CustomerPhone,
                        PostalCode=b.PostalCode,
                    })
                    .ToListAsync();  

                return address;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
