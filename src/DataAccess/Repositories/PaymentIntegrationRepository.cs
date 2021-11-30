using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class PaymentIntegrationRepository : Repository<PaymentIntegration>, IPaymentIntegrationRepository
    {
        public PaymentIntegrationRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}