using System;
using System.Runtime.CompilerServices;

namespace Ticketing.Infrastructure.Caching
{
	public interface ICache
	{
		void Add<T>(T value, DateTimeOffset expiration, [CallerMemberName]string key = "");
		T Get<T>([CallerMemberName]string key = "");
	}
}
