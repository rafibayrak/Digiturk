using MovieApp.Data.Models;
using System.Collections.Generic;

namespace MovieApp.DataAccess.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
    }
}
