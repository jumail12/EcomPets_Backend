using Pets_Project_Backend.Data.Models.ProductModel.Product_Dto;

namespace Pets_Project_Backend.Services.Product_Services
{
    public interface IProductServices
    {
        Task AddProduct(AddProduct_Dto addPro, IFormFile image);
        Task<List<Product_with_Category_Dto>> GetProducts();
        Task<List<Product_with_Category_Dto>> FeturedPro();
        Task<Product_with_Category_Dto> GetProductByID (int id);
        Task<List<Product_with_Category_Dto>> GetProductsByCategoryName (string Cat_name);
        Task<bool> DeleteProduct(int id);
        Task UpdatePro(int id, AddProduct_Dto addPro, IFormFile image);
        Task<List<Product_with_Category_Dto>> SearchProduct(string search);

    }
}
