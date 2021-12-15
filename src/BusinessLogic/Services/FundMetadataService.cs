using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using BusinessLogic.Models.FundMetadata;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundMetadataService : BaseService, IFundMetadataService
    {
        public FundMetadataService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<FundMetaData> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.FundMetadatas.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<FundMetaData>()
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

        public FundMetaData Get(int id)
        {
            try
            {
                return UnitOfWork.FundMetadatas.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(FundMetaData item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMetadatas.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the MOP Metadata record");
            }
        }

        public IResult Update(FundMetaData item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundMetadatas.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this MOP Metadata record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.FundMetadatas.Get(id);

                UnitOfWork.FundMetadatas.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this MOP Metadata record");
            }
        }

        public List<Metadata> GetMetadata()
        {
            return new List<Metadata>()
            {
                new Metadata() { Key = "IsACreditNoteEnabledFund", Description = "Is A Credit Note Enabled Fund" },
                new Metadata() { Key = "IsAnEReturnDefaultFund", Description = "Is An EReturn Default Fund" },
                new Metadata() { Key = "IsASuspenseJournalFund", Description = "Is A Suspense Journal Fund" },
                new Metadata() { Key = "IsABasketFund", Description = "Is A Basket Fund" },
                new Metadata() { Key = "Basket.ReferenceFieldLabel", Description = "Basket Reference Field Label" },
                new Metadata() { Key = "Basket.ReferenceFieldMessage", Description = "Basket Reference Field Message" }
            };
        }
    }
}