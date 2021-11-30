using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IImportProcessingRuleFieldService _importProcessingRuleFieldService;

        public CreateViewModelBuilder(ILog log
            , IImportProcessingRuleFieldService importProcessingRuleFieldService)
            : base(log)
        {
            _importProcessingRuleFieldService = importProcessingRuleFieldService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.ImportProcessingRuleId = args.ImportProcessingRuleId;
            model.Fields = GetFieldList();

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

    public class CreateViewModelBuilderArgs
    {
        public int ImportProcessingRuleId { get; set; }
    }
}