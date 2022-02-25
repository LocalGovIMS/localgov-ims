using Admin.Models.PaymentIntegration;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.PaymentIntegration
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IPaymentIntegrationService _paymentIntegrationService;

        public EditCommand(ILog log
            , IPaymentIntegrationService paymentIntegrationService)
            : base(log)
        {
            _paymentIntegrationService = paymentIntegrationService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.PaymentIntegration()
            {
                Id = model.Id,
                Name = model.Name,
                BaseUri = model.BaseUri
            };

            var result = _paymentIntegrationService.Update(item);

            return new CommandResult(result);
        }
    }
}