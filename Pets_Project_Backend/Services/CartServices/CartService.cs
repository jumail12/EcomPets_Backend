using Microsoft.EntityFrameworkCore;
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
                    Price = (int?)item._Product.ProductPrice,
                    ProductImage = item._Product.ImageUrl,
                    TotalAmount = (int?)(item._Product.ProductPrice * item.ProductQty),
                    Quantity = item.ProductQty
                })
                .ToList();
        }


        public async Task<bool> AddToCart(int userId, int productId)
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
                    throw new ArgumentException("User not found!");
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
                    throw new ArgumentException("Item alredy in your cart!");
                   
                }

                //chechking he sock of product
                var pro=await _context.Products.FirstOrDefaultAsync(a=>a.ProductId==productId);
                if (pro == null || pro?.StockId <= 0)
                {
                    throw new ArgumentException("Product not found or out of stock.");
                   
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

              return true;
            }


            catch (ArgumentException)
            {
                throw; // Rethrow specific exceptions.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //remove from cat
        public async Task<bool> RemoveFromCart(int userId, int productId)
        {
            try
            {
                var user =await _context.Users.Include(a=>a._Cart)
                    .ThenInclude(b=>b._Items)
                    .FirstOrDefaultAsync(c=>c.Id==userId);

                if(user == null)
                {
                    return false;
                }

                var pro_check= user._Cart?._Items?.FirstOrDefault(a=>a.ProductId==productId);
                if(pro_check == null)
                {
                    return false;
                }

               _context.CartItems.Remove(pro_check);
                await _context.SaveChangesAsync();
                return true;
            }
           
            catch(Exception ex)
            {
                throw new InvalidOperationException("An error occurred while removing the item from the cart.", ex);
            }
        }

        public async Task<bool> IncreaseQuantity(int userId, int pro_id)
        {
            try
            {
               
                var user=await _context.Users.Include(a=>a._Cart)
                    .ThenInclude(b=>b._Items)
                    .ThenInclude(c=>c._Product)
                    .FirstOrDefaultAsync (c=>c.Id==userId);

               if(user == null) 
                {
                    throw new ArgumentException("User not found");
                }

               //var proExists=user._Cart?._Items?.FirstOrDefault(a=>a.ProductId == pro_id);
               // if(proExists==null)
               // {
               //     throw new ArgumentException("Product not found");
               // }

                var item =user._Cart?._Items?.FirstOrDefault(b=>b.ProductId==pro_id);
                if (item==null)
                {
                    throw new ArgumentException("Product not found in cart");
                }

                if (item.ProductQty >= 10)
                {
                    throw new ArgumentException("You reach max quantity (10)");
                }

                if(item.ProductQty >= item._Product?.StockId)
                {
                    throw new ArgumentException("Out of stock!");
                }

                item.ProductQty++;
                await _context.SaveChangesAsync();
                return true;
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
        public async Task<bool> DecreaseQuantity(int userId, int productId)
        {
            try
            {
                var user =await _context.Users.Include(a=>a._Cart)
                    .ThenInclude(b=>b._Items)
                    .ThenInclude(c=>c._Product)
                    .FirstOrDefaultAsync(c=>c.Id==userId);

               if(user==null)
                {
                    throw new Exception("User not found");
                }

               var item =user?._Cart?._Items?.FirstOrDefault(b=>b.ProductId==productId);
                if (item==null)
                {
                    return false;
                }

                if (item.ProductQty > 1)
                {
                    item.ProductQty--;
                }
                else
                {
                    item.ProductQty = 1;
                }

                await _context.SaveChangesAsync();
                return true;
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
