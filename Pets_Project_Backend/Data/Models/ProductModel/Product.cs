using Pets_Project_Backend.Data.Models.CartModel;
using Pets_Project_Backend.Data.Models.CategoryModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pets_Project_Backend.Data.Models.ProductModel
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        [Required]
        public decimal? ProductPrice { get; set; }

        [Required]
        public decimal? OfferPrize { get; set; }

        [Required]
        public decimal? Rating {  get; set; } 

        [Required]
        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        public int? StockId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //nav
        public virtual Category? _Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<CartItem>? _CartItems {  get; set; } 
    }
}
