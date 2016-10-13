using StructureMap;
using StructureMap.Configuration.DSL;
using Ticketing.Model.Clients;
using Ticketing.Repository;
using Ticketing.Services;
using Ticketing.Services.Contracts;

namespace Ticketing.IoC
{
	public class StructureMapBootStrapper
	{
        public static IContainer Initialize()
        {
            IContainer container = new Container(new KingICTRegistry());

            return container;
        }

		public static void ConfigureDependencies()
		{
			
        }

		public class KingICTRegistry : Registry
		{
			public KingICTRegistry()
			{
				// Services
				For<IClientService>().Use<ClientService>();
                For<IKioskService>().Use<KioskService>();
                For<IDesperadoService>().Use<DesperadoService>();

                // Repositories
                For<IClientRepository>().Use<ClientMockRepository>();
				
            }
        }
	}
}