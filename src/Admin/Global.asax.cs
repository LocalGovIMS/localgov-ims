using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Admin
{
    [ExcludeFromCodeCoverage]
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog log;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            try
            {
                LogDebug(string.Format("System.Security.Principal.WindowsIdentity.GetCurrent().Name : {0}", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
                LogDebug(string.Format("System.Web.HttpContext.Current.User.Identity.Name : {0}", HttpContext.Current.User.Identity.Name));

                if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

                var userService = DependencyResolver.Current.GetService<IUserService>();

                if (userService == null)
                {
                    LogError(@"userService is null, user service is required to authenticate and begin the session!");
                    return;
                }

                var username = HttpContext.Current.User.Identity.Name;

                LogInfo(string.Format("Starting Session for user: {0}", username ?? "Unknown"));

                var user = userService.GetUser(username);

                LogDebug(string.Format("Found user account?: {0}", user != null));

                if (user == null)
                {
                    LogError(string.Format("Unable to load the user account: {0}", username ?? "Unknown User"));
                    return;
                }

                LogDebug(string.Format("Is user account disabled?: {0}", user.Disabled));

                if (user.Disabled) return;

                LogDebug("Checking to see if user needs disabling");

                if (userService.DoesUserAccountNeedDisabling(user))
                {
                    LogInfo(string.Format("Disabling user: {0}", username));

                    userService.DisableUser(username);
                }
                else
                {
                    LogInfo(string.Format("Recording login for user: {0}", username));

                    userService.RecordLogin(username);
                }
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(ex);

                // TODO: Now send an email with the error
            }
        }

        private void LogDebug(string message)
        {
            if (log == null) return;

            log.Debug(message);
        }

        private void LogInfo(string message)
        {
            if (log == null) return;

            log.Info(message);
        }

        private void LogError(string message)
        {
            if (log == null) return;

            log.Error(message);
        }

        private void LogError(Exception message)
        {
            if (log == null) return;

            log.Error(message);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = HttpContext.Current.Server.GetLastError();
            LogError(lastError);

            SendErrorEmail();
        }


        private void SendErrorEmail()
        {
            Exception lastError = HttpContext.Current.Server.GetLastError();

            string lastErrorTypeName = lastError.GetType().ToString();
            string lastErrorMessage = lastError.Message;
            string lastErrorStackTrace = lastError.StackTrace;

            string toAddress = ConfigurationManager.AppSettings["EmailMessageTo"];
            string fromAddress = ConfigurationManager.AppSettings["EmailMessageFrom"];
            string subject = "IMS PaymentsAdmin Error";

            // Create the MailMessage object
            MailMessage mm = new MailMessage(fromAddress, toAddress);
            mm.Subject = subject;
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.Body = string.Format(@"
                <html>
                <body style=""font-family: sans-serif"">
                  <h1>IMS PaymentsAdmin Error</h1>
                  <table cellpadding=""5"" cellspacing=""0"" border=""1"" style=""font-family: sans-serif"">
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">URL:</td>
                  <td>{0}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">User:</td>
                  <td>{1}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Exception Type:</td>
                  <td>{2}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Message:</td>
                  <td>{3}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Stack Trace:</td>
                  <td>{4}</td>
                  </tr> 
                  </table>
                </body>
                </html>",
                Request.RawUrl,
                User.Identity.Name,
                lastErrorTypeName,
                lastErrorMessage,
                lastErrorStackTrace.Replace(Environment.NewLine, "<br />"));

            // Send the email
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailHost"]);
            smtp.Send(mm);
        }
    }
}
