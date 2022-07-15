using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum ImportDataTypeEnum
    {
        [Display(Name = "Transaction")]
        Transaction = 1,

        [Display(Name = "Account Holder")]
        AccountHolder = 2
    }
}