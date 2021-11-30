using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class SystemMessageService : BaseService, ISystemMessageService
    {
        public SystemMessageService(ILog logger, IUnitOfWork unitOfWork, ISecurityContext securityContext) : base(logger, unitOfWork, securityContext)
        {
        }

        public List<SystemMessage> GetSystemMessages()
        {
            try
            {
                return UnitOfWork.SystemMessages.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading all system messages", e);
                return new List<SystemMessage>();
            }
        }

        public List<SystemMessage> GetActiveSystemMessages()
        {
            try
            {
                return UnitOfWork.SystemMessages.GetActive().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading active system messages", e);
                return new List<SystemMessage>();
            }
        }

        public SystemMessage GetSystemMessage(int id)
        {
            try
            {
                var result = UnitOfWork.SystemMessages.GetSystemMessage(id);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Create(SystemMessage item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.SystemMessages.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a System Message");
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Update(SystemMessage item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.SystemMessages.Update(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update a System Message");
            }
        }

        public List<KeyValuePair<string, string>> GetSeverities()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("info", "Informational"),
                new KeyValuePair<string, string>("warning", "Warning"),
                new KeyValuePair<string, string>("error", "Error")
            };
        }
    }
}
