using BusinessLogic.Entities;

namespace BusinessLogic.Models.Import
{
    public class ProcessResult
    {
        public FileImport FileImport { get; set; }

        public int NumberOfRowsImported { get; set; }
    }
}