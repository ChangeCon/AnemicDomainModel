using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Model.Clients
{

    public class Client : EntityBase<int>, IAggregateRoot
    {

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        public IEnumerable<Profile> Profiles { get; private set; }
        public IEnumerable<Coupon> PurchasedCoupons { get; private set; }

        public Client(int id, string firstName, string lastName, IEnumerable<Profile> profiles)
        {
            SetClient(id, firstName, lastName, profiles);
        }

        public void SetClient(int id, string firstName, string lastName, IEnumerable<Profile> profiles)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Profiles = profiles;

            base.ThrowExceptionIfInvalid();

            
        }

        public void GenerateCoupons(bool isDesperado, bool intendedForKioskSale)
        {
            ThrowExceptionIfInvalid();

            foreach (var profile in this.Profiles)
            {
                profile.SetAvailableCoupons(isDesperado, intendedForKioskSale);
            }
        }
        
        

        

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.FirstName))
                base.AddBrokenRule(ClientBusinessRules.FirstNameRequired);

            if (string.IsNullOrWhiteSpace(this.LastName))
                base.AddBrokenRule(ClientBusinessRules.LastNameRequired);

            if (this.Profiles == null || this.Profiles.Count() == 0)
                base.AddBrokenRule(ClientBusinessRules.ClientMustHaveAtLeastOneProfile);
        }
    }
}
