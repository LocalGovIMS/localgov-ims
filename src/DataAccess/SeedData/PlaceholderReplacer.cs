using System.Configuration;

namespace DataAccess.SeedData
{
    public static class PlaceholderReplacer
    {
        public static string Process(string script)
        {
            return script
                .Replace("[[SeedData.DemoData.PaymentIntegration.Name]]", ConfigurationManager.AppSettings["SeedData.DemoData.PaymentIntegration.Name"])
                .Replace("[[SeedData.DemoData.PaymentIntegration.BaseUri]]", ConfigurationManager.AppSettings["SeedData.DemoData.PaymentIntegration.BaseUri"])
                .Replace("[[SeedData.DemoData.User1.Username]]", ConfigurationManager.AppSettings["SeedData.DemoData.User1.Username"])
                .Replace("[[SeedData.DemoData.User1.Name]]", ConfigurationManager.AppSettings["SeedData.DemoData.User1.Name"])
                .Replace("[[SeedData.DemoData.User1.PasswordHash]]", ConfigurationManager.AppSettings["SeedData.DemoData.User1.PasswordHash"])
                .Replace("[[SeedData.DemoData.User2.Username]]", ConfigurationManager.AppSettings["SeedData.DemoData.User2.Username"])
                .Replace("[[SeedData.DemoData.User2.Name]]", ConfigurationManager.AppSettings["SeedData.DemoData.User2.Name"])
                .Replace("[[SeedData.DemoData.User2.PasswordHash]]", ConfigurationManager.AppSettings["SeedData.DemoData.User2.PasswordHash"])
                .Replace("[[SeedData.DemoData.FundMetadata.Key1]]", ConfigurationManager.AppSettings["SeedData.DemoData.FundMetadata.Key1"])
                .Replace("[[SeedData.DemoData.FundMetadata.Value1]]", ConfigurationManager.AppSettings["SeedData.DemoData.FundMetadata.Value1"])
                .Replace("[[SeedData.DemoData.FundMetadata.FundCode1]]", ConfigurationManager.AppSettings["SeedData.DemoData.FundMetadata.FundCode1"])

                .Replace("[[SeedData.UITestData.PaymentIntegration.Name]]", ConfigurationManager.AppSettings["SeedData.UITestData.PaymentIntegration.Name"])
                .Replace("[[SeedData.UITestData.PaymentIntegration.BaseUri]]", ConfigurationManager.AppSettings["SeedData.UITestData.PaymentIntegration.BaseUri"])
                ;
        }
    }
}
