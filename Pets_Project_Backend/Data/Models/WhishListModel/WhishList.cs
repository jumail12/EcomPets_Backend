using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.WhishListModel
{
    public class WhishList
    {
        [Key]
        public int? whishId {  get; set; }  
        public int? userId { get; set; }
        public int? productId { get; set; }

        public virtual User? _User { get; set; }
        public virtual Product? _Product { get; set; }
    }
}
