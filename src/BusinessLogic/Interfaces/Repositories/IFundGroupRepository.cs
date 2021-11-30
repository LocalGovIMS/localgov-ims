using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundGroupRepository : IRepository<FundGroup>
    {
        FundGroup GetFundGroup(int id);
        void Update(FundGroup entity);
    }
}