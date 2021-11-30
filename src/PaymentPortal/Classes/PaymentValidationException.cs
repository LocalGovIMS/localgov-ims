using System;
using System.Runtime.Serialization;

namespace PaymentPortal.Classes
{
    [Serializable]
    public class PaymentValidationException : Exception
    {
        public PaymentValidationException()
        {
        }

        public PaymentValidationException(string message)
            : base(message)
        {
        }

        public PaymentValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PaymentValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}