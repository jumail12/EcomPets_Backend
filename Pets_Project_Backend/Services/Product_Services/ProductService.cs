using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.CloudinaryServices;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.ProductModel.Product_Dto;

namespace Pets_Project_Backend.Services.Product_Services
{
    public class ProductService : IProductServices
    {
        private readonly ApplicationContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(ApplicationContext context,ICloudinaryService clo)
        {
            _context = context;
            _cloudinaryService = clo;
        }

        public async Task<List<Product_with_Category_Dto>> HotDeals()
        {
            try
            {
                var productWithCategory=await _context.Products
                    .Where(a=>(a.ProductPrice-a.OfferPrize)>200)
                      .Select(a => new Product_with_Category_Dto
                      {
                          ProductId = a.ProductId,
                          ProductName = a.ProductName,
                          ProductDescription = a.ProductDescription,
                          ProductPrice = a.ProductPrice,
                          OfferPrize = a.OfferPrize,
                          Rating = a.Rating,
                          ImageUrl = a.ImageUrl,
                          StockId = a.StockId,
                          CategoryName = a._Category.CategoryName
                      }).ToListAsync();

                return productWithCategory;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product_with_Category_Dto>> FeturedPro()
        {
            try
            {
                var productWithCategory = await _context.Products
                    .Where(c=>c.Rating>4)
                    .Select(a => new Product_with_Category_Dto
                {
                    ProductId = a.ProductId,
                    ProductName = a.ProductName,
                    ProductDescription = a.ProductDescription,
                    ProductPrice = a.ProductPrice,
                    OfferPrize = a.OfferPrize,
                    Rating = a.Rating,
                    ImageUrl = a.ImageUrl,
                    StockId = a.StockId,
                    CategoryName = a._Category.CategoryName
                }).ToListAsync();


                return productWithCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task AddProduct(AddProduct_Dto addPro,IFormFile image)
        {
            try
            {
                string imageUrl= await _cloudinaryService.UploadImageAsync(image);
                var pro = new Product
                {
                    ProductName=addPro.ProductName,
                    ProductDescription=addPro.ProductDescription,
                    ProductPrice=addPro.ProductPrice,
                    OfferPrize=addPro.OfferPrize,
                    Rating=addPro.Rating,
                    CategoryId=addPro.CategoryId,
                    ImageUrl=imageUrl,
                };

                await _context.Products.AddAsync(pro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message);
            }
          
        }

        public async Task<List<Product_with_Category_Dto>> GetProducts()
        {
            try
            {
                var productWithCategory=await _context.Products.Select(a=> new Product_with_Category_Dto
                {
                    ProductId = a.ProductId,
                    ProductName = a.ProductName,
                    ProductDescription = a.ProductDescription,
                    ProductPrice = a.ProductPrice,
                    OfferPrize = a.OfferPrize,
                    Rating=a.Rating,
                    ImageUrl = a.ImageUrl,
                    StockId = a.StockId,
                    CategoryName=a._Category.CategoryName
                }).ToListAsync();

               
                    return productWithCategory;
  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product_with_Category_Dto> GetProductByID(int id)
        {
            try
            {

                var pro = await _context.Products.Select(a => new Product_with_Category_Dto
                {
                    ProductId = a.ProductId,
                    ProductName = a.ProductName,
                    ProductDescription = a.ProductDescription,
                    ProductPrice = a.ProductPrice,
                    OfferPrize=a.OfferPrize,
                    ImageUrl = a.ImageUrl,
                    Rating = a.Rating,
                    StockId = a.StockId,
                    CategoryName = a._Category.CategoryName
                }).FirstOrDefaultAsync(b => b.ProductId == id);

                if(pro == null)
                {
                    return null;
                }

                return pro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product_with_Category_Dto>> GetProductsByCategoryName(string Cat_name)
        {
            try
            {
                if (Cat_name.ToLower() == "all")
                {
                    var allpro = await _context.Products.Select(a => new Product_with_Category_Dto
                    {
                        ProductId = a.ProductId,
                        ProductName = a.ProductName,
                        ProductDescription = a.ProductDescription,
                        ProductPrice = a.ProductPrice,
                        OfferPrize=a.OfferPrize,
                        Rating=a.Rating,
                        ImageUrl = a.ImageUrl,
                        StockId = a.StockId,
                        CategoryName = a._Category.CategoryName
                    }).ToListAsync();

                    if (allpro == null)
                    {
                        return null;
                    }

                    return allpro;
                }

                var catP1 = await _context.Products
                    .Where(b => b._Category.CategoryName.ToLower() == Cat_name.ToLower())
                    .Select(a => new Product_with_Category_Dto
                    {
                        ProductId = a.ProductId,
                        ProductName = a.ProductName,
                        ProductDescription = a.ProductDescription,
                        ProductPrice = a.ProductPrice,
                        OfferPrize=a.OfferPrize,
                        Rating=a.Rating,    
                        ImageUrl = a.ImageUrl,
                        StockId = a.StockId,
                        CategoryName = a._Category.CategoryName
                    }).ToListAsync();
                if (catP1 == null)
                {
                    return null;
                }

                return catP1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var isExists=await _context.Products.FirstOrDefaultAsync(a=>a.ProductId == id);
                if(isExists == null)
                {
                    return false;
                }

                _context.Products.Remove(isExists);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdatePro(int id, AddProduct_Dto addPro, IFormFile? image)
        {
            try
            {
                var pro= await _context.Products.FirstOrDefaultAsync(x=>x.ProductId == id);
                var CatExists = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == addPro.CategoryId);
                if(CatExists == null)
                {
                    throw new Exception("Category not found");
                }

                if (pro != null)
                {
                    pro.ProductName = addPro.ProductName;
                    pro.ProductDescription = addPro.ProductDescription;
                    pro.ProductPrice = addPro.ProductPrice; 
                    pro.CategoryId = addPro.CategoryId;
                    pro.Rating = addPro.Rating;
                    pro.OfferPrize = addPro.OfferPrize;
                    pro.CategoryId = addPro.CategoryId;

                  

                    if(image != null && image.Length>0)
                    {
                        string imgUrl=await _cloudinaryService.UploadImageAsync(image);
                        pro.ImageUrl = imgUrl;
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Product with ID: {id} not found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<Product_with_Category_Dto>> SearchProduct(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return new List<Product_with_Category_Dto> { new Product_with_Category_Dto() };
                }

                var pro=await _context.Products.Where(a=>a.ProductName.ToLower().Contains(search.ToLower()))
                    .Select(b=>new Product_with_Category_Dto
                    {
                        ProductId = b.ProductId,
                        ProductName=b.ProductName,
                        ProductDescription=b.ProductDescription,
                        ProductPrice=b.ProductPrice,
                        OfferPrize=b.OfferPrize,
                        Rating=b.Rating,
                        ImageUrl=b.ImageUrl,
                        StockId=b.StockId,
                        CategoryName=b._Category.CategoryName
                        
                    }).ToListAsync();

                return pro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
