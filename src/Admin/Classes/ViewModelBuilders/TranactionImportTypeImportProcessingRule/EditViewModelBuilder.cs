using Admin.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ITransactionImportTypeImportProcessingRuleService _transactionImportTypeImportProcessingRuleService;
        private readonly ITransactionImportTypeService _transactionImportTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public EditViewModelBuilder(ILog log
            , ITransactionImportTypeImportProcessingRuleService transactionImportTypeImportProcessingRuleService
            , ITransactionImportTypeService transactionImportTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _transactionImportTypeImportProcessingRuleService = transactionImportTypeImportProcessingRuleService;
            _transactionImportTypeService = transactionImportTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _transactionImportTypeImportProcessingRuleService.Get(id);

            var model = new EditViewModel();

            model.ImportProcessingRules = GetImportProcessingRules(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.TransactionImportTypeId = data.TransactionImportTypeId;
            model.TransactionImportTypeName = data.TransactionImportType.Name;
            model.ImportProcessingRuleId = data.ImportProcessingRuleId;

            return model;
        }

        private SelectList GetImportProcessingRules(BusinessLogic.Entities.TransactionImportTypeImportProcessingRule transactionImportTypeImportProcessingRule)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportProcessingRules(transactionImportTypeImportProcessingRule);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

            return new SelectList(selectListItems, true);
        }

        private IList<BusinessLogic.Entities.ImportProcessingRule> GetAvailableImportProcessingRules(BusinessLogic.Entities.TransactionImportTypeImportProcessingRule transactionImportTypeImportProcessingRule)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);
            var allSelectedImportProcessingRules = _transactionImportTypeService.Get(transactionImportTypeImportProcessingRule.TransactionImportTypeId).ImportProcessingRules.Select(x => x.ImportProcessingRule);
            var importProcessingRuleBeingEdited = allSelectedImportProcessingRules.Where(x => x.Id == transactionImportTypeImportProcessingRule.ImportProcessingRuleId);

            return allImportProcessingRules
                .Except(allSelectedImportProcessingRules)
                .Concat(importProcessingRuleBeingEdited)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}