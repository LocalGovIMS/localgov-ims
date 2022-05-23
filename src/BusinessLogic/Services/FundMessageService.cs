using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class FundMessageService : BaseService, IFundMessageService
    {
        public FundMessageService(ILog logger, IUnitOfWork unitOfWork, ISecurityContext securityContext) : base(logger, unitOfWork, securityContext)
        {
        }

        public List<FundMessage> GetAll()
        {
            try
            {
                return UnitOfWork.FundMessages.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading all messages", e);
                return new List<FundMessage>();
            }
        }

        public FundMessage GetById(int id)
        {
            try
            {
                return UnitOfWork.FundMessages.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading message", e);
                return null;
            }
        }
    }
}
