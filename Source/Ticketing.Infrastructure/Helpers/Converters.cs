
using System;

namespace Ticketing.Infrastructure.Helpers
{
	public static class Converters
	{
		public static DateTime GetFormattedDateTime(this DateTime value)
		{
			var formattedDateTimeString = value.ToString("yyyy-MM-dd HH:mm:ss");

			return DateTime.Parse(formattedDateTimeString);
		}
	}
}
