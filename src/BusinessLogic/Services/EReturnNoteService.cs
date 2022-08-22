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
    public class EReturnNoteService : BaseService, IEReturnNoteService
    {
        public EReturnNoteService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<EReturnNote> GetAll(int eReturnId)
        {
            try
            {
                return UnitOfWork.EReturnNotes.Find(x => x.EReturnId == eReturnId).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(EReturnNote item)
        {
            if (!(SecurityContext.IsInRole(Security.Role.EReturns)
                    || SecurityContext.IsInRole(Security.Role.EReturnAuthoriser)
                    || SecurityContext.IsInRole(Security.Role.EReturnDelete)))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedByUserId = SecurityContext.UserId;

                UnitOfWork.EReturnNotes.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create EReturn Note record");
            }
        }
    }
}
