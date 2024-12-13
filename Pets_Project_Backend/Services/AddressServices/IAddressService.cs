using Pets_Project_Backend.Data.Models.AddressModel.Address_Dtos;

namespace Pets_Project_Backend.Services.AddressServices
{
    public interface IAddressService
    {
        Task<bool> AddnewAddress(int userId, AddNewAddress_dto address);
        Task<List<GetAddress_dto>> GetAddress(int userId);
        Task<bool> RemoveAddress(int addId);
        
    }
}
