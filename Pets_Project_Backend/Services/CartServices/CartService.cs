using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.CartModel;
using Pets_Project_Backend.Data.Models.CartModel.Cart_Dtos;
using Pets_Project_Backend.Data.Models.ProductModel;

namespace Pets_Project_Backend.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ApplicationContext _context;

        public CartService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<CartView_Dto>> GetAllCartItems(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var userCart = await _context.Cart
                .Include(cart => cart._Items)
                .ThenInclude(item => item._Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

           



            if (userCart == null)
            {
                return new List<CartView_Dto>(); // Return empty list if no cart is found
            }

            return userCart._Items
                .Select(item => new CartView_Dto
                {
                    ProductId = item._Product.ProductId,
                    ProductName = item._Product.ProductName,
                    Price = (int?)item._Product.OfferPrize,
                    ProductImage = item._Product.ImageUrl,
                    TotalAmount = (int?)(item._Product.OfferPrize * item.ProductQty),
                    Quantity = item.ProductQty
                })
                .ToList();
        }


        public async Task<ApiResponse<CartItem>> AddToCart(int userId, int productId)
        {
            try
            {
                var user=await _context.Users
                    .Include(a => a._Cart)
                    .ThenInclude(b => b._Items)
                    .FirstOrDefaultAsync (c => c.Id == userId);

                //user verification
                if(user == null)
                {
                    //throw new ArgumentException("User not found!");
                    return new ApiResponse<CartItem>(false, "User not found!",null,"Check dtails");
                }

                //cart existb or not
                if (user._Cart == null)
                {
                    var new_cart = new Cart
                    {
                        UserId = userId,
                    };

                    _context.Cart.Add(new_cart);
                    await _context.SaveChangesAsync();

                    user._Cart = new_cart;
                }

                //item alredy in r not
                var check=user._Cart?._Items?.FirstOrDefault(a=>a.ProductId==productId);
                if(check != null)
                {
                    //throw new ArgumentException("Item alredy in your cart!");
                    return new ApiResponse<CartItem>(false, "Item alredy in your cart!", null, "Check dtails");


                }

                //chechking he sock of product
                var pro=await _context.Products.FirstOrDefaultAsync(a=>a.ProductId==productId);
                if (pro == null || pro?.StockId <= 0)
                {
                    //throw new ArgumentException("Product not found or out of stock.");
                    return new ApiResponse<CartItem>(false, "Product not found or out of stock.", null, "Check dtails");


                }


                //add net cart item
                var newItem = new CartItem
                {
                    ProductId=productId,
                    CartId=user._Cart?.CartId
                };

                //user._Cart?._Items.Add(newItem);
                _context.CartItems.Add(newItem);
                await _context.SaveChangesAsync();

                return new ApiResponse<CartItem>(true, "Successfully added to the cart", newItem, null);
               
            }


        
            catch (Exception ex)
            {

                //throw new ApiResponse<CartItem>(false, "Internal server error",null, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //remove from cat
        public async Task<ApiResponse<string>> RemoveFromCart(int userId, int productId)
        {
            try
            {
                var user =await _context.Users.Include(a=>a._Cart)
                    .ThenInclude(b=>b._Items)
                    .FirstOrDefaultAsync(c=>c.Id==userId);

                if(user == null)
                {
                    return new ApiResponse<string>(false, "User not found", null, "verify details");
                }

                var pro_check= user._Cart?._Items?.FirstOrDefault(a=>a.ProductId==productId);
                if(pro_check == null)
                {
                    return new ApiResponse<string>(false, "Product not found in Cart", null, "Check your items");

                }

                _context.CartItems.Remove(pro_check);
                await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Product removed from  Cart","" , null);

            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while removing the item from the cart.", ex);
            }
        }

        public async Task<ApiResponse<CartItem>> IncreaseQuantity(int userId, int pro_id)
        {
            try
            {
               
                var user=await _context.Users.Include(a=>a._Cart)
                    .ThenInclude(b=>b._Items)
                    .ThenInclude(c=>c._Product)
                    .FirstOrDefaultAsync (c=>c.Id==userId);

               if(user == null) 
                {
                    return new ApiResponse<CartItem>(false, "user not found", null, "Check the informations");
                }

               //var proExists=user._Cart?._Items?.FirstOrDefault(a=>a.ProductId == pro_id);
               // if(proExists==null)
               // {
               //     throw new ArgumentException("Product not found");
               // }

                var item =user._Cart?._Items?.FirstOrDefault(b=>b.ProductId==pro_id);
                if (item==null)
                {
                    //throw new ArgumentException("Product not found in cart");
                    return new ApiResponse<CartItem>(false, "Product not found in cart", null, "Check the informations");

                }

                if (item.ProductQty >= 10)
                {
                    //throw new ArgumentException("You reach max quantity (10)");
                    return new ApiResponse<CartItem>(false, "You reach max quantity (10)", null, "Check the informations");

                }

                if (item.ProductQty >= item._Product?.StockId)
                {
                    //throw new ArgumentException("Out of stock!");
                    return new ApiResponse<CartItem>(false, "Out of stock", null, "Check the informations");

                }

                item.ProductQty++;
                await _context.SaveChangesAsync();
                return new ApiResponse<CartItem>(true, "Quantity increased", item, null);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }


        //decrease qty
        public async Task<ApiResponse<CartItem>> DecreaseQuantity(int userId, int productId)
        {
            try
            {
                var user = await _context.Users
                    .Include(a => a._Cart)
                    .ThenInclude(b => b._Items)
                    .ThenInclude(c => c._Product)
                    .FirstOrDefaultAsync(c => c.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var item = user?._Cart?._Items?.FirstOrDefault(b => b.ProductId == productId);
                if (item == null)
                {
                    return new ApiResponse<CartItem>(false, "Product not found", null, "Check the information provided");
                }

                if (item.ProductQty > 1)
                {
                    // Decrease the quantity if it is greater than 1
                    item.ProductQty--;
                }
                else
                {
                    // Remove the item from the cart if the quantity reaches 1 and is decremented
                    user._Cart._Items.Remove(item);
                    item = null; // Since it's removed, set to null
                }

                await _context.SaveChangesAsync();

                if (item == null)
                {
                    return new ApiResponse<CartItem>(true, "Item removed from cart", null, null);
                }

                return new ApiResponse<CartItem>(true, "Quantity updated", item, null);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }





    }
}
