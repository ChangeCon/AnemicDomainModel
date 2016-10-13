using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticketing.Infrastructure.Domain
{
    public interface IRootRepository<T, TId> where T: IAggregateRoot
    {

    }
}
