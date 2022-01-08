using AutoMapper;
using MovieApp.Data.Dtos;
using MovieApp.Data.Models;

namespace MovieApp.Business.Mappers
{
    public class MovieMapper : Profile
    {
        public MovieMapper()
        {
            CreateMap<Movie, MovieDto>();
        }
    }
}
