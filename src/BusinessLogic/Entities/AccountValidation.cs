namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AccountValidation
    {
        public AccountValidation()
        {
            AccountValidationWeightings = new HashSet<AccountValidationWeighting>();
        }

        [Key]
        [StringLength(3)]
        public string ValidationReference { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(2)]
        public string Modulus { get; set; }

        [StringLength(1)]
        public string TenConversion { get; set; }

        [StringLength(30)]
        public string InputMask { get; set; }

        [StringLength(2)]
        public string MinLength { get; set; }

        [StringLength(2)]
        public string MaxLength { get; set; }

        public bool SubtractFlag { get; set; }

        [StringLength(10)]
        public string CheckDigitCalcAlphaReplace { get; set; }

        // TODO: Abilities such as the one represented here should be positive. This should be 'CanBeNumeric'
        public bool CanNotBeNumeric { get; set; }

        public virtual ICollection<AccountValidationWeighting> AccountValidationWeightings { get; set; }
    }
}
