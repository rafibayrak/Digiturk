using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public DashboardController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("getTopTwentyMovie")]
        public IActionResult GetTopTwentyMovie()
        {
            return Ok(_movieService.GetTopTwentyMovies());
        }
    }
}
