using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.ProductModel.Product_Dto
{
    public class AddProduct_Dto
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        [Required]
        public decimal? ProductPrice { get; set; }
        [Required]
        public decimal? OfferPrize { get; set; }



        [Required]
        public int CategoryId { get; set; }
    }
}
