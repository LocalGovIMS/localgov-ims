using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class MethodOfPaymentMetadataService : BaseService, IMethodOfPaymentMetadataService
    {
        public MethodOfPaymentMetadataService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<MopMetadata> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.MopMetadatas.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<MopMetadata>()
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

        public MopMetadata Get(int id)
        {
            try
            {
                return UnitOfWork.MopMetadatas.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(MopMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MopMetadatas.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the MOP Metadata record");
            }
        }

        public IResult Update(MopMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MopMetadatas.Update(item);
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
                var item = UnitOfWork.MopMetadatas.Get(id);

                UnitOfWork.MopMetadatas.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this MOP Metadata record");
            }
        }
    }
}