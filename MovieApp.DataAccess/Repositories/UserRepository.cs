using Microsoft.Extensions.Options;
using MovieApp.Data.Core;
using MovieApp.Data.Models;
using MovieApp.DataAccess.Repositories.IRepositories;
using MovieApp.DataAccess.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace MovieApp.DataAccess.Repositories
{
    public class UserRepository : DummyJsonDeserialize<User>, IUserRepository
    {
        private readonly AppSettings _appSettings;
        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User GetUserById(Guid id)
        {
            var users = GetJsonValues(_appSettings.WorkingDirectory, "Users");
            return users != null ? users.FirstOrDefault(x => x.Id == id) : null;
        }

        public User GetUserByUserName(string userName)
        {
            var users = GetJsonValues(_appSettings.WorkingDirectory, "Users");
            return users != null ? users.FirstOrDefault(x => x.UserName == userName) : null;
        }

        public void UpdateToken(string userName, string token)
        {
            var users = GetJsonValues(_appSettings.WorkingDirectory, "Users");
            var userDataPath = Path.Combine(_appSettings.WorkingDirectory, "DummyDatas", "Users.json");
            users.FirstOrDefault(x => x.UserName.Equals(userName)).Token = token;
            File.WriteAllText(userDataPath, JsonConvert.SerializeObject(users));
        }
    }
}
