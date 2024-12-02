using Pets_Project_Backend.Data.Models.CartModel;
using Pets_Project_Backend.Data.Models.CartModel.Cart_Dtos;

namespace Pets_Project_Backend.Services.CartServices
{
    public interface ICartService
    {
        Task<List<CartView_Dto>> GetAllCartItems(int userId);
        Task<bool> AddToCart(int userId, int productId);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> IncreaseQuantity(int userId, int productId);
        Task<bool> DecreaseQuantity(int userId, int productId);
    }
}
