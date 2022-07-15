using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.ImportType;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportTypeService : BaseService, IImportTypeService
    {
        public ImportTypeService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<ImportType> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.ImportTypes.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<ImportType>()
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

        public ImportType Get(int id)
        {
            try
            {
                return UnitOfWork.ImportTypes.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading Import Type", e);
                return null;
            }
        }

        public List<ImportType> GetAll()
        {
            try
            {
                return UnitOfWork.ImportTypes.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading Import Type", e);

                return new List<ImportType>();
            }
        }

        public IResult Create(ImportType item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportTypes.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create Import Type record");
            }
        }

        public IResult Update(ImportType item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.ImportTypes.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Import Type record");
            }
        }
    }
}