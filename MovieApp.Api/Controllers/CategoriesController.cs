using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeVerifyToken]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getCategories")]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetCategories());
        }
    }
}
