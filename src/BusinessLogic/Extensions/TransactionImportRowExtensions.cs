using BusinessLogic.Entities;

namespace BusinessLogic.Extensions
{
    public static class TransactionImportRowExtensions
    {
        public static ProcessedTransaction ToProcessedTransaction(this TransactionImportRow item)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ProcessedTransaction>(item.Data);
        }
    }
}
