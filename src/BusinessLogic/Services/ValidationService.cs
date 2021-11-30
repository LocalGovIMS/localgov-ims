using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;

namespace BusinessLogic.Services
{
    [Obsolete("Do not use this class. Reference validators in your code instead.")]
    public class ValidationService : BaseService, IValidationService
    {
        private readonly IAccountReferenceValidator _accountReferenceValidator;

        public ValidationService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IAccountReferenceValidator accountReferenceValidator)
            : base(logger, unitOfWork, securityContext)
        {
            _accountReferenceValidator = accountReferenceValidator;
        }

        public IResult ValidateReference(string reference, string fundCode, decimal amount, AccountReferenceValidationSource source)
        {
            return _accountReferenceValidator.ValidateReference(reference, fundCode, amount, source);
        }
    }
}
