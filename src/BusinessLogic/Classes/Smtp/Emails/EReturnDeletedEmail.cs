using BusinessLogic.Enums;
using System;
using System.Configuration;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class EReturnDeletedEmail : BaseEmail<string>
    {
        private const string EmailRecipientSettingKey = "EReturnDeletion.Email.Recipient";
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.EReturnDeleted; } }

        public EReturnDeletedEmail(string eReturnNumber, string deletedBy) : base(eReturnNumber)
        {
            var recipient = ConfigurationManager.AppSettings[EmailRecipientSettingKey];

            if (string.IsNullOrEmpty(recipient)) throw new NullReferenceException(string.Format("{0} is missing from the web.config", EmailRecipientSettingKey));

            Email.To.Add(new MailAddress(recipient));

            Email.Subject = "eReturn Deleted";
            Email.IsBodyHtml = true;
            Email.Body = string.Format("An eReturn has been deleted {0} by {1}", eReturnNumber, deletedBy);
        }
    }
}
