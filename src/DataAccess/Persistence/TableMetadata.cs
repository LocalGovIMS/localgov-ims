using System.Collections.Generic;

namespace DataAccess.Persistence
{
    public class TableMetadata
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public string SchemaAndName => $"[{Schema}].[{Name}]";
        public IEnumerable<string> Keys { get; set; }
        public IEnumerable<string> NonKeyFields { get; set; }
    }
}
