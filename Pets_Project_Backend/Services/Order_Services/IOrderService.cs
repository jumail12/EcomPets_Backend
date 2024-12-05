using Pets_Project_Backend.Data.Models.OrderModel.Order_Dto;

namespace Pets_Project_Backend.Services.Order_Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder_CheckOut(int userId,CreateOrder_Dto  createOrderDto);
        Task<List<OrderView_Dto>> GetOrderDetails(int userId);
        Task<List<OrderAdminViewDto>> GetOrderDetailsAdmin();
        Task<decimal> TotalRevenue();
        Task<int> TotalProductsPurchased();
       Task<List<OrderView_Dto>> GetOrderDetailsAdmin_byuserId(int userId);
    }
}
