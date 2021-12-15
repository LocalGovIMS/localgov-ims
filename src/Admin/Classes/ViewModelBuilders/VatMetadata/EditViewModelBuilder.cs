using Admin.Models.VatMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.VatMetadata
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IVatMetadataService _service;

        public EditViewModelBuilder(ILog log
            , IVatMetadataService fundMetadataService)
            : base(log)
        {
            _service = fundMetadataService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _service.Get(id);

            var model = new EditViewModel();

            model.Keys = GetKeyList();

            if (data == null) return model;

            model.Id = data.Id;
            model.VatCode = data.VatCode;
            model.Key = data.Key;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetKeyList()
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in _service.GetMetadata().OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Key.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }
    }
}