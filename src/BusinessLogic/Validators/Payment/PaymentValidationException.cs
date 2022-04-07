using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BusinessLogic.Validators.Payment
{
    [ExcludeFromCodeCoverage]
    public class PaymentValidationException : Exception
    {
        private const string DefaultMessage = "The account reference is not valid";

        public PaymentValidationException()
            : base(DefaultMessage)
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
