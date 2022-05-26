using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.FundMessage;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundMessageService : BaseService, IFundMessageService
    {
        public FundMessageService(ILog logger, IUnitOfWork unitOfWork, ISecurityContext securityContext) : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<FundMessage> Search(SearchCriteria criteria)
        {
            try
            {
                int itemsCount;
                var items = UnitOfWork.FundMessages.Search(criteria, out itemsCount).ToList();

                return new SearchResult<FundMessage>()
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

        public List<FundMessage> GetAll()
        {
            try
            {
                return UnitOfWork.FundMessages.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading all messages", e);
                return new List<FundMessage>();
            }
        }

        public FundMessage GetById(int id)
        {
            try
            {
                return UnitOfWork.FundMessages.GetById(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading message", e);
                return null;
            }
        }

        public IResult Create(FundMessage item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMessages.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create Fund Message record");
            }
        }

        public IResult Update(FundMessage item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMessages.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Fund Message record");
            }
        }
    }
}
