using System.ComponentModel;

namespace Admin.Models.EReturnTemplate
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Allow cheque payments")]
        public bool AllowCheque { get; set; }

        [DisplayName("Allow cash payments")]
        public bool AllowCash { get; set; }

        [DisplayName("Allow PDQ payments")]
        public bool AllowPdq { get; set; }
    }
}