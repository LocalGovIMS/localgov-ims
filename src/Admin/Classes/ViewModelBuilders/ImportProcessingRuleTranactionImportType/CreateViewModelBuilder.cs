using Admin.Models.ImportProcessingRuleTransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleTransactionImportType
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleService _importProcessingRuleService;
        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public CreateViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _importProcessingRuleService = importProcessingRuleService;
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var model = new EditViewModel();

            model.ImportProcessingRuleId = id;

            model.TransactionImportTypes = GetTransactionImportTypes(model.ImportProcessingRuleId);

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.TransactionImportTypes = GetTransactionImportTypes(model.ImportProcessingRuleId);

            return model;
        }

        private SelectList GetTransactionImportTypes(int importProcessingRuleId)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableTransactionImportTypes(importProcessingRuleId);

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

        private IList<BusinessLogic.Entities.TransactionImportType> GetAvailableTransactionImportTypes(int importProcessingRuleId)
        {
            var allTransactionImportTypes = _transactionImportTypeService.GetAll();
            var allSelectedTransactionImportTypes = _importProcessingRuleService.Get(importProcessingRuleId).TransactionImportTypes.Select(x => x.TransactionImportType);

            return allTransactionImportTypes.Except(allSelectedTransactionImportTypes).ToList();
        }
    }
}