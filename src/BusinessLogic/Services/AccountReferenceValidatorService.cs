using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.AccountReferenceValidator;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class AccountReferenceValidatorService : BaseService, IAccountReferenceValidatorService
    {
        public AccountReferenceValidatorService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public IResult Create(AccountReferenceValidator item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.AccountReferenceValidators.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Account Reference Validator record");
            }
        }

        public SearchResult<AccountReferenceValidator> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.AccountReferenceValidators.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<AccountReferenceValidator>()
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

        public List<AccountReferenceValidator> GetAll()
        {
            try
            {
                return UnitOfWork.AccountReferenceValidators.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public AccountReferenceValidator Get(int id)
        {
            try
            {
                return UnitOfWork.AccountReferenceValidators.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public AccountReferenceValidator GetByFundCode(string fundCode)
        {
            try
            {
                return UnitOfWork.AccountReferenceValidators.GetByFundCode(fundCode);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return null;
            }
        }

        public IResult Update(AccountReferenceValidator item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.AccountReferenceValidators.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Account Reference Validator record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = Get(id);

                UnitOfWork.AccountReferenceValidators.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Account Reference Validator record");
            }
        }
    }
}