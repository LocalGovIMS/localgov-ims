using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISystemMessageService
    {
        List<SystemMessage> GetSystemMessages();
        List<SystemMessage> GetActiveSystemMessages();
        SystemMessage GetSystemMessage(int id);
        IResult Create(SystemMessage item);
        IResult Update(SystemMessage item);
        List<KeyValuePair<string, string>> GetSeverities();
    }
}