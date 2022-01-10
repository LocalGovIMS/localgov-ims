using BusinessLogic.Entities;

namespace BusinessLogic.Models
{
    public class ProcessResult
    {
        public Import Import { get; set; }

        public int NumberOfRowsImported { get; set; }

        public decimal TotalAmountImported { get; set; }
    }
}