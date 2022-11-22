using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.EReturnTemplate
{
    public class EditViewModel
    {
        public int Id { get; set; } 

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Allow cheque payments")]
        public bool AllowCheque { get; set; }

        [DisplayName("Allow cash payments")]
        public bool AllowCash { get; set; }

        [DisplayName("Allow PDQ payments")]
        public bool AllowPdq { get; set; }

        public SelectList VatCodes { get; set; }
    }
}