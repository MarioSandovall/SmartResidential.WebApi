using System.Collections.Generic;

namespace Model.Models
{
    public class PaginatedList<T>
    {
        public int TotalOfItems { get; }
        public IEnumerable<T> Items { get; }

        public PaginatedList(IEnumerable<T> items, int totalOfItems)
        {
            Items = items;
            TotalOfItems = totalOfItems;
        }
    }
}
