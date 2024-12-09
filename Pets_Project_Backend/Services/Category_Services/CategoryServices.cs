using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.CategoryModel;
using Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto;

namespace Pets_Project_Backend.Services.Category_Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationContext _context;
        public CategoryServices(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Category_Dto>> GetCategories()
        {
            var c= await _context.Categories
                .Select(a=>new Category_Dto
                {
                    CategoryId = a.CategoryId,
                    CategoryName = a.CategoryName,
                })
                .ToListAsync();

            return c;

        }

        public async Task<ApiResponse<Cat_Add_dto>> AddCategory(Cat_Add_dto category)
        {
            var isExists = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryName == category.CategoryName);

            if (isExists != null)
            {
                return new ApiResponse<Cat_Add_dto>(false,"category already exists",null,"add another category");
            }

            var d = new Category
            {
               
                CategoryName = category.CategoryName,
            };

            _context.Categories.Add(d);
            await _context.SaveChangesAsync();
            return new ApiResponse<Cat_Add_dto>(true,"new category added to database",category,null);
        }

        public async Task<ApiResponse<string>> RemoveCategory(int id)
        {
            try
            {

                var res = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == id);
                var pro = await _context.Products.Where(a=>a.CategoryId == id).ToListAsync();

                if (res == null)
                {
                    return new ApiResponse<string>(false,"category not found","","check the details");
                }
                else
                {
                    _context.Products.RemoveRange(pro);
                    _context.Categories.Remove(res);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true,"done","category deleted",null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
