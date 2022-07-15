using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Import;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportService : BaseService, IImportService
    {
        public ImportService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<Import> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.Imports.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<Import>()
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

        public Import Get(int id)
        {
            try
            {
                return UnitOfWork.Imports.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading transaction import type", e);
                return null;
            }
        }
    }
}