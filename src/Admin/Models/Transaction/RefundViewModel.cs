using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Transaction
{
    public class RefundViewModel
    {
        public string Reference { get; set; }

        public List<RefundItem> RefundItems { get; set; }

        [Display(Name = "Refund reason")]
        [Required(ErrorMessage = "A reason for the refund is required")]
        [MaxLength(100, ErrorMessage = "The reason for the refund must be less than 100 characters")]
        public string RefundReason { get; set; }

        public RefundViewModel()
        {
            RefundItems = new List<RefundItem>();
        }
    }
}