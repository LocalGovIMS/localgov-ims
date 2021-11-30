using System.Configuration;

namespace DataAccess.SeedData
{
    public static class PlaceholderReplacer
    {
        public static string Process(string script)
        {
            return script
                .Replace("[[DBInitialiser.PaymentIntegration.Name]]", ConfigurationManager.AppSettings["DatabaseInitialisation.PaymentIntegration.Name"])
                .Replace("[[DBInitialiser.PaymentIntegration.BaseUri]]", ConfigurationManager.AppSettings["DatabaseInitialisation.PaymentIntegration.BaseUri"])
                .Replace("[[DBInitialiser.User1.Username]]", ConfigurationManager.AppSettings["DatabaseInitialisation.User1.Username"])
                .Replace("[[DBInitialiser.User1.Name]]", ConfigurationManager.AppSettings["DatabaseInitialisation.User1.Name"])
                .Replace("[[DBInitialiser.User2.Username]]", ConfigurationManager.AppSettings["DatabaseInitialisation.User2.Username"])
                .Replace("[[DBInitialiser.User2.Name]]", ConfigurationManager.AppSettings["DatabaseInitialisation.User2.Name"])

                .Replace("[[UITestInitialisation.PaymentIntegration.Name]]", ConfigurationManager.AppSettings["UITestInitialisation.PaymentIntegration.Name"])
                .Replace("[[UITestInitialisation.PaymentIntegration.BaseUri]]", ConfigurationManager.AppSettings["UITestInitialisation.PaymentIntegration.BaseUri"])
                .Replace("[[UITestInitialisation.User1.EmailAddress]]", ConfigurationManager.AppSettings["UITestInitialisation.User1.EmailAddress"])
                .Replace("[[UITestInitialisation.User1.PasswordHash]]", ConfigurationManager.AppSettings["UITestInitialisation.User1.PasswordHash"])
                .Replace("[[UITestInitialisation.User2.EmailAddress]]", ConfigurationManager.AppSettings["UITestInitialisation.User2.EmailAddress"])
                .Replace("[[UITestInitialisation.User2.PasswordHash]]", ConfigurationManager.AppSettings["UITestInitialisation.User2.PasswordHash"])
                ;
        }
    }
}
