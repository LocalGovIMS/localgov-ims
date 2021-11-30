namespace BusinessLogic.Entities
{
    public partial class UserTemplate
    {
        public int UserTemplateId { get; set; }

        public int UserId { get; set; }

        public int TemplateId { get; set; }

        public virtual Template Template { get; set; }

        public virtual User User { get; set; }
    }
}
