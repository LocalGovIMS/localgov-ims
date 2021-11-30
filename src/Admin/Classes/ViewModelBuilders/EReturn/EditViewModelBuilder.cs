using Admin.Models.EReturn;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.EReturn
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IEReturnService _eReturnService;
        private readonly IVatService _vatService;

        public EditViewModelBuilder(ILog log
            , IEReturnService eReturnService
            , IVatService vatService)
            : base(log)
        {
            _eReturnService = eReturnService;
            _vatService = vatService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var item = _eReturnService.GetEReturn(id);
            var model = new EditViewModel() { Transactions = new List<TransactionViewModel>() };

            if (item == null) return model;

            // NOTE: IMPORTANT - Needs to be in the right state to load...
            // We mustn't be able to edit anything not in these states
            if (item.EReturn.StatusId != (int)EReturnStatus.New
                && item.EReturn.StatusId != (int)EReturnStatus.InProgress
                && item.EReturn.StatusId != (int)EReturnStatus.Submitted) return null;

            // Step 1 - Get the EReturn
            model.EReturn = item;

            // Step 2 - Get Transaction details
            if ((EReturnStatus)item.EReturn.StatusId == EReturnStatus.Authorised)
            {
                if (item.EReturn.ProcessedTransactions.Any())
                {
                    model.Transactions = item.EReturn.ProcessedTransactions.Select(x => new TransactionViewModel()
                    {
                        Id = x.Id,
                        Amount = x.Amount,
                        Description = x.Narrative,
                        Reference = x.AccountReference,
                        VatCode = x.VatCode
                    }).ToList();

                }
            }
            else
            {
                if (item.EReturn.PendingTransactions.Any())
                {
                    model.Transactions = item.EReturn.PendingTransactions.Select(x => new TransactionViewModel()
                    {
                        Id = x.Id,
                        Amount = x.Amount,
                        Description = x.Narrative,
                        Reference = x.AccountReference,
                        VatCode = x.VatCode,
                        TemplateRowId = x.TemplateRowId ?? 0
                    }).ToList();
                }
            }

            // Step 3 - Get Cash or Cheque details
            if ((EReturnType)item.EReturn.TypeId == EReturnType.Cash)
            {
                var cash = item.EReturn.EReturnCashes.FirstOrDefault();
                if (cash == null)
                {
                    model.Cash = new CashBreakdownViewModel();
                }
                else
                {
                    model.Cash = new CashBreakdownViewModel()
                    {
                        Id = cash.Id,
                        BagNumber = cash.BagNumber,
                        Pounds50 = cash.Pounds50,
                        Pounds20 = cash.Pounds20,
                        Pounds10 = cash.Pounds10,
                        Pounds5 = cash.Pounds5,
                        Pounds2 = cash.Pounds2,
                        Pounds1 = cash.Pounds1,
                        Pence50 = cash.Pence50,
                        Pence20 = cash.Pence20,
                        Pence10 = cash.Pence10,
                        Pence5 = cash.Pence5,
                        Pence2 = cash.Pence2,
                        Pence1 = cash.Pence1,
                        Total = cash.Total
                    };
                }
            }

            if ((EReturnType)item.EReturn.TypeId == EReturnType.Cheque)
            {
                if (item.EReturn.EReturnCheques.Any())
                {
                    model.Cheques = item.EReturn.EReturnCheques.Select(x => new ChequeViewModel()
                    {
                        Id = x.Id,
                        Amount = x.Amount,
                        ItemNo = x.ItemNo,
                        Name = x.Name
                    }).ToList();
                }
                else
                {
                    model.Cheques = new List<ChequeViewModel>();
                }
            }

            // Step 4 populate reference data
            model.VatCodes = GetVatList();

            return model;
        }

        private SelectList GetVatList()
        {
            var vatSelectListItems = new List<System.Web.Mvc.SelectListItem>();
            var vats = _vatService.GetAllCodes().OrderBy(x => x.VatCode);

            foreach (var vat in vats)
            {
                vatSelectListItems.Add(new System.Web.Mvc.SelectListItem()
                {
                    Value = vat.VatCode,
                    Text = vat.VatCode + " - " + vat.Percentage.ToTwoDecimalPlaces()
                });
            }

            return new SelectList(vatSelectListItems, true);
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            if (model.Transactions == null) model.Transactions = new List<TransactionViewModel>();
            model.VatCodes = GetVatList();

            var item = _eReturnService.GetEReturn(model.EReturn.EReturn.Id);

            model.EReturn.EReturn.Template = item.EReturn.Template;
            model.EReturn.EReturn.EReturnStatus = item.EReturn.EReturnStatus;
            model.EReturn.EReturn.CreatedByUser = item.EReturn.CreatedByUser;
            model.EReturn.EReturn.SubmittedByUser = item.EReturn.SubmittedByUser;

            return model;
        }
    }
}