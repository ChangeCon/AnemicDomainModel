using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Ticketing.IoC
{
    public class StructureMapInstanceProvider: IInstanceProvider
    {
        private readonly Type _serviceType;

        public StructureMapInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var container = StructureMapBootStrapper.Initialize();

            var instance = container.GetInstance(_serviceType);

            return instance;
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

        }
    }
}