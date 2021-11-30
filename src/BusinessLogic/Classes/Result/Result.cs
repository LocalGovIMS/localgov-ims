using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Classes.Result
{
    public class Result : IResult
    {
        public bool Success { get { return !Errors.Any(); } }
        public IList<string> Errors { get; protected set; }
        public string Error { get { return string.Join(", ", Errors); } }
        public object Data { get; set; }

        public Result()
        {
            Errors = new List<string>();
        }

        public Result(string error)
            : this()
        {
            AddError(error);
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void SetData(object data)
        {
            Data = data;
        }
    }
}
