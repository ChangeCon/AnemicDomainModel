using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Ticketing.Model.Clients;
using Views = Ticketing.Services.Views;

namespace Ticketing.Services.Mapping
{
    public static class ClientMapper
    {
        public static Views.Client MapToViewClient(this Models.Client model   )
        {
            Views.Client result = new Views.Client{
                FirstName = model.FirstName,
                Id = model.Id,
                LastName = model.LastName
            };

            List<Views.Coupon> coupons = new List<Views.Coupon>();

            foreach (var profile in model.Profiles)
            {
                coupons.AddRange(profile.MapToViewCoupons());
            }

            result.AvailableCoupons = coupons;

            return result;

        }

        public static IEnumerable<Views.Coupon> MapToViewCoupons(this Models.Profile model)
        {
            if (model == null)
                return null;

            List<Views.Coupon> coupons = new List<Views.Coupon>();

            foreach (var coupon in model.AvailableCoupons)
            {
                coupons.Add(coupon.MapToViewCoupon());
            }

            return coupons;
        }

        public static Views.Coupon MapToViewCoupon(this Models.Coupon model)
        {
            if (model == null)
                throw new Exception("Can not map null objects");

            return new Views.Coupon
            {
                TariffId = model.Tariff.Id,
                TariffName = model.Tariff.Caption,
                ValidFrom = model.ValidFrom,
                ValidTo = model.ValidTo
            };


        }
    }
}
