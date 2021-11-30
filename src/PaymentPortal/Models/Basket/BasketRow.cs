using System;

namespace PaymentPortal.Models
{
    [Serializable]
    public class BasketRow
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public bool Deleted { get; set; }

        public BasketRow()
        {
            Deleted = false;
        }
    }
}