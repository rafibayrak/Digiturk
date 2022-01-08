using MovieApp.Data.Dtos;
using System;
using System.Collections.Generic;

namespace MovieApp.Business.Services.IServices
{
    public interface IMovieService
    {
        public MovieDto GetMovieById2(Guid id);
        public List<MovieDto> GetMoviesByCategoryId(Guid categoryId);
        public string GetMoviePath(string movieId);
        public List<MovieDto> GetTopTwentyMovies();
    }
}
