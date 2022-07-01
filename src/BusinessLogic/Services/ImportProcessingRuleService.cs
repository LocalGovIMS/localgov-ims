using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.ImportProcessingRule;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportProcessingRuleService : BaseService, IImportProcessingRuleService
    {
        public ImportProcessingRuleService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<ImportProcessingRule> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.ImportProcessingRules.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<ImportProcessingRule>()
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

        public ImportProcessingRule Get(int id)
        {
            try
            {
                return UnitOfWork.ImportProcessingRules.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<ImportProcessingRule> GetAll(bool includeDisabled)
        {
            try
            {
                return UnitOfWork.ImportProcessingRules
                    .GetAll(includeDisabled)
                    .ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<ImportProcessingRule> GetByTransactionImportType(int transactionImportTypeId)
        {
            try
            {
                return UnitOfWork.ImportProcessingRules
                    .GetByTransactionImportType(transactionImportTypeId)
                    .ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(ImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRules.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Import Processing Rule record");
            }
        }

        public IResult Update(ImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRules.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Import Processing Rule record");
            }
        }
    }
}