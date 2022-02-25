using Admin.Models.PaymentIntegration;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.PaymentIntegration
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IPaymentIntegrationService _paymentIntegrationService;

        public CreateCommand(ILog log
            , IPaymentIntegrationService paymentIntegrationService)
            : base(log)
        {
            _paymentIntegrationService = paymentIntegrationService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.PaymentIntegration()
            {
                Name = model.Name,
                BaseUri = model.BaseUri
            };

            var result = _paymentIntegrationService.Create(item);

            return new CommandResult(result);
        }
    }
}