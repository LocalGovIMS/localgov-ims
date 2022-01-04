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
    public class SuspenseNoteService : BaseService, ISuspenseNoteService
    {
        public SuspenseNoteService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<SuspenseNote> GetAll(int suspenseId)
        {
            try
            {
                return UnitOfWork.SuspenseNotes.Find(x => x.SuspenseId == suspenseId).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(SuspenseNote item)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedByUserId = SecurityContext.UserId;

                UnitOfWork.SuspenseNotes.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create Suspense Note record");
            }
        }
    }
}
