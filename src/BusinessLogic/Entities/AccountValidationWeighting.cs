namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class AccountValidationWeighting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string ValidationReference { get; set; }

        [StringLength(2)]
        public string Digit1Weighting { get; set; }

        [StringLength(2)]
        public string Digit2Weighting { get; set; }

        [StringLength(2)]
        public string Digit3Weighting { get; set; }

        [StringLength(2)]
        public string Digit4Weighting { get; set; }

        [StringLength(2)]
        public string Digit5Weighting { get; set; }

        [StringLength(2)]
        public string Digit6Weighting { get; set; }

        [StringLength(2)]
        public string Digit7Weighting { get; set; }

        [StringLength(2)]
        public string Digit8Weighting { get; set; }

        [StringLength(2)]
        public string Digit9Weighting { get; set; }

        [StringLength(2)]
        public string Digit10Weighting { get; set; }

        [StringLength(2)]
        public string Digit11Weighting { get; set; }

        [StringLength(2)]
        public string Digit12Weighting { get; set; }

        [StringLength(2)]
        public string Digit13Weighting { get; set; }

        [StringLength(2)]
        public string Digit14Weighting { get; set; }

        [StringLength(2)]
        public string Digit15Weighting { get; set; }

        [StringLength(2)]
        public string Digit16Weighting { get; set; }

        [StringLength(2)]
        public string Digit17Weighting { get; set; }

        [StringLength(2)]
        public string Digit18Weighting { get; set; }

        [StringLength(2)]
        public string Digit19Weighting { get; set; }

        [StringLength(2)]
        public string Digit20Weighting { get; set; }

        [StringLength(2)]
        public string Digit21Weighting { get; set; }

        [StringLength(2)]
        public string Digit22Weighting { get; set; }

        [StringLength(2)]
        public string Digit23Weighting { get; set; }

        [StringLength(2)]
        public string Digit24Weighting { get; set; }

        [StringLength(2)]
        public string Digit25Weighting { get; set; }

        [StringLength(2)]
        public string Digit26Weighting { get; set; }

        [StringLength(2)]
        public string Digit27Weighting { get; set; }

        [StringLength(2)]
        public string Digit28Weighting { get; set; }

        [StringLength(2)]
        public string Digit29Weighting { get; set; }

        [StringLength(2)]
        public string Digit30Weighting { get; set; }

        public virtual AccountValidation AccountValidation { get; set; }
    }
}
