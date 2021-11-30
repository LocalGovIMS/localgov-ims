using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Smtp;
using System.Configuration;
using System.Net.Mail;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public abstract class BaseEmail<T> : IEmail
    {
        public abstract EmailTypeEnum EmailType { get; }

        public T Args { get; private set; }
        public MailMessage Email { get; protected set; }

        public BaseEmail(T args)
        {
            Args = args;

            Email = new MailMessage
            {
                From = new MailAddress(ConfigurationManager.AppSettings["EmailMessageFrom"]
                    , ConfigurationManager.AppSettings["EmailMessageFromDisplayName"])
            };
        }

        public void AddAttachment(Attachment attachment)
        {
            Email.Attachments.Add(attachment);
        }
    }
}
