using MovieApp.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieApp.DataAccess.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        public Movie GetMovieById(Guid id);
        public List<Movie> GetMoviesByCategoryId(Guid categoryId);
        public List<Movie> GetTopTwentyMovies();
    }
}
