using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddCategory(Cat_Add_dto category)
        {
            var isExists = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryName == category.CategoryName);

            if (isExists != null)
            {
                return false;
            }

            var d = new Category
            {
               
                CategoryName = category.CategoryName,
            };

            _context.Categories.Add(d);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            try
            {

                var res = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == id);
                var pro = await _context.Products.Where(a=>a.CategoryId == id).ToListAsync();

                if (res == null)
                {
                    return false;
                }
                else
                {
                    _context.Products.RemoveRange(pro);
                    _context.Categories.Remove(res);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
