using BusinessLogic.Classes;
using BusinessLogic.Enums;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IPaymentService
    {
        PaymentResponse CreateHppPayment(PaymentDetails paymentDetails);
        PaymentResponse CreateHppPayments(List<PaymentDetails> paymentDetails);
        ProcessPaymentResponse ProcessPayment(PaymentResult paymentResult);
        ProcessPaymentResponse ProcessPayments(List<PaymentDetails> payments, PaymentTypeEnum type);
    }
}
