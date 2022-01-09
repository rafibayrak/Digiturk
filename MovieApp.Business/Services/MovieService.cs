using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MovieApp.Business.Aspects;
using MovieApp.Business.Extensions;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Core;
using MovieApp.Data.Dtos;
using MovieApp.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieApp.Business.Services
{
    [AuthorizationAspect("public")]
    public class MovieService : IMovieService
    {
        private readonly AppSettings _appSettings;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieService(IOptions<AppSettings> appSettings, IMovieRepository movieRepository, IMapper mapper = null, IHttpContextAccessor httpContextAccessor = null)
        {
            _appSettings = appSettings.Value;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [LoggerAspect]
        [MemoryCacheAspect]
        public MovieDto GetMovieById(Guid id)
        {
            return _mapper.Map<MovieDto>(_movieRepository.GetMovieById(id));
        }

        [LoggerAspect]
        [MemoryCacheAspect]
        public List<MovieDto> GetMoviesByCategoryId(Guid categoryId)
        {
            var movies = _movieRepository.GetMoviesByCategoryId(categoryId);
            if (movies == null)
            {
                return null;
            }

            var dataTableParameters = _httpContextAccessor.GetDataTableParameter();
            movies = string.IsNullOrEmpty(dataTableParameters.Filter) ? movies : movies.Where(x => x.Name.Contains(dataTableParameters.Filter)).ToList();
            movies = movies.Skip(dataTableParameters.PageIndex).Take(dataTableParameters.PageSize).ToList();
            return _mapper.Map<List<MovieDto>>(movies);
        }

        [LoggerAspect]
        [MemoryCacheAspect]
        public string GetMoviePath(string movieId)
        {
            var filePath = Path.Combine(_appSettings.WorkingDirectory, "Movies", $"{movieId}.mp4");
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            return filePath;
        }

        [LoggerAspect]
        [MemoryCacheAspect]
        public List<MovieDto> GetTopTwentyMovies()
        {
            return _mapper.Map<List<MovieDto>>(_movieRepository.GetTopTwentyMovies());
        }
    }
}
