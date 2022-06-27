using BusinessLogic.Entities;

namespace BusinessLogic.Models.Import.TransactionImport
{
    public class ProcessResult
    {
        public int NumberOfRowsImported { get; set; }

        public decimal TotalAmountImported { get; set; }
    }
}