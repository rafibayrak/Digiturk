using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using System;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AuthorizeVerifyToken]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("getMovieById/{id}")]
        [Logger]
        public IActionResult GetMovieById(Guid id)
        {
            var movieDtos = _movieService.GetMovieById2(id);
            return Ok(movieDtos);
        }

        [HttpGet("getMoviesByCategoryId/{categoryId}")]
        [Logger]
        public IActionResult GetMoviesByCategoryId(Guid categoryId)
        {
            var movieDtos = _movieService.GetMoviesByCategoryId(categoryId);
            return Ok(movieDtos);
        }

        [HttpGet("contentPlay/{movieId}")]
        [Logger]
        public IActionResult ContentPlay(Guid movieId)
        {
            var moviePath = _movieService.GetMoviePath(movieId.ToString());
            if (string.IsNullOrEmpty(moviePath))
            {
                return NotFound();
            }

            return PhysicalFile(moviePath, "application/octet-stream", enableRangeProcessing: true);
        }
    }
}
