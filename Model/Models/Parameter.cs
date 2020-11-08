namespace Model.Models
{
    public class Parameter
    {
        public int Page { get; set; }

        public string Filter { get; set; }

        public string SortBy { get; set; }

        public bool IsSortDesc { get; set; }

        public int ItemsPerPage { get; set; }

    }
}
