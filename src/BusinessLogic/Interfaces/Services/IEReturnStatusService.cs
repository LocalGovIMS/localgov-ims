using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnStatusService
    {
        List<EReturnStatus> GetAllEReturnStatuses();
    }
}