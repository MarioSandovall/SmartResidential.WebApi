
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Utils
{
    public static class ColumnFilter
    {
        public static string[] GetNames<T>(params Expression<Func<T, string>>[] filters) where T : class
        {
            var colums = new List<string>();

            foreach (var filter in filters)
            {
                colums.Add(GetPropertyName(filter));
            }

            return colums.ToArray();
        }

        private static string GetPropertyName<T>(Expression<Func<T, string>> propertySelector) where T : class
        {
            string propertyName;
            if (propertySelector.Body is MemberExpression expresion)
            {
                propertyName = expresion.Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)propertySelector.Body).Operand;
                propertyName = ((MemberExpression)op).Member.Name;

            }
            return propertyName;

        }
    }
}
