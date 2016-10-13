using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticketing.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(string message);
    }

}
