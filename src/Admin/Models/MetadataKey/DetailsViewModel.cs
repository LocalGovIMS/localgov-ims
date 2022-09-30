using BusinessLogic.Enums;
using System.ComponentModel;

namespace Admin.Models.MetadataKey
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Is a system type")]
        public bool IsASystemType { get; set; }

        [DisplayName("Entity type")]
        public MetadataKeyEntityType EntityType { get; set; }
    }
}