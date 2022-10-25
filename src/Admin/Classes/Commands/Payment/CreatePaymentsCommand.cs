using Admin.Interfaces.Resolvers;
using Admin.Models.Payment;
using Admin.Models.Shared;
using BusinessLogic.Classes;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.Commands.Payment
{
    public class CreatePaymentsCommand : BaseCommand<IndexViewModel>
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketValidator _basketValidator;
        private readonly IUrlResolver _urlResolver;
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        private readonly string _cardViaStaffMopCode;

        public CreatePaymentsCommand(
            ILog log,
            IPaymentService paymentService,
            IBasketValidator basketValidator,
            IUrlResolver urlResolver,
            IMethodOfPaymentService methodOfPaymentService) : base(log)
        {
            _paymentService = paymentService;
            _basketValidator = basketValidator;
            _urlResolver = urlResolver;
            _methodOfPaymentService = methodOfPaymentService;

            _cardViaStaffMopCode = GetCardPaymentViaStaffMopCode();
        }

        private string GetCardPaymentViaStaffMopCode()
        {
            return _methodOfPaymentService.GetAllMops(true).FirstOrDefault(x => x.IsACardViaStaffPayment()).MopCode;
        }

        protected override CommandResult OnExecute(IndexViewModel model)
        {
            var paymentDetailsList = new List<PaymentDetails>();
            var currentUrl = _urlResolver.GetCurrentUrl();

            // HIGH: I don't like this, it should in the CreteHppPayemnts below - i.e. we should always validate regardless of where the create it called from
            if (!IsValid(model)) return new CommandResult(false);

            foreach (var item in model.Basket.Items)
            {
                paymentDetailsList.Add(new PaymentDetails()
                {
                    AccountReference = item.AccountReference,
                    Fund = item.FundCode,
                    Amount = item.Amount,
                    CancelUrl = currentUrl + "/payment/cancelpayment",
                    CreatedAt = DateTime.Now,
                    FailUrl = currentUrl + "/payment/fail",
                    SuccessUrl = currentUrl + "/payment/success",
                    Source = "basket",
                    PayeeName = model.Address == null ? string.Empty : model.Address.PayeeName,
                    AddressLine1 = model.Address == null ? string.Empty : model.Address.AddressLine1,
                    AddressLine2 = model.Address == null ? string.Empty : model.Address.AddressLine2,
                    AddressLine3 = model.Address == null ? string.Empty : model.Address.AddressLine3,
                    AddressLine4 = model.Address == null ? string.Empty : model.Address.AddressLine4,
                    Postcode = model.Address == null ? string.Empty : model.Address.PostCode,
                    Narrative = item.Narrative,
                    MopCode = _cardViaStaffMopCode,
                    VatCode = item.VatCode
                });

            }

            var response = _paymentService.CreateHppPayments(paymentDetailsList);

            return new CommandResult(true) { Data = response.ResponseUrl };
        }

        private bool IsValid(IndexViewModel model)
        {
            var result = _basketValidator.Validate(model.Basket);

            if (!result.Success)
            {
                model.Message = new ErrorMessage(result.Error);
            }

            return result.Success;
        }
    }
}