using Microsoft.Extensions.Options;
using MovieApp.Data.Core;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using MovieApp.DataAccess.Utilities;
using System.Collections.Generic;

namespace MovieApp.DataAccess.Repositories
{
    public class CategoryRepository : DummyJsonDeserialize<Category>, ICategoryRepository
    {
        private readonly AppSettings _appSettings;

        public CategoryRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<Category> GetCategories()
        {
            var categories = GetJsonValues(_appSettings.WorkingDirectory, "Categories");
            return categories;
        }
    }
}
