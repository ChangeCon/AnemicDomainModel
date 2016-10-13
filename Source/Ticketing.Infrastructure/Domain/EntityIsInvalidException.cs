using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Domain
{
    public class EntityIsInvalidException: Exception
    {
		private List<string> _separateMessages;

		public EntityIsInvalidException(string message, List<string> separateMessages = null)
            : base(message)
        {
			_separateMessages = separateMessages;
        }

		public List<string> SeparateMessages
		{
			get { return _separateMessages; }
		}
    }
}
