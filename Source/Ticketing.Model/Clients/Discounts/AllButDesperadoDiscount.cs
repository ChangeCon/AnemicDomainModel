using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Model.Clients.Discounts
{
    public class AllButDesperadoDiscount : IDiscount
    {
        private readonly decimal _discount = 25;
        public decimal ApplyDiscountTo(decimal basePrice)
        {
            return basePrice - _discount;
        }
    }
}
