using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ticketing.Services.Views
{
    [DataContract]
    public class Coupon
    {
        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public DateTime ValidTo { get; set; }
        [DataMember]
        public int  TariffId { get; set; }
        [DataMember]
        public string TariffName { get; set; }
        [DataMember]
        public decimal Price { get; set; }

    }
}
