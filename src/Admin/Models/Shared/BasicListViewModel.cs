using System.Collections.Generic;

namespace Admin.Models.Shared
{
    public class BasicListViewModel
    {
        public string ListTitle { get; set; }
        public string ColumnTitle { get; set; }
        public string NoItemsMessage { get; set; } = "No items to show";
        public List<string> Items { get; set; }
    }
}