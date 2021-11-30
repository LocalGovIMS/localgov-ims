using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(IncomeDbContext context) : base(context)
        {
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
        }
    }
}