using MovieApp.Business.Services.IServices;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using System.Collections.Generic;

namespace MovieApp.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }
    }
}
