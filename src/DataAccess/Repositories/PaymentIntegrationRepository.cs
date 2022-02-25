using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PaymentIntegrationRepository : Repository<PaymentIntegration>, IPaymentIntegrationRepository
    {
        public PaymentIntegrationRepository(IncomeDbContext context) : base(context)
        {
        }

        public void Update(PaymentIntegration entity)
        {
            var item = IncomeDbContext.PaymentIntegrations
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.BaseUri = entity.BaseUri;
        }
    }
}