using BusinessLogic.Enums;
using System.Net.Mail;

namespace BusinessLogic.Interfaces.Smtp
{
    public interface IEmail
    {
        EmailTypeEnum EmailType { get; }
        MailMessage Email { get; }
        void AddAttachment(Attachment attachment);
    }
}
