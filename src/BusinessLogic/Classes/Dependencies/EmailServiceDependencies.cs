using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Dependencies;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Smtp;
using System;

namespace BusinessLogic.Classes.Dependencies
{
    public class EmailServiceDependencies : IEmailServiceDependencies
    {
        public IRepository<EmailLog> EmailLogRepository { get; private set; }
        public IEmailFactory EmailFactory { get; private set; }

        public EmailServiceDependencies(IEmailFactory emailFactory
            , IRepository<EmailLog> emailLogRepository)
        {
            EmailFactory = emailFactory ?? throw new ArgumentNullException("emailFactory");
            EmailLogRepository = emailLogRepository ?? throw new ArgumentNullException("emailLogRepository");
        }
    }
}
