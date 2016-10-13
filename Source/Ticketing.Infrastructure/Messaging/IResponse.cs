using System;

namespace Ticketing.Infrastructure.Messaging
{
	public interface IResponse
	{
		Guid ResponseToken { get; set; }
		bool Success { get; set; }
		string Message { get; set; }
	}
}