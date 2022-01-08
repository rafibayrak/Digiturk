using Microsoft.Extensions.Options;
using MovieApp.Data.Core;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using MovieApp.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.DataAccess.Repositories
{
    public class MovieRepository : DummyJsonDeserialize<Movie>, IMovieRepository
    {
        private readonly AppSettings _appSettings;

        public MovieRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Movie GetMovieById(Guid id)
        {
            var movies = GetJsonValues(_appSettings.WorkingDirectory, "Movies");
            return movies != null ? movies.Find(x => x.Id == id) : null;
        }

        public List<Movie> GetMoviesByCategoryId(Guid categoryId)
        {
            var movies = GetJsonValues(_appSettings.WorkingDirectory, "Movies");
            return movies != null ? movies.Where(x => x.CategoryId == categoryId).ToList() : new List<Movie>();
        }

        public List<Movie> GetTopTwentyMovies()
        {
            var movies = GetJsonValues(_appSettings.WorkingDirectory, "Movies");
            return movies != null ? movies.OrderByDescending(x => x.Point).Take(20).ToList() : new List<Movie>();
        }
    }
}
