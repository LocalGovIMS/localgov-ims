using Admin.Models.EReturnTemplate;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplate
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IVatService _vatService;
        private readonly IEReturnTemplateService _eReturnTemplateService;

        public EditViewModelBuilder(ILog log
            , IVatService vatService
            , IEReturnTemplateService eReturnTemplateService)
            : base(log)
        {
            _vatService = vatService;
            _eReturnTemplateService = eReturnTemplateService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _eReturnTemplateService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.AllowCash = data.Cash;
            model.AllowCheque = data.Cheque;
            model.AllowPdq = data.Pdq;

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