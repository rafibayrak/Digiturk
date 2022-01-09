using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Extensions;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Film kategorilerinin tamami gonderilir
        /// </summary>
        /// <returns></returns>
        [HttpGet("getCategories")]
        public IActionResult GetCategories()
        {
            return this.NotFoundOrOk(_categoryService.GetCategories());
        }
    }
}
