using BusinessLogic.Entities;
using System.Threading.Tasks;

namespace BusinessLogic.Clients.PaymentIntegrationClient
{
    public interface IClient
    {
        void ConfigureSettings(PaymentIntegration paymentIntegration);
        Task<RefundResponse> RequestRefund(RefundRequest refundRequest);
    }
}