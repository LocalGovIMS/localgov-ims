using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransactionImportTypeImportProcessingRuleService : BaseService, ITransactionImportTypeImportProcessingRuleService
    {
        public TransactionImportTypeImportProcessingRuleService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<TransactionImportTypeImportProcessingRule> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.TransactionImportTypeImportProcessingRules.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<TransactionImportTypeImportProcessingRule>()
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

        public TransactionImportTypeImportProcessingRule Get(int id)
        {
            try
            {
                return UnitOfWork.TransactionImportTypeImportProcessingRules.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(TransactionImportTypeImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.TransactionImportTypeImportProcessingRules.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Transaction Import Type Import Processing Rule record");
            }
        }

        public IResult Update(TransactionImportTypeImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.TransactionImportTypeImportProcessingRules.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Transaction Import Type Import Processing Rule record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.TransactionImportTypeImportProcessingRules.Get(id);

                UnitOfWork.TransactionImportTypeImportProcessingRules.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Transaction Import Type Import Processing Rule record");
            }
        }
    }
}