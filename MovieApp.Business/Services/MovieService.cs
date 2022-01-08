using AutoMapper;
using Microsoft.Extensions.Options;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Core;
using MovieApp.Data.Dtos;
using MovieApp.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieApp.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppSettings _appSettings;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IOptions<AppSettings> appSettings, IMovieRepository movieRepository, IMapper mapper = null)
        {
            _appSettings = appSettings.Value;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [MemoryCacheAspect]
        public MovieDto GetMovieById2(Guid id)
        {
            return _mapper.Map<MovieDto>(_movieRepository.GetMovieById(id));
        }

        public List<MovieDto> GetMoviesByCategoryId(Guid categoryId)
        {
            return _mapper.Map<List<MovieDto>>(_movieRepository.GetMoviesByCategoryId(categoryId));
        }

        public string GetMoviePath(string movieId)
        {
            var filePath = Path.Combine(_appSettings.WorkingDirectory, "Movies", movieId);
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            return filePath;
        }

        public List<MovieDto> GetTopTwentyMovies()
        {
            return _mapper.Map<List<MovieDto>>(_movieRepository.GetTopTwentyMovies());
        }
    }
}
