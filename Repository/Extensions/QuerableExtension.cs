using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Repository.Extensions
{
    public static class QuerableExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sortBy, bool isSortDesc) where T : class
        {
            if (string.IsNullOrEmpty(sortBy)) return source;

            var property = typeof(T).GetProperties().SingleOrDefault(x =>
             string.Equals(x.Name, sortBy, System.StringComparison.CurrentCultureIgnoreCase));
            if (property == null) return source;

            var orderDirection = isSortDesc ? "descending" : "ascending";
            return source.OrderBy($"{property.Name} {orderDirection}");
        }

        public static IQueryable<T> Contains<T>(this IQueryable<T> source, string filter, string[] columnsAllowed = null) where T : class
        {
            if (string.IsNullOrEmpty(filter)) return source;

            const string or = " || ";

            var properties = columnsAllowed == null
                ? typeof(T).GetProperties().ToList()
                : typeof(T).GetProperties().Where(x => columnsAllowed.Contains(x.Name)).ToList();

            var builder = new StringBuilder();

            var keyWorlds = GetkeyWorlds(filter);

            for (int i = 0; i < keyWorlds.Count; i++)
            {
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        builder.Append($"@{property.Name}.contains(@{i}) {or}");
                    }
                }
            }

            var stringPredicate = builder.ToString();
            if (string.IsNullOrEmpty(stringPredicate)) return source;

            stringPredicate = stringPredicate.Substring(0, stringPredicate.Length - or.Length);

            return source.Where(stringPredicate, keyWorlds.ToArray());

        }

        private static IList<string> GetkeyWorlds(string filter)
        {
            const char space = ' ';
            var keyWorlds = new List<string>();

            if (filter.Contains(space))
            {
                keyWorlds.AddRange(filter.Split(space).Where(x => !string.IsNullOrEmpty(x)));
            }
            else
            {
                keyWorlds.Add(filter);
            }

            return keyWorlds;
        }

        public static IQueryable<T> Take<T>(this IQueryable<T> source, int Page, int itemsPerPage)
        {
            if (itemsPerPage < 0) return source;

            if (Page <= 0)
            {
                Page = 1;
            }

            const int maximumItemsBydefault = 5;

            if (itemsPerPage == 0)
            {
                itemsPerPage = maximumItemsBydefault;
            }

            return source.Skip(itemsPerPage * (Page - 1)).Take(itemsPerPage);
        }

    }
}
