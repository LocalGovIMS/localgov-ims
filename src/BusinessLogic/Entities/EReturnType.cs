namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class EReturnType
    {
        public EReturnType()
        {
            EReturns = new HashSet<EReturn>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public virtual ICollection<EReturn> EReturns { get; set; }
    }
}
