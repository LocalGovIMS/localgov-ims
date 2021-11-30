using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Fund;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundService : BaseService, IFundService
    {
        public FundService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<Fund> GetAllFunds()
        {
            try
            {
                return GetAllFunds(false);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<Fund> GetAllFunds(bool includeDisabled)
        {
            try
            {
                return UnitOfWork.Funds.GetAll(includeDisabled).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public Fund GetByFundCode(string fundCode)
        {
            if ((!SecurityContext.IsInRole(Security.Role.Payments))
                && (!SecurityContext.IsInRole(Security.Role.ChequeProcess))
                && (!SecurityContext.IsInRole(Security.Role.PostPayment))) return null;

            try
            {
                return UnitOfWork.Funds.GetByFundCode(fundCode);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<Fund> GetCreditNoteFunds()
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance)) return null;

            try
            {
                return UnitOfWork.Funds.GetAll(true)
                    .Where(x => x.IsACreditNoteEnabledFund())
                    .ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<Fund> GetBasketFunds()
        {
            try
            {
                return UnitOfWork.Funds.GetAll(true)
                    .Where(x => x.IsABasketFund())
                    .ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public SearchResult<Fund> Search(SearchCriteria criteria)
        {
            try
            {
                int itemsCount;
                var items = UnitOfWork.Funds.Search(criteria, out itemsCount).ToList();

                return new SearchResult<Fund>()
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

        public IResult Create(Fund item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Funds.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create Fund record");
            }
        }

        public IResult Update(Fund item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Funds.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Fund record");
            }
        }
    }
}