using BusinessLogic.Entities;

namespace BusinessLogic.Models
{
    public class ProcessResult
    {
        public FileImport FileImport { get; set; }

        public int NumberOfRowsImported { get; set; }

        public decimal TotalAmountImported { get; set; }
    }
}