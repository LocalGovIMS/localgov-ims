using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleActionService _importProcessingRuleActionService;
        private readonly IImportProcessingRuleFieldService _importProcessingRuleFieldService;

        public EditViewModelBuilder(ILog log
            , IImportProcessingRuleActionService importProcessingRuleActionService
            , IImportProcessingRuleFieldService importProcessingRuleFieldService)
            : base(log)
        {
            _importProcessingRuleActionService = importProcessingRuleActionService;
            _importProcessingRuleFieldService = importProcessingRuleFieldService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _importProcessingRuleActionService.Get(id);

            var model = new EditViewModel();

            model.Fields = GetFieldList();

            if (data == null) return model;

            model.Id = data.Id;
            model.ImportProcessingRuleId = data.ImportProcessingRuleId;
            model.ImportProcessingRuleFieldId = data.ImportProcessingRuleFieldId;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetFieldList()
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in _importProcessingRuleFieldService.GetAll().OrderBy(x => x.DisplayOrder))
            {
                var dataAttributes = new List<ValuePair>();

                dataAttributes.Add(new ValuePair() { Key = "type", Value = item.Type });

                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(selectListItems, false);
        }
    }
}