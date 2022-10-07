using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models.Payments;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class BasketValidator : IBasketValidator
    {
        private readonly ILog _logger;
        private readonly IPaymentValidationHandler _paymentValidationHandler;
        private readonly ISecurityContext _securityContext;

        private const string DefaultErrorMessage = "There is an error with the basket";

        public BasketValidator(ILog logger
            , IPaymentValidationHandler paymentValidationHandler
            , ISecurityContext securityContext)
        {
            _logger = logger;
            _paymentValidationHandler = paymentValidationHandler;
            _securityContext = securityContext;
        }

        public IResult Validate(Basket basket)
        {
            try
            {
                var validateItemsAreUniqueResult = ValidateItemsAreUnique(basket);
                if (!validateItemsAreUniqueResult.Success) return validateItemsAreUniqueResult;

                var validateItemsResult = ValidateItems(basket);
                if (!validateItemsResult.Success) return validateItemsResult;

                return new Result();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result(DefaultErrorMessage);
            }
        }

        private IResult ValidateItemsAreUnique(Basket basket)
        {
            try
            {
                var repeatingItems = basket.Items.GroupBy(x => x.AccountReference)
                    .Where(grp => grp.Count() > 1)
                    .Select(grp => grp.Key);

                if (repeatingItems != null && repeatingItems.Any() && !_securityContext.IsInRole(BusinessLogic.Security.Role.Finance) && !_securityContext.IsInRole(BusinessLogic.Security.Role.ChequeProcess))
                {
                    return new Result("Duplicate account references detected");
                }

                return new Result();

            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result(DefaultErrorMessage);
            }
        }

        private IResult ValidateItems(Basket basket)
        {
            try
            {
                var validationResult = new Result();

                foreach (var item in basket.Items)
                {
                    var result = _paymentValidationHandler.Validate(new Payment.PaymentValidationArgs()
                    {
                        Reference = item.AccountReference,
                        FundCode = item.FundCode,
                        Amount = item.Amount
                    });

                    if (!result.Success)
                    {
                        validationResult.AddError(result.Error);
                    }

                    if (item.Amount != decimal.Round(item.Amount, 2))
                    {
                        _logger.WarnFormat("An invalid payment amount has been discovered: {0}", item.Amount);
                        validationResult.AddError("A payment amount is invalid");
                    }
                }

                return validationResult;
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result(DefaultErrorMessage);
            }
        }
    }
}