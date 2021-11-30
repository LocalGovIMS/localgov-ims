using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.User;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<User> GetAllUsers()
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)
                && !SecurityContext.IsInRole(Security.Role.Finance)) return new List<User>();

            try
            {
                return UnitOfWork.Users.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<User>();
            }
        }

        public SearchResult<User> Search(SearchCriteria criteria)
        {
            //if (!SecurityContext.IsInRole(Security.Role.TransactionList)) return null; // TODO - Work out what this should be

            try
            {
                int itemsCount;
                var items = UnitOfWork.Users.Search(criteria.TrimStringProperties(), out itemsCount).ToList();

                return new SearchResult<User>()
                {
                    Count = itemsCount,
                    Items = items,
                    Page = criteria.Page,
                    PageSize = criteria.PageSize
                };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                var result = UnitOfWork.Users.GetUser(id);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public User GetUser(string userName)
        {
            try
            {
                var result = UnitOfWork.Users.GetUser(userName);
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
        public IResult Create(User item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                item.CreatedAt = DateTime.Now;
                UnitOfWork.Users.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a User");
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Update(User user)
        {
            if ((!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                 && (!SecurityContext.IsInRole(Security.Role.ServiceDesk)))
                return new Result("You do not have permission to perform the requested action");

            // Make sure only System.Admins can edit their own account
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && (user.UserId == SecurityContext.UserId))
                return new Result("You do not have permission to edit your own account");

            try
            {
                UnitOfWork.Users.Update(user);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update a User");
            }
        }

        // This is called when a user logs in (begins a session), so this is publically accessible.
        // That's why we have it really - it means we're very specific about what we're updating,
        // in this case just a time stamp, rather than reusing the Update method - which gives us
        // access to update all fields.
        public IResult RecordLogin(string userName)
        {
            try
            {
                var user = UnitOfWork.Users.GetUser(userName);
                if (user == null)
                {
                    return new Result("Unable to record Login");
                }
                else
                {
                    UnitOfWork.Users.RecordLogin(userName);
                    UnitOfWork.Complete(user.UserId);

                    return new Result();
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Unable to record login for user: {0}", userName), e);
                return new Result("Unable to record Login");
            }
        }

        public bool DoesUserAccountNeedDisabling(User user)
        {
            try
            {
                if (user.LastLogin.HasValue)
                    return (DateTime.Now - user.LastLogin.Value) > new TimeSpan(user.ExpiryDays, 0, 0, 0);

                if (user.LastEnabledAt.HasValue)
                    return (DateTime.Now - user.LastEnabledAt.Value) > new TimeSpan(user.ExpiryDays, 0, 0, 0);

                if (user.CreatedAt.HasValue)
                    return (DateTime.Now - user.CreatedAt.Value) > new TimeSpan(user.ExpiryDays, 0, 0, 0);

                return false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Unable to check whether a user account needs disabling: {0}", user.UserName), e);
                return false;
            }
        }

        public IResult DisableUser(string userName)
        {
            try
            {
                if (UnitOfWork.Users.GetUser(userName) == null)
                {
                    return new Result("Unable to disabled user");
                }
                else
                {
                    UnitOfWork.Users.DisableUser(userName);
                    UnitOfWork.Complete(SecurityContext.UserId);

                    return new Result();
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Unable to disable user: {0}", userName), e);
                return new Result("Unable to disabled user");
            }
        }

        public IResult IsUserDisabled(string userName)
        {
            try
            {
                if (UnitOfWork.Users.GetUser(userName) == null)
                {
                    Logger.InfoFormat("User not found in the system: {0}", userName);

                    return new Result("Error getting disabled status") { Data = false };
                }
                else
                {
                    Logger.DebugFormat("Checking to se eif user is disabled: {0}", userName);

                    var result = UnitOfWork.Users.IsDisabled(userName);

                    Logger.DebugFormat("Is user disabled?: {0}", result);

                    return new Result() { Data = result };
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Unable to get user disable status: {0}", userName), e);
                return new Result("Error getting disabled status") { Data = false };
            }
        }
    }
}