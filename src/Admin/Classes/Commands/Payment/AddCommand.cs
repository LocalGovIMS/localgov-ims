using Admin.Models.Payment;
using Admin.Models.Shared;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models.Payments;
using log4net;
using System.Linq;

namespace Admin.Classes.Commands.Payment
{
    public class AddCommand : BaseCommand<IndexViewModel>
    {
        private readonly IFundService _fundService;
        private readonly IMethodOfPaymentService _mopService;
        private readonly IPaymentValidationHandler _paymentValidationHandler;
        private readonly ISecurityContext _securityContext;

        public AddCommand(ILog log,
            IFundService fundService,
            IMethodOfPaymentService mopService,
            IPaymentValidationHandler paymentValidationHandler,
            ISecurityContext securityContext)
            : base(log)
        {
            _fundService = fundService;
            _mopService = mopService;
            _paymentValidationHandler = paymentValidationHandler;
            _securityContext = securityContext;
        }

        protected override CommandResult OnExecute(IndexViewModel model)
        {
            model.AccountReference = model.AccountReference.Trim();

            var fund = _fundService.GetByFundCode(model.FundCode);
            var mop = _mopService.GetMop(model.MopCode);

            if (!IsValid(model, fund)) return new CommandResult(false) { Data = model };

            model.Basket.Items.Add(new BasketItem()
            {
                AccountReference = model.AccountReference,
                Amount = model.Amount,
                Narrative = model.Narrative,
                FundCode = model.FundCode,
                FundName = fund.FundName,
                MopCode = model.MopCode,
                MopName = mop.MopName,
                VatCode = model.VatCode
            });

            // Reset form values - so they're ready for the next entry
            model.FundCode = string.Empty;
            model.MopCode = string.Empty;
            model.AccountReference = string.Empty;
            model.Narrative = string.Empty;
            model.Amount = 0;
            model.VatCode = string.Empty;

            return new CommandResult(true) { Data = model };
        }

        private bool IsValid(IndexViewModel model, BusinessLogic.Entities.Fund fund)
        {
            // TODO: This is business logic - it should be moved into BLL.
            // However, BasketValidator - called later on, would check this anyway, so maybe it can be removed? 
            if (model.Basket.Items.Any(x => x.AccountReference == model.AccountReference) && !_securityContext.IsInRole(BusinessLogic.Security.Role.Finance) && !_securityContext.IsInRole(BusinessLogic.Security.Role.ChequeProcess))
            {
                model.Message = new ErrorMessage("The account reference provided has already been used");
                return false;
            }

            if (model.VatCode != fund.VatCode && fund.VatOverride == false)
            {
                model.Message = new ErrorMessage("You cannot override the VAT code for this fund");
                return false;
            }

            if (model.Amount != decimal.Round(model.Amount, 2))
            {
                Log.WarnFormat("An invalid payment amount has been discovered: {0}", model.Amount);
                model.Message = new ErrorMessage("A payment amount is invalid");
                return false;
            }

            var result = _paymentValidationHandler.Validate(new BusinessLogic.Validators.Payment.PaymentValidationArgs()
            {
                Reference = model.AccountReference,
                FundCode = model.FundCode,
                Amount = model.Amount
            });

            if (!result.Success)
            {
                model.Message = new ErrorMessage(result.Error);
                return false;
            }

            return true;
        }
    }
}