using Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto;

namespace Pets_Project_Backend.Services.Category_Services
{
    public interface ICategoryServices
    {
        Task<List<Category_Dto>> GetCategories();
        Task<bool> AddCategory(Cat_Add_dto category);
        Task<bool> RemoveCategory(int id);

    }
}
