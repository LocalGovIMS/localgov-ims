using BusinessLogic.Entities;

namespace BusinessLogic.Models
{
    public class LoadFromFileResult
    {
        public Import Import { get; set; }

        public int RowCount { get; set; }
    }
}