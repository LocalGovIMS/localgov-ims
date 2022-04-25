using BusinessLogic.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;
using Web.Mvc.DataAnnotations;

namespace Admin.Models.AccountReferenceValidator
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Min length")]
        [Range(1, 36)]
        public short MinLength { get; set; }

        [Required]
        [DisplayName("Max length")]
        [Range(1, 36)]
        [GreaterThan(otherProperty:"MinLength", OtherPropertyDisplayName = "Min length", AllowEquality = true)]
        public short MaxLength { get; set; }

        [StringLength(30, ErrorMessage = "Regex cannot be longer than 30 characters")]
        public string Regex { get; set; }

        [Required]
        [DisplayName("Input mask")]
        [StringLength(36, ErrorMessage = "Input mask cannot be longer than 36 characters")]
        public string InputMask { get; set; }

        [Required]
        [DisplayName("Character type")]
        public CharacterType CharacterType { get; set; }

        [DisplayName("Check digit configuration")]
        public int? CheckDigitConfigurationId { get; set; }

        public SelectList CharacterTypes { get; set; }
        public SelectList CheckDigitConfigurations { get; set; }
    }
}