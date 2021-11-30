using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Result
{
    public interface IResult
    {
        bool Success { get; }
        IList<string> Errors { get; }
        string Error { get; }
        object Data { get; }
    }
}
