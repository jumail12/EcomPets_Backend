using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.WhishListModel.WhishList_Dto;

namespace Pets_Project_Backend.Services.WhishList_Service
{
    public interface IWhishListService
    {
        Task<ApiResponse<string>> AddOrRemove(int u_id, int pro_id);
        Task<List<WhishList_View_Dto>> GetAllWishItems(int u_id);
    }
}
