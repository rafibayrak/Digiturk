using System;
using System.Threading.Tasks;

namespace MovieApp.Business.Services.IServices
{
	public interface ICustomCacheService
	{
		public T Get<T>(string key);
		public void Create(string key, object data, TimeSpan timeSpan);
        public Task<T> GetAsync<T>(string key);
        public Task CreateAsync(string key, object data, TimeSpan timeSpan);
        public void Remove(string key);
		public void Clear();

    }
}

