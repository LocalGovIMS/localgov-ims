using BusinessLogic.Entities;

namespace BusinessLogic.Models
{
    public class LoadFromFileResult
    {
        public FileImport FileImport { get; set; }

        public int RowCount { get; set; }
    }
}