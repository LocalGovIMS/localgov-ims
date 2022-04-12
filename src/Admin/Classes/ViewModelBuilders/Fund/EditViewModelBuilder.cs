using Admin.Models.Fund;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Fund
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IFundService _fundService;
        private readonly IVatService _vatService;
        private readonly IAccountReferenceValidatorService _accountReferenceValidatorService;

        public EditViewModelBuilder(ILog log
            , IFundService fundService
            , IVatService vatService
            , IAccountReferenceValidatorService accountReferenceValidatorService)
            : base(log)
        {
            _fundService = fundService;
            _vatService = vatService;
            _accountReferenceValidatorService = accountReferenceValidatorService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(string id)
        {
            var data = _fundService.GetByFundCode(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.AccessLevel = data.AccessLevel;
            model.AccountExist = data.AccountExist;
            model.AquireAddress = data.AquireAddress;
            model.ExportToFund = data.ExportToFund;
            model.ExportToLedger = data.ExportToLedger;
            model.DisplayName = data.DisplayName;
            model.FundExportFormat = data.FundExportFormat;
            model.FundName = data.FundName;
            model.GLCode = data.GeneralLedgerCode;
            model.MaximumAmount = data.MaximumAmount;
            model.Narrative = data.NarrativeFlag;
            model.OverPayAccount = data.OverPayAccount;
            model.UseGLCode = data.UseGeneralLedgerCode;
            model.AccountReferenceValidatorId = data.AccountReferenceValidatorId;
            model.FundCode = data.FundCode;
            model.VatCode = data.VatCode;
            model.VatOverride = data.VatOverride;
            model.IsDisabled = data.Disabled;

            model.VatCodes = GetVatCodes();
            model.AccountReferenceValidators = GetAccountReferenceValidators();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.VatCodes = GetVatCodes();
            model.AccountReferenceValidators = GetAccountReferenceValidators();

            return model;
        }

        private SelectList GetVatCodes()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _vatService.GetAllCodes();

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.VatCode,
                    Text = item.VatCode,
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetAccountReferenceValidators()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _accountReferenceValidatorService.GetAll()
                .OrderBy(x => x.Name);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

            return new SelectList(selectListItems, false);
        }
    }
}