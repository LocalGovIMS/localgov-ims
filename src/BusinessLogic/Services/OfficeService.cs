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
    public class OfficeService : BaseService, IOfficeService
    {
        public OfficeService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<Office> GetAll()
        {
            try
            {
                return UnitOfWork.Offices.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public Office Get(string officeCode)
        {
            try
            {
                return UnitOfWork.Offices.SingleOrDefault(x => x.OfficeCode == officeCode);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading VAT code", e);
                return null;
            }
        }


        public IResult Create(Office item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Offices.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create Office record");
            }
        }

        public IResult Update(Office item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Offices.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Office record");
            }
        }
    }
}
