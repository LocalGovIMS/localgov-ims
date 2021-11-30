using Admin.Models.Fund;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Fund
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IFundService _fundService;
        private readonly IVatService _vatService;

        public EditViewModelBuilder(ILog log
            , IFundService fundService
            , IVatService vatService)
            : base(log)
        {
            _fundService = fundService;
            _vatService = vatService;
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
            model.ValidationReference = data.ValidationReference;
            model.FundCode = data.FundCode;
            model.VatCode = data.VatCode;
            model.VatOverride = data.VatOverride;
            model.IsDisabled = data.Disabled;

            model.VatCodes = GetVatCodes();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.VatCodes = GetVatCodes();

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
    }
}