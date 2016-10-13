using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;

namespace Ticketing.Model.Clients
{
    public class Tariff: ValueObjectBase
    {
        public int Id { get; private set; }

        public bool IsForSaleOnKiosk { get; private set; }

        public string Caption { get; private set; }

        public DateTime ValidFrom { get; private set; }

        public DateTime? ValidTo { get; private set; }

        public PeriodType PeriodType { get; private set; }

        public decimal BasePrice { get; private set; }

        public Tariff(int id, bool isForSaleOnKiosk, string caption, DateTime validFrom, DateTime? validTo, PeriodType periodType, decimal basePrice)
        {
            this.Id = id;
            this.IsForSaleOnKiosk = isForSaleOnKiosk;
            this.Caption = caption;
            this.ValidFrom = validFrom;
            this.ValidTo = validTo;
            this.PeriodType = periodType;
            this.BasePrice = basePrice;
        }

        public override void Validate()
        {
            
        }
    }
}
