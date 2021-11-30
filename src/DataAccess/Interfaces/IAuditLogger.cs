using DataAccess.Persistence;

namespace DataAccess.Interfaces
{
    public interface IAuditLogger
    {
        void CreateAudit(IncomeDbContext context, int userId);
    }
}
