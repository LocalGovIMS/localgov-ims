using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using BusinessLogic.Models.FundMessageMetadata;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundMessageMetadataService : BaseService, IFundMessageMetadataService
    {
        public FundMessageMetadataService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<FundMessageMetadata> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.FundMessageMetadata.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<FundMessageMetadata>()
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

        public FundMessageMetadata Get(int id)
        {
            try
            {
                return UnitOfWork.FundMessageMetadata.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(FundMessageMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMessageMetadata.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the VAT Metadata record");
            }
        }

        public IResult Update(FundMessageMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMessageMetadata.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this VAT Metadata record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.FundMessageMetadata.Get(id);

                UnitOfWork.FundMessageMetadata.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Fund Message Metadata record");
            }
        }

        public List<Metadata> GetMetadata()
        {
            return new List<Metadata>()
            {
                new Metadata() { Key = "IsOnStopForPaymentPortal", Description = "Payments via the Payment Portal are stopped" },
                new Metadata() { Key = "IsOnStopForAdmin", Description = "Payments via Admin are stopped" },
                new Metadata() { Key = "ExternalCode", Description = "External code" }
            };
        }
    }
}