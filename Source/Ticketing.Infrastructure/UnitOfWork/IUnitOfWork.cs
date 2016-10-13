using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterUpdated(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterInserted(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterDeleted(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void Commit();
    }

}
