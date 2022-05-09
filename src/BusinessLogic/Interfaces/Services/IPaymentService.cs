using BusinessLogic.Classes;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IPaymentService
    {
        PaymentResponse CreateHppPayment(PaymentDetails paymentDetails);
        PaymentResponse CreateHppPayments(List<PaymentDetails> paymentDetails);
        ProcessPaymentResponse ProcessPayment(PaymentResult paymentResult);
        ProcessPaymentResponse ProcessPayments(List<PaymentDetails> payments, PaymentTypeEnum type);
        IResult ProcessFee(PaymentResult paymentResult);
    }
}
