using Admin.Models.Payment;
using BusinessLogic.Models.Payments;
using log4net;

namespace Admin.Classes.Commands.Payment
{
    public class EmptyBasketCommand : BaseCommand<string>
    {
        public EmptyBasketCommand(ILog log) : base(log)
        {
        }

        protected override CommandResult OnExecute(string accountReference)
        {
            var model = new IndexViewModel
            {
                Basket = new Basket(),
                Address = null
            };

            return new CommandResult(true) { Data = model };
        }
    }
}