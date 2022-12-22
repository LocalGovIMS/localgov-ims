using System.ComponentModel;

namespace Admin.Models.Role
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Display name")]
        public string DisplayName { get; set; }
    }
}