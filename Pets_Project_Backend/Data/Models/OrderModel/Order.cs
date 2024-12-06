using Pets_Project_Backend.Data.Models.AddressModel;
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
        public int? AddressId { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }

        public string? OrderStatus {  get; set; }

        [Required]
        public decimal? Total { get; set; }
        [Required]
        public string? OrderString { get; set; }
   
        [Required]
        public string? TransactionId { get; set; }

        public virtual UserAddress? _UserAd {  get; set; }

        public virtual User? _user {  get; set; }
        public virtual List<OrderItem>? _Items { get; set; }

    }
}
