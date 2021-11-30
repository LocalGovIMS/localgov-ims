using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.ImportProcessingRuleCondition;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportProcessingRuleConditionService : BaseService, IImportProcessingRuleConditionService
    {
        public ImportProcessingRuleConditionService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<ImportProcessingRuleCondition> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.ImportProcessingRuleConditions.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<ImportProcessingRuleCondition>()
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

        public ImportProcessingRuleCondition Get(int id)
        {
            try
            {
                return UnitOfWork.ImportProcessingRuleConditions.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(ImportProcessingRuleCondition item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRuleConditions.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Import Processing Rule Condition record");
            }
        }

        public IResult Update(ImportProcessingRuleCondition item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRuleConditions.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Import Processing Rule Condition record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.ImportProcessingRuleConditions.Get(id);

                UnitOfWork.ImportProcessingRuleConditions.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Import Processing Rule Condition record");
            }
        }
    }
}