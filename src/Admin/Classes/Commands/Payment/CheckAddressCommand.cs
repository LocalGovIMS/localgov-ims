using Admin.Models.Payment;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Payment
{
    public class CheckAddressCommand : BaseCommand<IndexViewModel>
    {
        private readonly IFundService _fundService;

        public CheckAddressCommand(ILog log,
            IFundService fundService) : base(log)
        {
            _fundService = fundService;
        }

        protected override CommandResult OnExecute(IndexViewModel model)
        {
            if (model.Address == null)
            {
                foreach (var item in model.Basket.Items)
                {
                    if (_fundService.GetByFundCode(item.FundCode).AquireAddress == true)
                    {
                        return new CommandResult(true) { Data = true };
                    }
                }

                return new CommandResult(true) { Data = false };
            }

            return new CommandResult(true) { Data = !model.AddressReviewed };
        }
    }
}