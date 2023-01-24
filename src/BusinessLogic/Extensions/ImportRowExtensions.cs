using BusinessLogic.Entities;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class ImportRowExtensions
    {
        public static ProcessedTransaction ToProcessedTransaction(this ImportRow item)
        {
            return MessagePackSerializer.Deserialize<ProcessedTransaction>(Convert.FromBase64String(item.Data));
        }

        public static AccountHolder ToAccountHolder(this ImportRow item)
        {
            return MessagePackSerializer.Deserialize<AccountHolder>(Convert.FromBase64String(item.Data));
        }

        public static void UpdateImportId(this IEnumerable<ImportRow> rows, int importId)
        {
            rows.ToList().ForEach(x => x.ImportId = importId);
        }
    }
}
