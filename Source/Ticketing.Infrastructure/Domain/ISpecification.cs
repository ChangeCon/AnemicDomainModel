using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}
