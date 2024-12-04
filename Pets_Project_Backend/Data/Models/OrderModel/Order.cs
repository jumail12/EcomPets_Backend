using Pets_Project_Backend.Data.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.OrderModel
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? CustomerPhone { get; set; }
        [Required]
        public string? CustomerCity { get; set; }
        [Required]
        public string? HomeAddress { get; set; }
        [Required]
        public decimal? Total { get; set; }
        [Required]
        public string? OrderString { get; set; }
   
        [Required]
        public string? TransactionId { get; set; }

        public virtual User? _user {  get; set; }
        public virtual ICollection<OrderItem>? _Items { get; set; }

    }
}
