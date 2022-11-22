using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplateRow
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IEReturnTemplateRowService _eReturnTemplateRowService;
        private readonly IVatService _vatService;

        public EditViewModelBuilder(ILog log
            , IEReturnTemplateRowService eReturnTemplateRowService
            , IVatService vatService)
            : base(log)
        {
            _eReturnTemplateRowService = eReturnTemplateRowService;
            _vatService = vatService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _eReturnTemplateRowService.Get(id);

            var model = new EditViewModel();

            model.VatCodes = GetVatCodes();

            if (data == null) return model;

            model.Id = data.Id;
            model.EReturnTemplateId = data.TemplateId;
            model.Reference = data.Reference;
            model.ReferenceOverride = data.ReferenceOverride;
            model.VatCode = data.VatCode;
            model.VatOverride = data.VatOverride;
            model.Description = data.Description;
            model.DescriptionOverride = data.DescriptionOverride;

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