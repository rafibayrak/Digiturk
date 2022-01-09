using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Anasayfa icin filmlerin bir Point sistemine sahip oldugu varsayilarak
        /// En yuksek Pointe sahip 20 film gonderildi
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTopTwentyMovies")]
        public IActionResult GetTopTwentyMovies()
        {
            return Ok(_movieService.GetTopTwentyMovies());
        }
    }
}
