using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Dtos;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;

namespace MovieApp.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICustomCacheService _customCacheService;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IMemoryCache memoryCache, ICustomCacheService customCacheService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _customCacheService = customCacheService;
        }

        [LoggerAspect]
        [AuthorizationAspect("public")]
        public List<CategoryDto> GetCategories()
        {
            var key = "GetCategories";
            var value = _customCacheService.Get<List<CategoryDto>>(key);
            if (value != null)
                return value;
            else
            {
                var result = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
                _customCacheService.Create(key, result, TimeSpan.FromMinutes(1));
                return result;
            }
        }
    }
}
