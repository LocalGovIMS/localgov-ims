using System.ComponentModel;

namespace Admin.Models.TransactionImportType
{
    public class DetailsViewModel
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("External reference")]
        public string ExternalReference { get; set; }
    }
}