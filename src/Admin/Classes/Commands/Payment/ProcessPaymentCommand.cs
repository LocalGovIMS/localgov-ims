using Admin.Classes.Models;
using Admin.Interfaces.Resolvers;
using Admin.Models.Shared;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;
using System.Collections.Generic;

namespace Admin.Classes.Commands.Payment
{
    public class ProcessPaymentCommand : BaseCommand<ProcessPaymentCommandAgrs>
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketValidator _basketValidator;
        private readonly IUrlResolver _urlResolver;

        public ProcessPaymentCommand(ILog log,
            IPaymentService paymentService,
            IBasketValidator basketValidator,
            IUrlResolver urlResolver) : base(log)
        {
            _paymentService = paymentService;
            _basketValidator = basketValidator;
            _urlResolver = urlResolver;
        }

        protected override CommandResult OnExecute(ProcessPaymentCommandAgrs args)
        {
            if (!IsValid(args)) return new CommandResult(false);

            var paymentDetailsList = new List<PaymentDetails>();
            var currentUrl = _urlResolver.GetCurrentUrl();

            foreach (var item in args.Model.Basket.Items)
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
                    Narrative = item.Narrative,
                    MopCode = item.MopCode,
                    VatCode = item.VatCode,
                    BankAccountNo = args.Model.Cheques == null ? null : args.Model.Cheques.BankAccountNo,
                    ChequeNumber = args.Model.Cheques == null ? null : args.Model.Cheques.ChequeNumber,
                    SortCode = args.Model.Cheques == null ? null : args.Model.Cheques.SortCode,
                    ChequeName = args.Model.Cheques == null ? null : args.Model.Cheques.Name,
                    PayeeName = args.Model.Cheques == null ? null : args.Model.Cheques.Name
                });
            }

            var response = _paymentService.ProcessPayments(paymentDetailsList, args.Type);

            return new CommandResult(response.Success) { Data = response.RedirectUrl };
        }


        private bool IsValid(ProcessPaymentCommandAgrs args)
        {
            var result = _basketValidator.Validate(args.Model.Basket);

            if (!result.Success)
            {
                args.Model.Message = new ErrorMessage(result.Error);
            }

            return result.Success;
        }
    }
}