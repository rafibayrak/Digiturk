using MovieApp.Data.Dtos;
using System.Collections.Generic;

namespace MovieApp.Business.Services.IServices
{
    public interface ICategoryService
    {
        public List<CategoryDto> GetCategories();
    }
}
