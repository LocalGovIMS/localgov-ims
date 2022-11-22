using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplateRow
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IVatService _vatService;

        public CreateViewModelBuilder(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.EReturnTemplateId = args.EReturnTemplateId;

            model.VatCodes = GetVatCodes();

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

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

    public class CreateViewModelBuilderArgs
    {
        public int EReturnTemplateId { get; set; }
    }
}