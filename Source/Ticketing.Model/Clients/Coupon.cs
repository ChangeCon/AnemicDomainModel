using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;
using Ticketing.Model.Clients.Discounts;

namespace Ticketing.Model.Clients
{
    public class Coupon: ValueObjectBase 
    {
        public DateTime ValidFrom { get; private set; }

        public DateTime ValidTo { get; private set; }

        public Tariff Tariff { get; private set; }

        public decimal Price { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="isDesperado">Dependency Inversion principle => constructor injection</param>
        /// <returns></returns>
        public Coupon(DateTime validFrom, DateTime validTo, Tariff tariff, bool isDesperado)
        {
            this.ValidFrom = validFrom;
            this.ValidTo = validTo;
            this.Tariff = tariff;

            base.ThrowExceptionIfInvalid();

            CalculatePrice(isDesperado);

            //Only one place to implement business rule
            EnsurePriceIsNotSubZero();
        }
        
        private void CalculatePrice(bool isDesperado)
        {
            IDiscount discount = DiscountFactory.GetDiscount(isDesperado);

            this.Price = discount.ApplyDiscountTo(this.Tariff.BasePrice);
        }

        private void EnsurePriceIsNotSubZero()
        {
            if (this.Price < 0)
                this.Price = 0;
        }

        public override void Validate()
        {
            if (this.Tariff == null
                || this.Tariff.GetBrokenRules().Count() > 0)
            {
                base.AddBrokenRule(ClientBusinessRules.CouponTariffMustBeValid);
            }
        }
    }
}
