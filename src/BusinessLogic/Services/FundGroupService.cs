using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundGroupService : BaseService, IFundGroupService
    {
        public FundGroupService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public FundGroup GetFundGroup(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) return null;

            try
            {
                var result = UnitOfWork.FundGroups.GetFundGroup(id);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<FundGroup> GetAllFundGroups()
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk))
                return new List<FundGroup>();

            try
            {
                return UnitOfWork.FundGroups.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<FundGroup>();
            }
        }

        public IResult Create(FundGroup item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.FundGroups.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a Fund Group");
            }
        }

        public IResult Remove(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var entity = UnitOfWork.FundGroups.SingleOrDefault(x => x.FundGroupId == id);
                UnitOfWork.FundGroups.Remove(entity);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to remove a Fund Group");
            }
        }

        public IResult Update(FundGroup item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var existingEntity = UnitOfWork.FundGroups.SingleOrDefault(x => x.FundGroupId == item.FundGroupId);

                //Now we need to load FundGroupFunds
                var existingFundGroupFunds = UnitOfWork.FundGroupFunds.GetFundGroupFundsByFundGroupId(item.FundGroupId);

                // For those Entity.FundGroupFunds which no longer exist - delete them.
                var fundGroupFunds = from a in existingFundGroupFunds
                                     join b in item.FundGroupFunds
                                     on new { a.FundGroupId, a.FundCode } equals new { b.FundGroupId, b.FundCode }
                                     select a;

                var fundGroupFundsToRemove = existingFundGroupFunds.Except(fundGroupFunds);

                // For those Entity.FundGroupFunds which no longer exist - delete them.
                var itemFundGroupFunds = from a in item.FundGroupFunds
                                         join b in existingFundGroupFunds
                                         on new { a.FundGroupId, a.FundCode } equals new { b.FundGroupId, b.FundCode }
                                         select a;

                var fundGroupFundsToAdd = item.FundGroupFunds.Except(itemFundGroupFunds);

                foreach (var fundGroupFund in fundGroupFundsToRemove)
                {
                    UnitOfWork.FundGroupFunds.Remove(fundGroupFund);
                }

                foreach (var fundGroupFund in fundGroupFundsToAdd)
                {
                    UnitOfWork.FundGroupFunds.Add(fundGroupFund);
                }

                existingEntity.Name = item.Name;

                //UnitOfWork.FundGroups.Update(existingEntity);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result() { Data = existingEntity };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to udpate a Fund Group");
            }
        }
    }
}