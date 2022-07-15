namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ImportRow
    {
        public ImportRow()
        {
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ImportId { get; set; }
        public virtual Import Import { get; set; }

        public string Data { get; set; }
    }
}
