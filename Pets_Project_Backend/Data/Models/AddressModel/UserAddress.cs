using Pets_Project_Backend.Data.Models.OrderModel;
using Pets_Project_Backend.Data.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.AddressModel
{
    public class UserAddress
    {
        [Key]
        public int? AddressId { get; set; }
        public string? CustomerName { get; set; }
        public int userId { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? HomeAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PostalCode { get; set; }

        public virtual User? _userAd {  get; set; }
        public ICollection<Order>? _orders { get; set; }
    }
}
