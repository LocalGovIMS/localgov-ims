using Admin.Models.MethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MethodOfPayment
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public EditCommand(ILog log
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
                MinimumAmount = model.MinimumAmount ?? 0,
                Disabled = model.IsDisabled
            };

            var result = _methodOfPaymentService.Update(item);

            return new CommandResult(result);
        }
    }
}