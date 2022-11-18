using System.ComponentModel;

namespace Admin.Models.User
{
    public class SearchCriteria
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Display name")]
        public string DisplayName { get; set; }

        public int Page { get; set; }
    }
}