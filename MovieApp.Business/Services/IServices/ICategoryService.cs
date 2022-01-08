using MovieApp.Data.Models;
using System.Collections.Generic;

namespace MovieApp.Business.Services.IServices
{
    public interface ICategoryService
    {
        public List<Category> GetCategories();
    }
}
