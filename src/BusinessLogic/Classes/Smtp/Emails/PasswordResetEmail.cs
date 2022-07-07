using BusinessLogic.Enums;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class PasswordResetEmail : BaseEmail<IdentityMessage>
    {
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.PasswordReset; } }

        public PasswordResetEmail(IdentityMessage message) : base(message)
        {
           
            Email.To.Add(new MailAddress(message.Destination));

            Email.Subject = message.Subject;
            Email.IsBodyHtml = true;
            Email.Body = message.Body;
        }
    }
}
