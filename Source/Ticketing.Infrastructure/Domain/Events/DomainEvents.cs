using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Ticketing.Infrastructure.Domain.Events
{
    public static class DomainEvents
    {
        public static IDomainEventHandlerFactory DomainEventHandlerFactory;

        public static void Initialize(IContainer container)
        {
            if (DomainEventHandlerFactory == null)
                DomainEventHandlerFactory = new StructureMapDomainEventHandlerFactory(container);
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            DomainEventHandlerFactory.GetDomainEventHandlersFor(domainEvent)
                                                    .ForEach(h => h.Handle(domainEvent));
        }
    }
}
