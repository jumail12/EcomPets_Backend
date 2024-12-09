using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.CartModel;
using Pets_Project_Backend.Data.Models.CartModel.Cart_Dtos;

namespace Pets_Project_Backend.Services.CartServices
{
    public interface ICartService
    {
        Task<List<CartView_Dto>> GetAllCartItems(int userId);
        Task<ApiResponse<CartItem>> AddToCart(int userId, int productId);
        Task<ApiResponse<string>> RemoveFromCart(int userId, int productId);
        Task<ApiResponse<CartItem>> IncreaseQuantity(int userId, int productId);
        Task<ApiResponse<CartItem>> DecreaseQuantity(int userId, int productId);
    }
}
