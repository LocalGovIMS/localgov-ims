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
    public class UserPostPaymentMopCodeService : BaseService, IUserPostPaymentMopCodeService
    {
        public UserPostPaymentMopCodeService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<UserPostPaymentMopCode> GetUserPostPaymentMopCodes(int id)
        {
            if ((!SecurityContext.IsInRole(Security.Role.PostPayment))
               && (!SecurityContext.IsInRole(Security.Role.SystemAdmin))) return new List<UserPostPaymentMopCode>();

            try
            {
                return UnitOfWork.UserPostPaymentMopCodes.GetUserPostPaymentMopCodes(id).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<UserPostPaymentMopCode>();
            }
        }

        public Mop GetDefaultUserPostPaymentMopCode()
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