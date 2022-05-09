using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.CheckDigitConfiguration
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BusinessLogic.Enums.CheckDigitType Type { get; set; }

        public string TypeName { get; set; }

        public short Modulus { get; set; }

        [DisplayName("Source substitutions")]
        [StringLength(20)]
        public string SourceSubstitutions { get; set; }

        [StringLength(30)]
        public string Weightings { get; set; }

        [DisplayName("Result substitutions")]
        [StringLength(20)]
        public string ResultSubstitutions { get; set; }

        [DisplayName("Apply subtraction")]
        public bool ApplySubtraction { get; set; }
    }
}