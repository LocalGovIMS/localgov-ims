namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class EmailLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(10)]
        public string EmailType { get; set; }

        public int? TransactionProcessedId { get; set; }

        [Required]
        [StringLength(255)]
        public string RecipientEmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Body { get; set; }

        public virtual ProcessedTransaction ProcessedTransaction { get; set; }
    }
}
