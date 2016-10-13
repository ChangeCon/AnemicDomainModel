using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticketing.Infrastructure.Querying
{
    public enum CriteriaOperator
    {
        Equal,
        LesserThanOrEqual,
        LesserThan,
        GreaterThanOrEqual,
        Greater,
        Different,
        Contains,
        DoesNotContain,
        In,
        InGroup,
        NotInGroup,
        NotApplicable
        
    }
}
