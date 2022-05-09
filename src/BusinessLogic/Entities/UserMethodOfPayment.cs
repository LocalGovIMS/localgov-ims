namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class UserMethodOfPayment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(5)]
        public string MopCode { get; set; }

        public virtual Mop Mop { get; set; }

        public virtual User User { get; set; }
    }
}
