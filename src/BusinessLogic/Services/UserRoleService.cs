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
    public class UserRoleService : BaseService, IUserRoleService
    {
        public UserRoleService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<UserRole> GetByUserId(int id)
        {
            if ((!SecurityContext.IsInRole(Security.Role.SystemAdmin))
               && (!SecurityContext.IsInRole(Security.Role.ServiceDesk))) return new List<UserRole>();

            try
            {
                return UnitOfWork.UserRoles.GetByUserId(id).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<UserRole>();
            }
        }

        public List<string> GetByUserRole(string username)
        {
            if ((!SecurityContext.IsInRole(Security.Role.SystemAdmin))
               && (!SecurityContext.IsInRole(Security.Role.ServiceDesk))) return new List<string>();

            try
            {
                return UnitOfWork.UserRoles.GetByUsername(username, true);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<string>();
            }
        }

        public IResult Update(List<UserRole> items, int userId)
        {
            if ((!SecurityContext.IsInRole(Security.Role.SystemAdmin))
               && (!SecurityContext.IsInRole(Security.Role.ServiceDesk))) return new Result("You do not have permission to perform the requested action");

            // Make sure only System.Admins can edit their own account
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && (items.Any(x => x.UserId == SecurityContext.UserId)))
                return new Result("You do not have permission to edit your own account");

            try
            {
                UnitOfWork.UserRoles.Update(items, userId);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update User Roles");
            }
        }
    }
}