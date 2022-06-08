using Admin.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ITransactionImportTypeService _transactionImportTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public CreateViewModelBuilder(ILog log
            , ITransactionImportTypeService transactionImportTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _transactionImportTypeService = transactionImportTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var model = new EditViewModel();

            model.TransactionImportTypeId = id;
            
            model.ImportProcessingRules = GetImportProcessingRules(model.TransactionImportTypeId);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.ImportProcessingRules = GetImportProcessingRules(model.TransactionImportTypeId);

            return model;
        }

        private SelectList GetImportProcessingRules(int transactionImportTypeId)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportProcessingRules(transactionImportTypeId);

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

        private IList<BusinessLogic.Entities.ImportProcessingRule> GetAvailableImportProcessingRules(int transactionImportTypeId)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);
            var allSelectedImportProcessingRules = _transactionImportTypeService.Get(transactionImportTypeId).ImportProcessingRules.Select(x => x.ImportProcessingRule);

            return allImportProcessingRules.Except(allSelectedImportProcessingRules).ToList();
        }
    }
}