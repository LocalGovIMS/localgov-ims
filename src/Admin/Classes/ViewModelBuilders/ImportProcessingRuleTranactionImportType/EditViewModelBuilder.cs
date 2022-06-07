using Admin.Models.ImportProcessingRuleTransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleTransactionImportType
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

            model.TransactionImportTypes = GetTransactionImportTypes(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.TransactionImportTypeId = data.TransactionImportTypeId;
            model.ImportProcessingRuleId = data.ImportProcessingRuleId;
            model.ImportProcessingRuleName = data.ImportProcessingRule.Name;

            return model;
        }

        private SelectList GetTransactionImportTypes(BusinessLogic.Entities.TransactionImportTypeImportProcessingRule transactionImportTypeImportProcessingRule)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableTransacitonImportTypes(transactionImportTypeImportProcessingRule);

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

        private IList<BusinessLogic.Entities.TransactionImportType> GetAvailableTransacitonImportTypes(BusinessLogic.Entities.TransactionImportTypeImportProcessingRule transactionImportTypeImportProcessingRule)
        {
            var allTransactionImportTypes = _transactionImportTypeService.GetAll();
            var allSelectedTransactionImportTypes = _importProcessingRuleService.Get(transactionImportTypeImportProcessingRule.ImportProcessingRuleId).TransactionImportTypes.Select(x => x.TransactionImportType);
            var transactionImportTypeBeingEdited = allSelectedTransactionImportTypes.Where(x => x.Id == transactionImportTypeImportProcessingRule.TransactionImportTypeId);

            return allTransactionImportTypes
                .Except(allSelectedTransactionImportTypes)
                .Concat(transactionImportTypeBeingEdited)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}