using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Domain
{
	public interface IReservationCache<TValue>
	{
		IEnumerable<Guid> GetAllKeys();

		IEnumerable<ReservationItem<TValue>> GetAllValues();

		ReservationItem<TValue> Get(Guid key);

		void Save(ReservationItem<TValue> item);

		void Update(ReservationItem<TValue> item);

		void Delete(ReservationItem<TValue> item);
	}
}
