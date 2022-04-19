using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.CheckDigitConfiguration;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class CheckDigitConfigurationService : BaseService, ICheckDigitConfigurationService
    {
        public CheckDigitConfigurationService(
            ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public IResult Create(CheckDigitConfiguration item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.CheckDigitConfigurations.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the Check Digit Configuration record");
            }
        }

        public SearchResult<CheckDigitConfiguration> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.CheckDigitConfigurations.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<CheckDigitConfiguration>()
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

        public List<CheckDigitConfiguration> GetAll()
        {
            try
            {
                return UnitOfWork.CheckDigitConfigurations.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading Check Digit Configurations", e);
                return new List<CheckDigitConfiguration>();
            }
        }

        public CheckDigitConfiguration Get(int id)
        {
            try
            {
                return UnitOfWork.CheckDigitConfigurations.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }        

        public IResult Update(CheckDigitConfiguration item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) 
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.CheckDigitConfigurations.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this Check Digit Configuration record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = Get(id);

                UnitOfWork.CheckDigitConfigurations.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this Check Digit Configuration record");
            }
        }
    }
}