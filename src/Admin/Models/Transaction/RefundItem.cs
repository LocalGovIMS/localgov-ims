using BusinessLogic.Entities;

namespace Admin.Models.Transaction
{
    public class RefundItem
    {
        public ProcessedTransaction Transaction { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}