using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using BusinessLogic.Models.VatMetadata;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class VatMetadataService : BaseService, IVatMetadataService
    {
        public VatMetadataService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<VatMetadata> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.VatMetadatas.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<VatMetadata>()
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

        public VatMetadata Get(int id)
        {
            try
            {
                return UnitOfWork.VatMetadatas.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(VatMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.VatMetadatas.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the VAT Metadata record");
            }
        }

        public IResult Update(VatMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.VatMetadatas.Update(item);
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
                var item = UnitOfWork.VatMetadatas.Get(id);

                UnitOfWork.VatMetadatas.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this VAT Metadata record");
            }
        }

        public List<Metadata> GetMetadata()
        {
            return new List<Metadata>()
            {
                new Metadata() { Key = "IsASuspenseJournalVatCode", Description = "Is A Suspense Journal Vat Code" }
            };
        }
    }
}