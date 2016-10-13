using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Model.Clients.Discounts
{
    public class DesperadoDiscount : IDiscount
    {
        private readonly decimal _desperadoDiscount = 20;
        public decimal ApplyDiscountTo(decimal basePrice)
        {
            return basePrice - _desperadoDiscount;
        }
    }
}
