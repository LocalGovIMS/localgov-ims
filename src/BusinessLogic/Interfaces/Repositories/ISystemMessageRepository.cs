using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ISystemMessageRepository : IRepository<SystemMessage>
    {
        void Update(SystemMessage entity);
        IEnumerable<SystemMessage> GetActive();
        SystemMessage GetSystemMessage(int id);
    }
}
