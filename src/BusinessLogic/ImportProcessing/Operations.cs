using System;
using System.Collections.Generic;

namespace BusinessLogic.ImportProcessing
{
    public class Operations : IOperations
    {
        public IDictionary<OperationType, Func<OperationArgs, bool>> TextOperators => new Dictionary<OperationType, Func<OperationArgs, bool>>()
        {
            { OperationType.Contain, (args) => { return ((string)args.FieldValue ?? string.Empty).Contains((string)args.Value ?? string.Empty); } },
            { OperationType.NotContain, (args) => { return !((string)args.FieldValue ?? string.Empty).Contains((string)args.Value ?? string.Empty); } },
            { OperationType.StartWith, (args) => { return ((string)args.FieldValue ?? string.Empty).StartsWith((string)args.Value ?? string.Empty); } },
            { OperationType.EndWith, (args) => { return ((string)args.FieldValue ?? string.Empty).EndsWith((string)args.Value ?? string.Empty); } },
            { OperationType.Equal, (args) => { return ((string)args.FieldValue ?? string.Empty).Equals((string)args.Value ?? string.Empty); } }
        };

        public IDictionary<OperationType, Func<OperationArgs, bool>> DecimalOperators => new Dictionary<OperationType, Func<OperationArgs, bool>>()
        {
            { OperationType.BeGreaterThan, (args) => { return (decimal)args.FieldValue > (decimal)args.Value; } },
            { OperationType.BeLessThan, (args) => { return (decimal)args.FieldValue < (decimal)args.Value; } },
            { OperationType.Equal, (args) => { return (decimal)args.FieldValue == (decimal)args.Value; } }
        };
    }
}
