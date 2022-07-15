namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ImportStatusHistory
    {
        public ImportStatusHistory()
        {
            
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        [Required]
        public int ImportId { get; set; }
        public virtual Import Import { get; set; }
    }
}
