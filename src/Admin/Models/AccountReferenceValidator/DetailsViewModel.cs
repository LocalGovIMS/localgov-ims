using BusinessLogic.Enums;
using System.ComponentModel;

namespace Admin.Models.AccountReferenceValidator
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Min length")]
        public short MinLength { get; set; }

        [DisplayName("Max length")]
        public short MaxLength { get; set; }

        public string Regex { get; set; }
        
        [DisplayName("Input mask")]
        public string InputMask { get; set; }

        [DisplayName("Character type")]
        public CharacterType CharacterType { get; set; }

        [DisplayName("Check digit configuration")]
        public int? CheckDigitConfigurationId { get; set; }

        [DisplayName("Check digit configuration")]
        public string CheckDigitConfigurationName { get; set; }
    }
}