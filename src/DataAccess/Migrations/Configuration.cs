using DataAccess.SeedData;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;

namespace DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Persistence.IncomeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistence.IncomeDbContext context)
        {
            RunScript("DataAccess.SeedData.SeedData.sql", context);
            RunScript("DataAccess.SeedData.Indexes.sql", context);
            RunScript("DataAccess.SeedData.ComputedColumns.sql", context);
        }

        private void RunScript(string filename, Persistence.IncomeDbContext context)
        {
            string commandText;
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            using (Stream s = thisAssembly.GetManifestResourceStream(filename))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                }
            }

            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, PlaceholderReplacer.Process(commandText));
        }
    }
}
