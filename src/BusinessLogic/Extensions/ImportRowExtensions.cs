using BusinessLogic.Entities;

namespace BusinessLogic.Extensions
{
    public static class ImportRowExtensions
    {
        public static ProcessedTransaction ToProcessedTransaction(this ImportRow item)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ProcessedTransaction>(item.Data);
        }

        public static AccountHolder ToAccountHolder(this ImportRow item)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AccountHolder>(item.Data);
        }
    }
}
