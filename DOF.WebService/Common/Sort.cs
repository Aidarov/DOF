using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Common
{
    public enum SortDirection
    {
        ASC = 1,
        DESC = 2
    }
    public class Sort
    {
        public string FieldName { get; set; }
        public SortDirection Direction { get; set; }
    }

    public static class SortHelper
    {
        public static IOrderedEnumerable<T> ApplySort<T>(this IQueryable<T> collection, IEnumerable<Sort> sorts)
        {
            IOrderedEnumerable<T> orderedCollection = null;

            foreach (var sort in sorts)
            {
                Func<T, object> orderByValue = sd => typeof(T).GetProperty(sort.FieldName).GetValue(sd);
                orderedCollection = sort.Direction == SortDirection.ASC ? collection.OrderBy(orderByValue) : collection.OrderByDescending(orderByValue);
            }

            return orderedCollection;
        }
    }
}
