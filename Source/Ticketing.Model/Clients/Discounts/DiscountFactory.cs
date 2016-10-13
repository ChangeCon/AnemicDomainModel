using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Model.Clients.Discounts
{
    public class DiscountFactory
    {
        public static IDiscount GetDiscount(bool isDesperado)
        {
            if (isDesperado)
                return new DesperadoDiscount();
            else
                return new AllButDesperadoDiscount();

        }
    }
}
