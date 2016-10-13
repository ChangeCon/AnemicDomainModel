using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Model.Clients
{
    public interface IClientRepository: IReadOnlyRepository<Client, int>
    {
    }
}
