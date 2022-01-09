using AutoMapper;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Dtos;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using System.Collections.Generic;

namespace MovieApp.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [LoggerAspect]
        [MemoryCacheAspect]
        [AuthorizationAspect("public")]
        public List<CategoryDto> GetCategories()
        {
            return _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
        }

        //CacheRemoveAspect("GetCategories")
        //AddCtg()
    }
}
