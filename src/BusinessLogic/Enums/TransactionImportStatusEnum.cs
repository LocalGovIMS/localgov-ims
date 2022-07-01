using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum TransactionImportStatusEnum
    {
        [Display(Name = "Received")]
        Received = 1,

        [Display(Name = "In Progess")]
        InProgress = 2,

        [Display(Name = "Completed")]
        Completed = 3,

        [Display(Name = "Failed")]
        Failed = 4,

        [Display(Name = "Reverted")]
        Reverted = 5
    }
}