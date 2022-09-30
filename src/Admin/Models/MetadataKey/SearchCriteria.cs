using BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.MetadataKey
{
    public class SearchCriteria
    {
        public string Name { get; set; }

        [Display(Name="Entity type")]
        public MetadataKeyEntityType? EntityType { get; set; }

        public int Page { get; set; }
    }
}