using System;
using System.Collections.Generic;

namespace BusinessLogic.ImportProcessing
{
    public interface IOperations
    {
        IDictionary<OperationType, Func<OperationArgs, bool>> TextOperators { get; }
        IDictionary<OperationType, Func<OperationArgs, bool>> DecimalOperators { get; }
    }
}
