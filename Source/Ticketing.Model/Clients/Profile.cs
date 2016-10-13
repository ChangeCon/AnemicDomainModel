using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Model.Clients
{
    public class Profile : ValueObjectBase
    {
        public int Id { get; private set; }
        public string Caption { get; private set; }
        public string Description { get; private set; }

        public IEnumerable<Tariff> AvailableTariffs { get; private set; }

        public IEnumerable<Coupon> AvailableCoupons { get; private set; }


        public Profile(int id, string caption, string description, IEnumerable<Tariff> availableTariffs)
        {
            this.Id = id;
            this.Caption = caption;
            this.Description = description;
            this.AvailableTariffs = availableTariffs;

            this.AvailableCoupons = new List<Coupon>();

            ThrowExceptionIfInvalid();
        }

        public void SetAvailableCoupons(bool isDesperado, bool intendedForKioskSale)
        {
            ThrowExceptionIfInvalid();

            List<Coupon> generatedCoupons = new List<Coupon>();
            IEnumerable<Tariff> filteredTariffs;

            if (intendedForKioskSale)
                filteredTariffs = this.AvailableTariffs.Where(tariff => tariff.IsForSaleOnKiosk);
            else
                filteredTariffs = this.AvailableTariffs;
            
            foreach (var tariff in filteredTariffs)
            {
                //1. Recursive function to create coupon for each period (Monthly coupons)
                #region Logic to generate coupons from available tariffs 
                generatedCoupons.Add(new Coupon(DateTime.Now, DateTime.Now.AddYears(1), tariff, isDesperado));























































































                #endregion
            }
            this.AvailableCoupons = generatedCoupons;
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Caption))
                base.AddBrokenRule(ClientBusinessRules.ProfileCaptionValid);

            if (this.AvailableTariffs == null
                || this.AvailableTariffs.Any(tar => tar.GetBrokenRules().Count() > 0))
            {
                base.AddBrokenRule(ClientBusinessRules.AvailableTariffsValid);
            }
        }

    }
}
