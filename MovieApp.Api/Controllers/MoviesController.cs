using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Extensions;
using MovieApp.Business.Services.IServices;
using System;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Id ye göre Filmin bilgilerini gonderir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getMovieById/{id}")]
        public IActionResult GetMovieById(Guid id)
        {
            var movieDtos = _movieService.GetMovieById(id);
            return this.NotFoundOrOk(movieDtos);
        }

        /// <summary>
        /// Kategorinin Id sine göre Filminlerin bilgilerini gonderir
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("getMoviesByCategoryId/{categoryId}")]
        public IActionResult GetMoviesByCategoryId(Guid categoryId)
        {
            var movieDtos = _movieService.GetMoviesByCategoryId(categoryId);
            return this.NotFoundOrOk(movieDtos);
        }

        /// <summary>
        /// Filmin oynatilabilir icerigini gondermektedir
        /// PhysicalFile fiziksel dosya yolundan parcalayarak aralikli olarak gonderilmesini saglamaktadir
        /// Dosya yolu olarak workingDirectory altinda Movies folder kullanilmaktadir
        /// frontend de video tagnin src alanina url verilmesi yeterlidir
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("contentPlay/{movieId}")]
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
