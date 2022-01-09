using AutoMapper;
using MovieApp.Data.Dtos;
using MovieApp.Data.Models;

namespace MovieApp.Business.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
