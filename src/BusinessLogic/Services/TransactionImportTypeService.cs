using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.TransactionImportType;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransactionImportTypeService : BaseService, ITransactionImportTypeService
    {
        public TransactionImportTypeService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<TransactionImportType> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.TransactionImportTypes.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<TransactionImportType>()
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

        public TransactionImportType Get(int id)
        {
            try
            {
                return UnitOfWork.TransactionImportTypes.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading transaction import type", e);
                return null;
            }
        }

        public List<TransactionImportType> GetAll()
        {
            try
            {
                return UnitOfWork.TransactionImportTypes.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading transaction import type", e);

                return new List<TransactionImportType>();
            }
        }

        public IResult Create(TransactionImportType item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.TransactionImportTypes.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create transaction import type record");
            }
        }

        public IResult Update(TransactionImportType item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.TransactionImportTypes.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this transaction import type record");
            }
        }
    }
}