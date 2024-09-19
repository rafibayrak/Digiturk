using System;
using System.Threading.Tasks;

namespace MovieApp.Business.Services.IServices
{
    public interface IRedisCacheService
    {
        string GetValue(string key);
        bool SetValue(string key, string value, TimeSpan timeSpan);
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value, TimeSpan timeSpan);
        Task Clear(string key);
        void ClearAll();
    }
}

