using DataAccess.Persistence;
using DataAccess.SeedData;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Reflection;

namespace DataAccess.DatabaseInitialisers
{
    public class CreateDatabaseIfNotExists : CreateDatabaseIfNotExists<IncomeDbContext>
    {
        protected override void Seed(IncomeDbContext context)
        {
            RunScript("DataAccess.SeedData.SeedData.sql", context);
            RunScript("DataAccess.SeedData.Indexes.sql", context);

            if (ConfigurationManager.AppSettings["Environment"].Equals("Demo"))
            {
                RunScript("DataAccess.SeedData.DemoData.sql", context);
            }

            if (ConfigurationManager.AppSettings["Environment"].Equals("UITest"))
            {
                RunScript("DataAccess.SeedData.DemoData.sql", context);
                RunScript("DataAccess.SeedData.UITestData.sql", context);
            }
        }

        private void RunScript(string filename, IncomeDbContext context)
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
