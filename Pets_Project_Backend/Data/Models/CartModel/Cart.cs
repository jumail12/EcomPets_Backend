using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.UserModels;
using System.Text.Json.Serialization;

namespace Pets_Project_Backend.Data.Models.CartModel
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        //nav
        [JsonIgnore]
        public virtual ICollection<CartItem>? _Items { get; set; }
        [JsonIgnore]
        public virtual User? _User { get; set; }
    }
}
