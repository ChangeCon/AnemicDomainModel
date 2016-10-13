using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Messaging;

namespace Ticketing.Infrastructure.Domain
{
	public interface IHistoryMessageHandler<TRequest, TResponse>
		where TRequest : RequestBase, new()
		where TResponse : ResponseBase<TRequest>, new()
	{
		bool IsAUniqueRequest(Guid requestToken);

		void LogResponse(Guid requestToken, TResponse response);

		TResponse RetrievePreviousResponseFor(Guid requestToken);
	}
}
