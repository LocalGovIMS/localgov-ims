using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BusinessLogic.ImportProcessing
{
    [ExcludeFromCodeCoverage]
    public class TransactionImportProcessorException : Exception
    {
        private const string DefaultMessage = "The import is not valid";

        public TransactionImportProcessorException()
            : base(DefaultMessage)
        {
        }

        public TransactionImportProcessorException(string message)
            : base(message)
        {
        }

        public TransactionImportProcessorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected TransactionImportProcessorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
