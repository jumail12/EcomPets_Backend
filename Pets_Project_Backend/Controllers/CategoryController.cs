using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Data.Models.CategoryModel.CategoryDto;
using Pets_Project_Backend.Services.Category_Services;

namespace Pets_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCat()
        {
            try
            {
                var categoryList= await _categoryServices.GetCategories();
                return Ok(new ApiResponse<IEnumerable<Category_Dto>>(true,"categories fetched",categoryList,null));
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCate(Cat_Add_dto newCate)
        {
            try
            {
                var res=await _categoryServices.AddCategory(newCate);
              return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [Authorize(Roles ="Admin")]
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete_Cat(int id)
        {
            try
            {
                var res=await _categoryServices.RemoveCategory(id);
               return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
