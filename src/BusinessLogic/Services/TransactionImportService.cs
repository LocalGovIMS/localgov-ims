using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.TransactionImport;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransactionImportService : BaseService, ITransactionImportService
    {
        public TransactionImportService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<TransactionImport> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.TransactionImports.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<TransactionImport>()
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

        public TransactionImport Get(int id)
        {
            try
            {
                return UnitOfWork.TransactionImports.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading transaction import type", e);
                return null;
            }
        }
    }
}