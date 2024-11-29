using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto
{
    public class Category_Dto
    {
        public int CategoryId { get; set; } 
        public string? CategoryName { get; set; }
    }
}
