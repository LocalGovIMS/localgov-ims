namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ImportEventLog
    {
        public ImportEventLog()
        {
            
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        [Required]
        public int ImportId { get; set; }
        public virtual Import Import { get; set; }
        
        public byte Type { get; set; }

        public string Message { get; set; }
    }
}
