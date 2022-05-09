using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum CheckDigitType
    {
        [Display(Name = "Weighted sum")]
        WeightedSum = 1,

        [Display(Name = "Dynix library")]
        DynixLibrary = 2
    }
}
