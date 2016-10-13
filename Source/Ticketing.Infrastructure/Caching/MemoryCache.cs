using System;
using System.Runtime.CompilerServices;

namespace Ticketing.Infrastructure.Caching
{
	public class MemoryCache : ICache
	{
		public void Add<T>(T value, DateTimeOffset expiration, [CallerMemberName] string key = "")
		{
			System.Runtime.Caching.MemoryCache.Default.Add(key, value, expiration);
		}

		public T Get<T>([CallerMemberName] string key = "")
		{
			return (T)System.Runtime.Caching.MemoryCache.Default.Get(key);
		}
	}
}
