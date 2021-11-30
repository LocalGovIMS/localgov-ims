using Admin.Models.MethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MethodOfPayment
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public CreateCommand(ILog log
            , IMethodOfPaymentService methodOfPaymentService)
            : base(log)
        {
            _methodOfPaymentService = methodOfPaymentService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Mop()
            {
                MopCode = model.Code,
                MopName = model.Name,
                MaximumAmount = model.MaximumAmount ?? 0,
                MinimumAmount = model.MaximumAmount ?? 0,
                Disabled = model.IsDisabled
            };

            var result = _methodOfPaymentService.Create(item);

            return new CommandResult(result);
        }
    }
}