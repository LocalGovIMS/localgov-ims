using System.Collections.Generic;

namespace BusinessLogic.Models.Shared
{
    public class SearchResult<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }

        public int Pages
        {
            get { return Count / PageSize; }
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}