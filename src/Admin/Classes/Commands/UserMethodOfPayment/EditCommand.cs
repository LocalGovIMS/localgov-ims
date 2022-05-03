using Admin.Models.UserMethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Linq;

namespace Admin.Classes.Commands.UserMethodOfPayment
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserMethodOfPaymentService _userMethodOfPaymentService;

        public EditCommand(ILog log
            , IUserMethodOfPaymentService userMethodOfPaymentService)
            : base(log)
        {
            _userMethodOfPaymentService = userMethodOfPaymentService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var items = model.MopCodes
                .Where(x => x.IsChecked == true)
                .Select(x => new BusinessLogic.Entities.UserMethodOfPayment
                {
                    UserId = model.UserId,
                    MopCode = x.Id
                }).ToList();

            var result = _userMethodOfPaymentService.Update(items, model.UserId);

            return new CommandResult(result);
        }
    }
}