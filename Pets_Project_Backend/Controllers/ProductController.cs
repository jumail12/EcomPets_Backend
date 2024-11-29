using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.Data.Models.ProductModel.Product_Dto;
using Pets_Project_Backend.Services.Product_Services;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _Services;

        public ProductController(IProductServices services)
        {
            _Services = services;
        }

        //get all
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products=await _Services.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //get by id
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult>  GetById(int id)
        {
            try
            {
                var p=await _Services.GetProductByID(id);
                return Ok(p);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //get products by cat name
        [HttpGet("getByCategoryName")]
        public async Task<IActionResult> GetByCateName(string CatName)
        {
            try
            {
                var p=await _Services.GetProductsByCategoryName(CatName);
                return Ok(p);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //delete
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeletePro(int id)
        {
            try
            {
                bool res=await _Services.DeleteProduct(id);
                if (res)
                {
                    return Ok("Product deleted");
                }
                return NotFound("Product Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //search
        [HttpGet("search-item")]
        public async Task<IActionResult> SearchPro(string search)
        {
            try
            {
                var res= await _Services.SearchProduct(search);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //add
        [Authorize(Roles ="Admin")]
        [HttpPost("Add_Pro")]
        public async Task<IActionResult> AddPro([FromForm] AddProduct_Dto new_pro ,IFormFile image)
        {
            try
            {

                await _Services.AddProduct(new_pro, image);
                return Ok("Product added Successfully!");
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        //update
        [Authorize(Roles="Admin")]
        [HttpPut("Update_Pro/{id}")]
        public async Task<IActionResult> Update_pro( int id ,[FromForm]AddProduct_Dto updateProduct_Dto, IFormFile image)
        {
            try
            {
                await _Services.UpdatePro(id, updateProduct_Dto, image);
                return Ok("product updated");
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


    }
}
