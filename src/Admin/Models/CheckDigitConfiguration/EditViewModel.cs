using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.CheckDigitConfiguration
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public BusinessLogic.Enums.CheckDigitType Type { get; set; }

        [Required]
        public short Modulus { get; set; }

        [DisplayName("Source substitutions")]
        [StringLength(20, ErrorMessage = "source substitutions cannot be longer than 20 characters")]
        public string SourceSubstitutions { get; set; }

        [Required]
        [StringLength(30)]
        public string Weightings { get; set; }

        [DisplayName("Result substitutions")]
        [StringLength(20, ErrorMessage = "Result substitutions cannot be longer than 20 characters")]
        public string ResultSubstitutions { get; set; }

        [DisplayName("Apply subtraction")]
        public bool ApplySubtraction { get; set; }

        public SelectList Types { get; set; }
    }
}