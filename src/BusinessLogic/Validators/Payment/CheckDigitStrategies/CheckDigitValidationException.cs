using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BusinessLogic.Validators.Payment
{
    [ExcludeFromCodeCoverage]
    public class CheckDigitValidationException : Exception
    {
        private const string DefaultMessage = "The check digit is not valid";

        public CheckDigitValidationException()
            : base(DefaultMessage)
        {
        }

        public CheckDigitValidationException(string message)
            : base(message)
        {
        }

        public CheckDigitValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CheckDigitValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
