using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Ticketing.Infrastructure.Domain.Events
{
    public class StructureMapDomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private IContainer _container;

        public StructureMapDomainEventHandlerFactory(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>
                                              (T domainEvent) where T : IDomainEvent
        {
            return _container.GetAllInstances<IDomainEventHandler<T>>();
        }
    }

}
