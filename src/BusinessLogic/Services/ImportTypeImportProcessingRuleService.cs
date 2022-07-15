using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportTypeImportProcessingRuleService : BaseService, IImportTypeImportProcessingRuleService
    {
        public ImportTypeImportProcessingRuleService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<ImportTypeImportProcessingRule> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.ImportTypeImportProcessingRules.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<ImportTypeImportProcessingRule>()
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

        public ImportTypeImportProcessingRule Get(int id)
        {
            try
            {
                return UnitOfWork.ImportTypeImportProcessingRules.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(ImportTypeImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportTypeImportProcessingRules.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Import Type Import Processing Rule record");
            }
        }

        public IResult Update(ImportTypeImportProcessingRule item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportTypeImportProcessingRules.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Import Type Import Processing Rule record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.ImportTypeImportProcessingRules.Get(id);

                UnitOfWork.ImportTypeImportProcessingRules.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Import Type Import Processing Rule record");
            }
        }
    }
}