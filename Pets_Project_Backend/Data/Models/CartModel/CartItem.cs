using Pets_Project_Backend.Data.Models.ProductModel;
using System.Text.Json.Serialization;

namespace Pets_Project_Backend.Data.Models.CartModel
{
    public class CartItem
    {
        public int? CartItemId { get; set; }
        public int? ProductId { get; set; }
        public int? CartId { get; set; }
        public int? ProductQty { get; set; }

        //nav
        [JsonIgnore]
        public virtual Cart? _Cart{ get; set; }
        [JsonIgnore]
        public virtual Product? _Product { get; set; }
    }
}
