using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class CheckDigitConfiguration
    {
        public CheckDigitConfiguration()
        {
            AccountReferenceValidators = new HashSet<AccountReferenceValidator>();
        }

        public int Id { get; set; }

        public int Type { get; set; }

        public short Modulus { get; set; }

        [StringLength(20)]
        public string SourceSubstitutions { get; set; }

        [StringLength(30)]
        public string Weightings { get; set; }

        [StringLength(20)]
        public string ResultSubstitutions { get; set; }

        public bool ApplySubtraction { get; set; }

        public virtual ICollection<AccountReferenceValidator> AccountReferenceValidators { get; set; }
    }
}
