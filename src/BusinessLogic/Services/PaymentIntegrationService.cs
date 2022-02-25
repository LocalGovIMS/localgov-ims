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
    public class PaymentIntegrationService : BaseService, IPaymentIntegrationService
    {
        public PaymentIntegrationService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<PaymentIntegration> GetAll()
        {
            try
            {
                return UnitOfWork.PaymentIntegrations.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading MOP codes", e);

                return new List<PaymentIntegration>();
            }
        }

        public PaymentIntegration Get(int id)
        {
            try
            {
                var result = UnitOfWork.PaymentIntegrations.SingleOrDefault(x => x.Id == id);

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return null;
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Create(PaymentIntegration item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.PaymentIntegrations.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return new Result("Unable to create a Payment Integration");
            }
        }

        // NOTE: I've kept Update and Create separate - as we may want to 
        // grant access by permission - and it's might be easier to do this 
        // if the two are in separate methods. We can always merge them later...
        public IResult Update(PaymentIntegration item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.PaymentIntegrations.Update(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return new Result("Unable to update a Payment Integration");
            }
        }
    }
}