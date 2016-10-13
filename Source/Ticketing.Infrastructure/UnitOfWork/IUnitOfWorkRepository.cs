using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IAggregateRoot entity);
        void PersistUpdateOf(IAggregateRoot entity);
        void PersistDeletionOf(IAggregateRoot entity);
    }
}
