using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.EReturnTemplate;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class EReturnTemplateService : BaseService, IEReturnTemplateService
    {
        public EReturnTemplateService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<Template> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.EReturnTemplates.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<Template>()
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

        public Template Get(int id)
        {
            try
            {
                return UnitOfWork.EReturnTemplates.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading eReturn template", e);
                return null;
            }
        }

        public List<Template> GetAll()
        {
            try
            {
                return UnitOfWork.EReturnTemplates.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading eReturn template", e);

                return new List<Template>();
            }
        }

        public IResult Create(Template item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.EReturnTemplates.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create eReturn template record");
            }
        }

        public IResult Update(Template item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.EReturnTemplates.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this eReturn template record");
            }
        }
    }
}