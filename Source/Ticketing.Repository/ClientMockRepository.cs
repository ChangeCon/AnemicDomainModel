using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Querying;
using Ticketing.Model.Clients;

namespace Ticketing.Repository
{
    public class ClientMockRepository : IClientRepository
    {
        public IEnumerable<Client> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> FindBy(Query query)
        {
            throw new NotImplementedException();
        }

        public Client FindBy(int id)
        {
            return FillDemoClient();
        }

        public IEnumerable<Client> FindBy(Query query, int index, int count)
        {
            throw new NotImplementedException();
        }

        private Client FillDemoClient()
        {
            return new Client(1, "Jura", "Klafura", FillDemoProfile());
            
        }

        private List<Profile> FillDemoProfile()
        {
            List<Profile> result = new List<Profile>();

            result.Add(new Profile(1, "Student", "Profile for students", FillDemoAvailableTariffs()));
            

            return result;
        }

        private List<Tariff> FillDemoAvailableTariffs()
        {
            List<Tariff> result = new List<Tariff>();


            result.Add(new Tariff(10, true, "Year Coupon", DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), PeriodType.Year, 24));
            
            return result;
        }

        
    }
}
