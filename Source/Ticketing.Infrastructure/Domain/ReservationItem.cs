using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Domain
{
	public class ReservationItem<TValue>
	{
		public Guid Key { get; set; }

		public TValue Value { get; set; }

		public DateTime ExpiryPeriod { get; set; }

		public bool HasBeenPersisted { get; set; }

		public bool HasExpired()
		{
			return DateTime.Now > ExpiryPeriod;
		}

		public bool IsStillActive()
		{
			return !HasBeenPersisted && !HasExpired();
		}
	}
}
