using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Configuration
{
	public interface IConfigurationProvider
	{
		System.Collections.Specialized.NameValueCollection AcquireConfiguration();
	}
}
