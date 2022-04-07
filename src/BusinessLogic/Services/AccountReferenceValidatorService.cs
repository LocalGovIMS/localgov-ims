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
    public class AccountReferenceValidatorService : BaseService, IAccountReferenceValidatorService
    {
        public AccountReferenceValidatorService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public AccountReferenceValidator GetByFundCode(string fundCode)
        {
            try
            {
                return UnitOfWork.AccountReferenceValidators.GetByFundCode(fundCode);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return null;
            }
        }

        public List<AccountReferenceValidator> GetAll()
        {
            try
            {
                return UnitOfWork.AccountReferenceValidators.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }
    }
}