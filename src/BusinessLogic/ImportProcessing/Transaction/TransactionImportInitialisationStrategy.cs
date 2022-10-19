using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportInitialisationStrategy : IImportInitialisationStrategy
    {
        private readonly IMetadataKeyService _metadataKeyService;

        public TransactionImportInitialisationStrategy(IMetadataKeyService metadataKeyService)
        {
            _metadataKeyService = metadataKeyService;
        }

        public void Initialise(ImportInitialisationStrategyArgs args)
        {
            var metadataKeyId = _metadataKeyService.Search(new Models.MetadataKey.SearchCriteria() 
            { 
                EntityType = MetadataKeyEntityType.Import, 
                Name = ImportMetadataKeys.TotalImportValue 
            }).Items.FirstOrDefault().Id;

            var totalAmountToImport = args.ImportRows.Sum(x => x.ToProcessedTransaction().Amount);

            args.Import.Metadata.Add(new Entities.ImportMetadata()
            {
                MetadataKeyId = metadataKeyId,
                Value = string.Format("{0:C}", totalAmountToImport)
            });
        }
    }
}
