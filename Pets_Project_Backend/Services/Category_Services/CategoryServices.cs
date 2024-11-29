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

        public async Task<bool> AddCategory(Category_Dto category)
        {
            var isExists = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryName == category.CategoryName);

            if (isExists != null)
            {
                return false;
            }

            var d = new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            _context.Categories.Add(d);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            var res=await _context.Categories.FirstOrDefaultAsync(a=>a.CategoryId==id);
            if (res == null)
            {
                return false;
            }
            else
            {
                _context.Categories.Remove(res);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
