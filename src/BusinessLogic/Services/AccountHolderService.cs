using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models.AccountHolder;
using BusinessLogic.Models.Shared;
using log4net;
using System;

namespace BusinessLogic.Services
{
    public class AccountHolderService : BaseService, IAccountHolderService
    {
        private readonly IAccountHolderFundMessageValidator _accountHolderFundMessageValidator;

        public AccountHolderService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IAccountHolderFundMessageValidator accountHolderFundMessageValidator)
            : base(logger, unitOfWork, securityContext)
        {
            _accountHolderFundMessageValidator = accountHolderFundMessageValidator;
        }

        public IResult Create(AccountHolder accountHolder)
        {
            try
            {
                var validateAccountHolderFundMessageResult = _accountHolderFundMessageValidator.Validate(accountHolder);
                if (!validateAccountHolderFundMessageResult.Success) 
                    return validateAccountHolderFundMessageResult;

                UnitOfWork.AccountHolders.Add(accountHolder);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result() { Data = accountHolder };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to save Account Holder");
            }
        }

        public IResult Create(CreateAccountHolderArgs args)
        {
            try
            {
                var validateAccountHolderFundMessageResult = _accountHolderFundMessageValidator.Validate(args.AccountHolder);
                if (!validateAccountHolderFundMessageResult.Success)
                    return validateAccountHolderFundMessageResult;

                UnitOfWork.AccountHolders.Add(args.AccountHolder);

                if (args.SaveChanges)
                {
                    UnitOfWork.Complete(SecurityContext.UserId);
                }

                return new Result() { Data = args.AccountHolder };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to save Account Holder");
            }
        }

        public IResult Update(AccountHolder accountHolder)
        {
            try
            {
                var validateAccountHolderFundMessageResult = _accountHolderFundMessageValidator.Validate(accountHolder);
                if (!validateAccountHolderFundMessageResult.Success) 
                    return validateAccountHolderFundMessageResult;

                var existingRecord = UnitOfWork.AccountHolders.GetByAccountReference(accountHolder.AccountReference);

                if (existingRecord == null)
                    throw new NullReferenceException("Unable to find the Account Holder record to update");

                existingRecord.Update(accountHolder);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result() { Data = existingRecord };
            }
            catch (NullReferenceException ex)
            {
                Logger.Error(null, ex);
                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(null, ex);
                return new Result("Unable to update Account Holder");
            }
        }

        public IResult Update(UpdateAccountHolderArgs args)
        {
            try
            {
                var validateAccountHolderFundMessageResult = _accountHolderFundMessageValidator.Validate(args.AccountHolder);
                if (!validateAccountHolderFundMessageResult.Success)
                    return validateAccountHolderFundMessageResult;

                var existingRecord = UnitOfWork.AccountHolders.GetByAccountReference(args.AccountHolder.AccountReference);

                if (existingRecord == null)
                    throw new NullReferenceException("Unable to find the Account Holder record to update");

                existingRecord.Update(args.AccountHolder);

                if (args.SaveChanges)
                {
                    UnitOfWork.Complete(SecurityContext.UserId);
                }

                return new Result() { Data = existingRecord };
            }
            catch (NullReferenceException ex)
            {
                Logger.Error(null, ex);
                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(null, ex);
                return new Result("Unable to update Account Holder");
            }
        }

        public AccountHolder GetByAccountReference(string accountReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.Payments)
                && !SecurityContext.IsInRole(Security.Role.Finance)
                && !SecurityContext.IsInRole(Security.Role.PostPayment)
                && !SecurityContext.IsInRole(Security.Role.ChequeProcess)) return null;

            try
            {
                return UnitOfWork.AccountHolders.GetByAccountReference(accountReference);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public AccountHolder GetByAccountReference(string accountReference, string fundCode)
        {
            if (!SecurityContext.IsInRole(Security.Role.Payments)
                && !SecurityContext.IsInRole(Security.Role.Finance)
                && !SecurityContext.IsInRole(Security.Role.PostPayment)
                && !SecurityContext.IsInRole(Security.Role.ChequeProcess)) return null;

            try
            {
                return UnitOfWork.AccountHolders.GetByAccountReference(accountReference, fundCode);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public SearchResult<AccountHolder> Search(SearchCriteria searchCriteria)
        {
            try
            {
                int resultCount;
                var result = UnitOfWork.AccountHolders.Search(searchCriteria, out resultCount);

                return new SearchResult<AccountHolder>()
                {
                    Count = resultCount,
                    Items = result,
                    Page = searchCriteria.Page,
                    PageSize = searchCriteria.PageSize
                };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }
    }

    public class CreateAccountHolderArgs
    {
        public AccountHolder AccountHolder { get; set; }
        public bool SaveChanges { get; set; } = true;
    }

    public class UpdateAccountHolderArgs
    {
        public AccountHolder AccountHolder { get; set; }
        public bool SaveChanges { get; set; } = true;
    }
}