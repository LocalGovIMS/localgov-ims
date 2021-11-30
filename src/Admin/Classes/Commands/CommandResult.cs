using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace Admin.Classes.Commands
{
    public class CommandResult
    {
        public bool Success { get; private set; }

        // HIGH: We should rename this Errors....
        public IList<string> Messages { get; private set; }
        public object Data { get; set; }

        public CommandResult(bool success)
        {
            Messages = new List<string>();
            Success = success;
        }

        public CommandResult(bool success, string message)
            : this(success)
        {
            Messages.Add(message);
        }

        public CommandResult(bool success, IList<string> messages)
            : this(success)
        {
            Messages = messages;
        }

        public CommandResult(IResult result)
        {
            Success = result.Success;
            Messages = result.Errors;
            Data = result.Data;
        }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}