using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class StopMessageService : BaseService, IStopMessageService
    {
        public StopMessageService(ILog logger, IUnitOfWork unitOfWork, ISecurityContext securityContext) : base(logger, unitOfWork, securityContext)
        {
        }

        public List<StopMessage> GetAll()
        {
            try
            {
                return UnitOfWork.StopMessages.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading all stop messages", e);
                return new List<StopMessage>();
            }
        }

        public StopMessage GetById(int id)
        {
            try
            {
                return UnitOfWork.StopMessages.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading stop message", e);
                return null;
            }
        }
    }
}
