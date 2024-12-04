using Pets_Project_Backend.Data.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.OrderModel
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal? TotalPrice { get; set; }

        [Required]
        public int? Quantity { get; set; }

        public virtual Order? _order { get; set; }
        public virtual Product? _product { get; set; }

    }
}
