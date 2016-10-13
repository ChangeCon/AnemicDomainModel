using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticketing.Infrastructure.Domain.Events
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }
    }

}
