namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class EReturnCheque
    {
        public int Id { get; set; }

        public int EReturnId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ItemNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public virtual EReturn EReturn { get; set; }
    }
}
