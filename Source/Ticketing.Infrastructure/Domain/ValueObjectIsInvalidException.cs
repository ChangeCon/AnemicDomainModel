using System;
using System.Collections.Generic;

namespace Ticketing.Infrastructure.Domain
{
    public class ValueObjectIsInvalidException : Exception
    {
		private List<string> _separateMessages;

        public ValueObjectIsInvalidException(string message, List<string> separateMessages = null)
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
