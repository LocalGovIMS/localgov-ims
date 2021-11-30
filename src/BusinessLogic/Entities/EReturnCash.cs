namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EReturnCash")]
    public partial class EReturnCash
    {
        public int Id { get; set; }

        public int EReturnId { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(50)]
        public string BagNumber { get; set; }

        public decimal? Pounds50 { get; set; }

        public decimal? Pounds20 { get; set; }

        public decimal? Pounds10 { get; set; }

        public decimal? Pounds5 { get; set; }

        public decimal? Pounds2 { get; set; }

        public decimal? Pounds1 { get; set; }

        public decimal? Pence50 { get; set; }

        public decimal? Pence20 { get; set; }

        public decimal? Pence10 { get; set; }

        public decimal? Pence5 { get; set; }

        public decimal? Pence2 { get; set; }

        public decimal? Pence1 { get; set; }

        public decimal? Total { get; set; }

        public virtual EReturn EReturn { get; set; }
    }
}
