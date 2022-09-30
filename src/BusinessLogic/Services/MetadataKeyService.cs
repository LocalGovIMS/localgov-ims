using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.MetadataKey;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class MetadataKeyService : BaseService, IMetadataKeyService
    {
        public MetadataKeyService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<MetadataKey> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.MetadataKeys.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<MetadataKey>()
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

        public MetadataKey Get(int id)
        {
            try
            {
                return UnitOfWork.MetadataKeys.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<MetadataKey> GetAll()
        {
            try
            {
                return UnitOfWork.MetadataKeys.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(MetadataKey item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MetadataKeys.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Metadata Key record");
            }
        }

        public IResult Update(MetadataKey item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MetadataKeys.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Metadata Key record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.MetadataKeys.Get(id);

                if (item.SystemType)
                    return new Result("Cannot delete a system Metadata Key");

                UnitOfWork.MetadataKeys.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Metadata Key record");
            }
        }
    }
}