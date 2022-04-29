using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class UserMopCodeService : BaseService, IUserMopCodeService
    {
        public UserMopCodeService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<UserMopCode> GetByUserId(int id)
        {
            if ((!SecurityContext.IsInRole(Security.Role.PostPayment))
               && (!SecurityContext.IsInRole(Security.Role.SystemAdmin))) return new List<UserMopCode>();

            try
            {
                return UnitOfWork.UserMopCodes.GetByUserId(id).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<UserMopCode>();
            }
        }

        public Mop GetDefaultUserMopCode()
        {
            try
            {
                return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsACardViaStaffPayment());
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }
    }
}