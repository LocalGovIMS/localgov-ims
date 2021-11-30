using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Smtp;

namespace BusinessLogic.Interfaces.Dependencies
{
    public interface IEmailServiceDependencies
    {
        IEmailFactory EmailFactory { get; }
        IRepository<EmailLog> EmailLogRepository { get; }
    }
}
