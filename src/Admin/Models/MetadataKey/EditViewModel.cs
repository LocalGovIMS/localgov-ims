using BusinessLogic.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.MetadataKey
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DisplayName("Is a system type")]
        [Required(ErrorMessage = "Is a system type is required")]
        public bool IsASystemType { get; set; }

        [DisplayName("Entity type")]
        [Required(ErrorMessage = "Entity type is required")]
        public MetadataKeyEntityType EntityType { get; set; }
    }
}