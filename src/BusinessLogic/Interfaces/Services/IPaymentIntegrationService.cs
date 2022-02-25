using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IPaymentIntegrationService
    {
        List<PaymentIntegration> GetAll();
        PaymentIntegration Get(int id);
        IResult Create(PaymentIntegration item);
        IResult Update(PaymentIntegration item);
    }
}