using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Configuration;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class NotificationEmail : BaseEmail<string>
    {
        private const string EmailRecipientSettingKey = "Notification.Email.Recipient";
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.NotificationEmail; } }

        public NotificationEmail(TransactionNotification notification) : base(notification.Id.ToString())
        {
            var recipient = ConfigurationManager.AppSettings[EmailRecipientSettingKey];

            if (string.IsNullOrEmpty(recipient)) throw new NullReferenceException(string.Format("{0} is missing from the web.config", EmailRecipientSettingKey));

            Email.To.Add(new MailAddress(recipient));

            Email.Subject = "Income Management - Important Transaction Event";
            Email.IsBodyHtml = true;
            Email.Body = BuildBody(notification);
        }

        private string BuildBody(TransactionNotification notification)
        {
            return string.Format(@"
                <html>
                <body style=""font-family: sans-serif"">
                  <h1>IMS Important Transaction Event</h1>
                  <table cellpadding=""5"" cellspacing=""0"" border=""1"" style=""font-family: sans-serif"">
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Event Code:</td>
                  <td>{0}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Date:</td>
                  <td>{1}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">PSP Reference:</td>
                  <td>{2}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Merchant (Internal) Reference:</td>
                  <td>{3}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Original Reference:</td>
                  <td>{4}</td>
</tr><tr>
                  <td style=""text-align: right;font-weight: bold"">Reason:</td>
                  <td>{5}</td>
                  </tr> 
                  </table>
                </body>
                </html>",
                notification.EventCode,
                notification.EventDate,
                notification.PspReference,
                notification.MerchantReference,
                notification.OriginalReference,
                notification.Reason);

        }
    }
}
