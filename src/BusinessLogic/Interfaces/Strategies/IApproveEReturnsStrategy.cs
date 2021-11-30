using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Strategies
{
    public interface IApproveEReturnsStrategy
    {
        Result.IResult Execute(List<Tuple<int, string>> eReturns);
    }
}
