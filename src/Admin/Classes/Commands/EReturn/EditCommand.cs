using Admin.Models.EReturn;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.Commands.EReturn
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IEReturnService _eReturnService;

        public EditCommand(ILog log
            , IEReturnService eReturnService)
            : base(log)
        {
            _eReturnService = eReturnService ?? throw new ArgumentNullException("eReturnService");
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var transactions = new List<BusinessLogic.Entities.PendingTransaction>();

            var item = new BusinessLogic.Entities.EReturn()
            {
                Id = model.EReturn.EReturn.Id,
                PendingTransactions = model.Transactions.Select(x => new BusinessLogic.Entities.PendingTransaction()
                {
                    Amount = x.Amount,
                    Narrative = x.Description,
                    VatCode = x.VatCode,
                    AccountReference = x.Reference,
                    Id = x.Id
                }).ToList(),
            };

            if (model.Cash != null)
            {
                item.EReturnCashes = new List<BusinessLogic.Entities.EReturnCash>()
                {
                    new BusinessLogic.Entities.EReturnCash() {
                        Id = model.Cash.Id,
                        EReturnId = model.EReturn.EReturn.Id,
                        BagNumber = model.Cash.BagNumber,
                        Pounds50 = model.Cash.Pounds50,
                        Pounds20 = model.Cash.Pounds20,
                        Pounds10 = model.Cash.Pounds10,
                        Pounds5 = model.Cash.Pounds5,
                        Pounds2 = model.Cash.Pounds2,
                        Pounds1 = model.Cash.Pounds1,
                        Pence50 = model.Cash.Pence50,
                        Pence20 = model.Cash.Pence20,
                        Pence10 = model.Cash.Pence10,
                        Pence5 = model.Cash.Pence5,
                        Pence2 = model.Cash.Pence2,
                        Pence1 = model.Cash.Pence1,
                        Total = model.Cash.Total
                    }
                };
            }

            if (model.Cheques != null && model.Cheques.Any())
            {
                item.EReturnCheques = model.Cheques.Select(x => new BusinessLogic.Entities.EReturnCheque()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Name = x.Name,
                    ItemNo = x.ItemNo
                }).ToList();
            }

            var result = _eReturnService.Update(item);

            return new CommandResult(result);
        }
    }
}