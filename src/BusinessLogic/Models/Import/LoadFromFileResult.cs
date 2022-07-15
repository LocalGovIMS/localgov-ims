using BusinessLogic.Entities;

namespace BusinessLogic.Models.Import
{
    public class LoadFromFileResult
    {
        public FileImport FileImport { get; set; }

        public int RowCount { get; set; }
    }
}