using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.EReturnTemplateRow;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class EReturnTemplateRowService : BaseService, IEReturnTemplateRowService
    {
        public EReturnTemplateRowService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<TemplateRow> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.EReturnTemplateRows.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<TemplateRow>()
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

        public TemplateRow Get(int id)
        {
            try
            {
                return UnitOfWork.EReturnTemplateRows.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(TemplateRow item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.EReturnTemplateRows.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the eReturn template record");
            }
        }

        public IResult Update(TemplateRow item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.EReturnTemplateRows.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this eReturn template record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.EReturnTemplateRows.SingleOrDefault(x => x.Id == id);

                UnitOfWork.EReturnTemplateRows.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this eReturn template record");
            }
        }
    }
}