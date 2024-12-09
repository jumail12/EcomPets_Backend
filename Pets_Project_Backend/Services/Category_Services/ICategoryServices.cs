using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.CategoryModel;
using Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto;

namespace Pets_Project_Backend.Services.Category_Services
{
    public interface ICategoryServices
    {
        Task<List<Category_Dto>> GetCategories();
        Task<ApiResponse<Cat_Add_dto>> AddCategory(Cat_Add_dto category);
        Task<ApiResponse<string>> RemoveCategory(int id);

    }
}
