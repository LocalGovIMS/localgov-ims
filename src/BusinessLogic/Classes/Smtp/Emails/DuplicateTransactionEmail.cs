using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class DuplicateTransactionEmail : BaseEmail<string>
    {
        private const string EmailRecipientSettingKey = "DuplicateTransaction.Email.Recipient";
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.DuplicateTransaction; } }

        public DuplicateTransactionEmail(List<ProcessedTransaction> processedTransactions) : base(processedTransactions.First().InternalReference)
        {
            var recipient = ConfigurationManager.AppSettings[EmailRecipientSettingKey];

            if (string.IsNullOrEmpty(recipient)) throw new NullReferenceException(string.Format("{0} is missing from the web.config", EmailRecipientSettingKey));

            Email.To.Add(new MailAddress(recipient));

            Email.Subject = "Income Management - Duplicate Transaction Warning";
            Email.IsBodyHtml = true;
            Email.Body = BuildBody(processedTransactions);
        }

        private string BuildBody(List<ProcessedTransaction> transactions)
        {
            return string.Format(@"
                <html>
                <body style=""font-family: sans-serif"">
                  <h1>IMS Duplicate Transaction Warning</h1>
                  <table cellpadding=""5"" cellspacing=""0"" border=""1"" style=""font-family: sans-serif"">
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Internal Reference:</td>
                  <td>{0}</td>
                  </tr>
                  <tr>
                  <td style=""text-align: right;font-weight: bold"">Amount:</td>
                  <td>{1}</td>
                  </tr> 
                  </table>
                </body>
                </html>",
                transactions.First().InternalReference,
                transactions.Sum(x => x.Amount));

        }
    }
}
