using BusinessLogic.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class AccountReferenceValidator
    {
        public AccountReferenceValidator()
        {
            Funds = new HashSet<Fund>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public short MinLength { get; set; }

        public short MaxLength { get; set; }

        [StringLength(30)]
        public string Regex { get; set; }

        [StringLength(30)]
        public string InputMask { get; set; }

        public CharacterType? CharacterType { get; set; }

        public int? CheckDigitConfigurationId { get; set; }

        public CheckDigitConfiguration CheckDigitConfiguration { get; set; }

        public virtual ICollection<Fund> Funds { get; set; }
    }
}
