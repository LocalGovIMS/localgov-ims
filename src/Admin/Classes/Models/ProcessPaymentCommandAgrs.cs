using Admin.Models.Payment;
using BusinessLogic.Enums;

namespace Admin.Classes.Models
{
    public class ProcessPaymentCommandAgrs
    {
        public IndexViewModel Model { get; private set; }
        public PaymentTypeEnum Type { get; private set; }

        public ProcessPaymentCommandAgrs(IndexViewModel model, PaymentTypeEnum type)
        {
            Model = model;
            Type = type;
        }
    }
}