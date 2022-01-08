﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using System;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeVerifyToken]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("getMovieById/{id}")]
        public IActionResult GetMovieById(Guid id)
        {
            var movieDtos = _movieService.GetMovieById(id);
            return Ok(movieDtos);
        }

        [HttpGet("getMoviesByCategoryId/{categoryId}")]
        public IActionResult GetMoviesByCategoryId(Guid categoryId)
        {
            var movieDtos = _movieService.GetMoviesByCategoryId(categoryId);
            return Ok(movieDtos);
        }

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