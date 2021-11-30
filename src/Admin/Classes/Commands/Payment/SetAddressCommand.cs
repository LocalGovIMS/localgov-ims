using Admin.Models.Payment;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Payment
{
    public class SetAddressCommand : BaseCommand<IndexViewModel>
    {
        private readonly IFundService _fundService;
        private readonly IAccountHolderService _accountHolderService;

        public SetAddressCommand(ILog log,
            IFundService fundService,
            IAccountHolderService accountHolderService)
            : base(log)
        {
            _fundService = fundService;
            _accountHolderService = accountHolderService;
        }

        protected override CommandResult OnExecute(IndexViewModel model)
        {
            if (model.Address != null) return new CommandResult(true);

            foreach (var item in model.Basket.Items)
            {
                var fund = _fundService.GetByFundCode(item.FundCode);
                if (fund == null) continue;
                if (!fund.AccountExist) continue;

                var accountHolder = _accountHolderService.GetByAccountReference(item.AccountReference);
                if (accountHolder == null) continue;

                // This gets rid of empty fields
                MoveUp(accountHolder.AddressLine1, accountHolder.AddressLine2);
                MoveUp(accountHolder.AddressLine2, accountHolder.AddressLine3);
                MoveUp(accountHolder.AddressLine3, accountHolder.AddressLine4);

                MoveUp(accountHolder.AddressLine1, accountHolder.AddressLine2);
                MoveUp(accountHolder.AddressLine2, accountHolder.AddressLine3);
                MoveUp(accountHolder.AddressLine3, accountHolder.AddressLine4);

                MoveUp(accountHolder.AddressLine1, accountHolder.AddressLine2);
                MoveUp(accountHolder.AddressLine2, accountHolder.AddressLine3);
                MoveUp(accountHolder.AddressLine3, accountHolder.AddressLine4);

                if (!string.IsNullOrEmpty(accountHolder.AddressLine1))
                {
                    model.Address = new Address()
                    {
                        PayeeName = string.Format("{0} {1}", accountHolder.Forename, accountHolder.Surname).Trim(),
                        HouseNumberOrName = string.Empty,
                        Street = accountHolder.AddressLine1,
                        City = string.Format("{0}, {1}, {2}"
                            , accountHolder.AddressLine2
                            , accountHolder.AddressLine3
                            , accountHolder.AddressLine4)
                            .Trim().Replace(", , ,", ",").Replace(", ,", ",").TrimEnd(','),
                        PostCode = accountHolder.Postcode
                    };
                }
            }

            return new CommandResult(true) { Data = model };
        }

        private void MoveUp(string a, string b)
        {
            if (!string.IsNullOrEmpty(a)) return;

            a = b;
            b = string.Empty;
        }
    }
}