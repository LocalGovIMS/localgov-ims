using BusinessLogic.Enums;
using System;
using System.Configuration;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class NewUserRegistrationEmail : BaseEmail<string>
    {
        private const string EmailRecipientSettingKey = "NewUserRegistration.Email.Recipient";
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.NewUserRegistration; } }

        public NewUserRegistrationEmail(string newUserEmailAddress) : base(newUserEmailAddress)
        {
            var recipient = ConfigurationManager.AppSettings[EmailRecipientSettingKey];

            if (string.IsNullOrEmpty(recipient)) throw new NullReferenceException(string.Format("{0} is missing from the web.config", EmailRecipientSettingKey));

            Email.To.Add(new MailAddress(recipient));

            Email.Subject = "New User Registration";
            Email.IsBodyHtml = true;
            Email.Body = string.Format($"A new user has registered for access to the system: {newUserEmailAddress}");
        }
    }
}
