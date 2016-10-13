using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Model.Clients
{
    public class ClientBusinessRules
    {
        public static readonly BusinessRule FirstNameRequired = 
            new BusinessRule("FirstName", "Client must have a valid first name ");

        public  static readonly BusinessRule LastNameRequired = 
            new BusinessRule("LastName", "Client must have a valid last name.");

        public static readonly BusinessRule ClientMustHaveAtLeastOneProfile =
            new BusinessRule("Profiles", "Client must have at least one profile.");

        public static readonly BusinessRule CouponTariffMustBeValid =
            new BusinessRule("Coupon.Tariff", "Tariff in a Coupon must be valid.");

        public static readonly BusinessRule ProfileCaptionValid =
            new BusinessRule("Profile.Caption", "Profile must have a valid caption.");

        public static readonly BusinessRule AvailableTariffsValid =
            new BusinessRule("Profile.AvailableTariffs", "Profile mus have at least one valid tariff");
    }
}
