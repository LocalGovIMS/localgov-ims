using BusinessLogic.Entities;

namespace BusinessLogic.ImportProcessing
{
    public class ImportProcessingStrategyArgs
    {
        public Import Import { get; set; }
        public ImportRow Row { get; set; }
    }
}
