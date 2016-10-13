using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Model.Clients.Discounts
{
    public interface IDiscount
    {
        decimal ApplyDiscountTo(decimal basePrice);
    }
}
