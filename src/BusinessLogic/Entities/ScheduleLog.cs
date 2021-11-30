namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class ScheduleLog
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string JobName { get; set; }

        public DateTime JobRunTime { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }
    }
}
