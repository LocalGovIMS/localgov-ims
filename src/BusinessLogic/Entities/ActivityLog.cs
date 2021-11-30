namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class ActivityLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string ObjectId { get; set; }

        public int? ObjectType { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid GroupId { get; set; }
    }
}
