using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.ImportProcessingRuleAction;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportProcessingRuleActionService : BaseService, IImportProcessingRuleActionService
    {
        public ImportProcessingRuleActionService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<ImportProcessingRuleAction> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.ImportProcessingRuleActions.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<ImportProcessingRuleAction>()
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

        public ImportProcessingRuleAction Get(int id)
        {
            try
            {
                return UnitOfWork.ImportProcessingRuleActions.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(ImportProcessingRuleAction item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)
                && !SecurityContext.IsInRole(Security.Role.Finance)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRuleActions.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Import Processing Rule Action record");
            }
        }

        public IResult Update(ImportProcessingRuleAction item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)
                && !SecurityContext.IsInRole(Security.Role.Finance)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportProcessingRuleActions.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Import Processing Rule Action record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)
                && !SecurityContext.IsInRole(Security.Role.Finance)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.ImportProcessingRuleActions.Get(id);

                UnitOfWork.ImportProcessingRuleActions.Remove(item);
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