using Pets_Project_Backend.Data.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.CategoryModel
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string?  CategoryName { get; set; }

        //nav
        public virtual ICollection<Product> _Products { get; set; }
    }
}
