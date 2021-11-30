using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundGroupService
    {
        List<FundGroup> GetAllFundGroups();
        FundGroup GetFundGroup(int id);
        IResult Create(FundGroup item);
        IResult Update(FundGroup item);
        IResult Remove(int id);
    }
}