using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.FundMessage
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Fund is required")]
        [DisplayName("Fund")]
        public string FundCode { get; set; }

        [DisplayName("Fund")]
        public string FundName { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        public SelectList Funds { get; set; }
    }
}