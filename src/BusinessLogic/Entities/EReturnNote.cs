using System;

namespace BusinessLogic.Entities
{
    public partial class EReturnNote
    {
        public int Id { get; set; }

        public int EReturnId { get; set; }
        public virtual EReturn EReturn { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
    }
}
