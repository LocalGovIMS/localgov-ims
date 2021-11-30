using System.Collections.Generic;

namespace Admin.Models.Shared
{
    public class BasicListViewModel
    {
        public string ListTitle { get; set; }
        public string ColumnTitle { get; set; }
        public List<string> Items { get; set; }
    }
}