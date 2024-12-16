using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.WhishListModel;
using Pets_Project_Backend.Data.Models.WhishListModel.WhishList_Dto;

namespace Pets_Project_Backend.Services.WhishList_Service
{
    public class WhishListService : IWhishListService
    { 
        private readonly ApplicationContext _context;

        public WhishListService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>> AddOrRemove(int u_id, int pro_id)
        {
            try
            {
                var isExists= await _context.WhishList
                    .Include(a=>a._Product)
                    .FirstOrDefaultAsync(b=>b.productId == pro_id && b.userId==u_id);

                if (isExists == null)
                {
                    var add_wish = new WhishList
                    {
                        userId=u_id,
                        productId=pro_id,
                    };

                    _context.WhishList.Add(add_wish);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Item added to the wishList","done",null);
                }
                else
                {
                    _context.WhishList.Remove(isExists);
                    await _context.SaveChangesAsync();
                   
                    return new ApiResponse<string>(true, "Item removed from wishList", "done", null);
                }
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the wishlist.", ex);
            }
        }

        public async Task<List<WhishList_View_Dto>> GetAllWishItems(int u_id)
        {
            try
            {
                var items = await _context.WhishList
                    .Include(a => a._Product)
                    .ThenInclude(b => b._Category)
                    .Where(c => c.userId == u_id)
                    .ToListAsync();

                if(items != null)
                {
                    var p=items.Select(a=> new WhishList_View_Dto
                    {
                        Id=a.whishId,
                        ProductId=a._Product.ProductId,
                        ProductName=a._Product.ProductName,
                        ProductDescription=a._Product.ProductDescription,
                        Price=a._Product.ProductPrice,
                        OfferPrice=a._Product.OfferPrize,
                        ProductImage=a._Product.ImageUrl,
                        CategoryName=a._Product._Category?.CategoryName
                    }).ToList();

                    return p;
                }
                else
                {
                    return new List<WhishList_View_Dto>();
                }

            }
            
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> RemovefromWishlist(int u_id, int pro_id)
        {
            try
            {
                var isExists = await _context.WhishList
                  .Include(a => a._Product)
                  .FirstOrDefaultAsync(b => b.productId == pro_id && b.userId == u_id);

                if (isExists != null)
                {
                    _context.WhishList .Remove(isExists);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Item removed from wishList", "done", null);
                }

                return new ApiResponse<string>(false, "Product not found", "", null);


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
