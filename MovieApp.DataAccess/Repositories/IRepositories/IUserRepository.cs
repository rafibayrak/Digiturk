using MovieApp.Data.Models;
using System;

namespace MovieApp.DataAccess.Repositories.IRepositories
{
    public interface IUserRepository
    {
        public User GetUserById(Guid id);
        public User GetUserByUserName(string userName);
    }
}
