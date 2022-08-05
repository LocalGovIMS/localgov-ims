using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Operations
{
    public class BulkInsert
    {
        private readonly IncomeDbContext _incomeDbContext;

        public BulkInsert(IncomeDbContext incomeDbContext)
        {
            _incomeDbContext = incomeDbContext;
        }

        public void Execute<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            data = data.ToArray();

            using (var connection = new SqlConnection(_incomeDbContext.Database.Connection.ConnectionString))
            {
                Type t = typeof(TEntity);

                var tableMetaData = GetTableMetaData(_incomeDbContext, t);

                var bulkCopy = new SqlBulkCopy(connection)
                {
                    DestinationTableName = tableMetaData.SchemaAndName,
                    BatchSize = BatchSize()
                };

                var properties = t.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), true).Length == 0)
                    .Where(p => tableMetaData.NonKeyFields.Contains(p.Name));

                var dataTable = CreateDataTable(t, data, properties);

                SetupBulkCopyColumnMappings(bulkCopy, properties);

                connection.Open();

                bulkCopy.WriteToServer(dataTable);

                connection.Close();
            }
        }

        private DataTable CreateDataTable<TEntity>(Type t, IEnumerable<TEntity> entities, IEnumerable<System.Reflection.PropertyInfo> properties)
        {
            var table = new DataTable();

            foreach (var property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                table.Columns.Add(new DataColumn(property.Name, propertyType));
            }

            foreach (var entity in entities)
            {
                table.Rows.Add(properties.Select(property => GetPropertyValue(property.GetValue(entity, null))).ToArray());
            }

            return table;
        }

        private object GetPropertyValue(object o)
        {
            return o ?? DBNull.Value;
        }

        public void SetupBulkCopyColumnMappings(SqlBulkCopy sqlBulkCopy, IEnumerable<System.Reflection.PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                sqlBulkCopy.ColumnMappings.Add(property.Name, property.Name);
            }
        }

        public class DatabaseTableMeta
        {
            public string Schema { get; set; }

            public string Name { get; set; }

            public string SchemaAndName => $"[{Schema}].[{Name}]";

            public IEnumerable<string> Keys { get; set; }

            public IEnumerable<string> NonKeyFields { get; set; }
        }

        public static DatabaseTableMeta GetTableMetaData(IncomeDbContext context, Type type)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var items = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                .GetItems<EntityType>(DataSpace.OSpace)
                .Single(p => items.GetClrType(p) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(p => p.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                .Single()
                .EntitySetMappings
                .Single(p => p.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // TODO: Somehow remove fields that are decorated with a DatabaseGenerated attribute, or setup via FluentAPI as database generated
            return new DatabaseTableMeta
            {
                Schema = (string)table.MetadataProperties["Schema"].Value ?? table.Schema,
                Name = (string)table.MetadataProperties["Table"].Value ?? table.Name,
                Keys = entityType.KeyProperties.Select(p => p.Name),
                NonKeyFields = entityType.DeclaredProperties.Select(p => p.Name).Except(entityType.KeyProperties.Select(p => p.Name))
            };
        }

        private static int BatchSize()
        {
            return int.TryParse(ConfigurationManager.AppSettings["BulkInsert.BatchSize"], out int defaultBatchSize)
                ? defaultBatchSize
                : 5000;
        }
    }
}
