using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum CharacterType
    {
        [Display(Name = "Alpha")]
        Alpha = 1,

        [Display(Name = "Alpha whitespace")]
        AlphaWhiteSpace,

        [Display(Name = "Numeric")]
        Numeric,

        [Display(Name = "Numeric whitespace")]
        NumericWhiteSpace,

        [Display(Name = "Alphanumeric")]
        AlphaNumeric,

        [Display(Name = "Alphanumeric whitespace")]
        AlphaNumericWhiteSpace
    }
}
