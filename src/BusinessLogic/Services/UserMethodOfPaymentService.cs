using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
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
    public class UserMethodOfPaymentService : BaseService, IUserMethodOfPaymentService
    {
        public UserMethodOfPaymentService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<UserMethodOfPayment> GetByUserId(int id)
        {
            if ((!SecurityContext.IsInRole(Security.Role.PostPayment))
               && (!SecurityContext.IsInRole(Security.Role.SystemAdmin))) return new List<UserMethodOfPayment>();

            try
            {
                return UnitOfWork.UserMethodOfPayments.GetByUserId(id).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<UserMethodOfPayment>();
            }
        }

        public Mop GetDefaultUserMethodOfPayment()
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

        public IResult Update(List<UserMethodOfPayment> userMopCodes, int userId)
        {
            if ((!SecurityContext.IsInRole(Security.Role.SystemAdmin))
               && (!SecurityContext.IsInRole(Security.Role.ServiceDesk))) return new Result("You do not have permission to perform the requested action");

            // Make sure only System.Admins can edit their own account
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && (userMopCodes.Any(x => x.UserId == SecurityContext.UserId)))
                return new Result("You do not have permission to edit your own account");

            try
            {
                UnitOfWork.UserMethodOfPayments.Update(userMopCodes, userId);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update User Methods of Payment");
            }
        }
    }
}