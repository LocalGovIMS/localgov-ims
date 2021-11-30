using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.MethodOfPayment;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class MethodOfPaymentService : BaseService, IMethodOfPaymentService
    {
        public MethodOfPaymentService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<Mop> GetAllMops()
        {
            try
            {
                return GetAllMops(false);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading MOP codes", e);
                return new List<Mop>();
            }
        }

        public List<Mop> GetAllMops(bool includeDisabled)
        {
            try
            {
                return UnitOfWork.Mops.GetAll(includeDisabled).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading MOP codes", e);
                return new List<Mop>();
            }
        }

        public Mop GetMop(string id)
        {
            try
            {
                var result = UnitOfWork.Mops.GetMop(id);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public SearchResult<Mop> Search(SearchCriteria criteria)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionList)) return null;

            try
            {
                int resultCount;
                var result = UnitOfWork.Mops.Search(criteria.TrimStringProperties(), out resultCount);

                return new SearchResult<Mop>()
                {
                    Count = resultCount,
                    Items = result,
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

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Create(Mop item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Mops.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a Mop");
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Update(Mop item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Mops.Update(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update a Mop");
            }
        }
    }
}