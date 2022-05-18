using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IStopMessageService
    {
        List<StopMessage> GetAll();
        StopMessage GetById(int id);
    }
}