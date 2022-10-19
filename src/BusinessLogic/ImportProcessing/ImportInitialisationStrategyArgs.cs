using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.ImportProcessing
{
    public class ImportInitialisationStrategyArgs
    {
        public Import Import { get; set; }
        public List<ImportRow> ImportRows { get; set; }
    }
}
