using Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto;

namespace Pets_Project_Backend.Services.Category_Services
{
    public interface ICategoryServices
    {
        Task<List<Category_Dto>> GetCategories();
        Task<bool> AddCategory(Category_Dto category);
        Task<bool> RemoveCategory(int id);

    }
}
