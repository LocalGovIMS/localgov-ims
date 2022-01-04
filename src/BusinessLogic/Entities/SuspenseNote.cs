using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public partial class SuspenseNote
    {
        public int Id { get; set; }

        public int SuspenseId { get; set; }
        public virtual Suspense Suspense { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

    }
}
