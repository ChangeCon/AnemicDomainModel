using System;

namespace Ticketing.Infrastructure.Messaging
{
	public interface IRequest
	{
		Guid RequestToken { get; set; }
	}
}