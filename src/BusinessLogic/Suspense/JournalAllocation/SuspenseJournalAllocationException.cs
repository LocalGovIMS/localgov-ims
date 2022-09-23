using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BusinessLogic.Suspense.JournalAllocation
{
    [ExcludeFromCodeCoverage]
    public class SuspenseJournalAllocationException : Exception
    {
        private const string DefaultMessage = "The suspense journal allocation is not valid";

        public SuspenseJournalAllocationException()
            : base(DefaultMessage)
        {
        }

        public SuspenseJournalAllocationException(string message)
            : base(message)
        {
        }

        public SuspenseJournalAllocationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SuspenseJournalAllocationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
