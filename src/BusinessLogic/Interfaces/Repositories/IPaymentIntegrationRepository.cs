using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IPaymentIntegrationRepository : IRepository<PaymentIntegration>
    {
        void Update(PaymentIntegration entity);
    }
}