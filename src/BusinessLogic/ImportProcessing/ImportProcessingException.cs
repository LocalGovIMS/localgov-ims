using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BusinessLogic.ImportProcessing
{
    [ExcludeFromCodeCoverage]
    public class ImportProcessingException : Exception
    {
        private const string DefaultMessage = "The import is not valid";

        public ImportProcessingException()
            : base(DefaultMessage)
        {
        }

        public ImportProcessingException(string message)
            : base(message)
        {
        }

        public ImportProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ImportProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
