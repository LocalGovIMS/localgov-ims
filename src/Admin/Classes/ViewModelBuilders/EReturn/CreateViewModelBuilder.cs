using Admin.Models.EReturn;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.EReturn
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<CreateViewModel, int>
    {
        private readonly ITemplateService _templateService;
        private readonly IEReturnTypeService _eReturnTypeService;

        public CreateViewModelBuilder(ILog log
            , ITemplateService templateService
            , IEReturnTypeService eReturnTypeService)
            : base(log)
        {
            _templateService = templateService;
            _eReturnTypeService = eReturnTypeService;
        }

        protected override CreateViewModel OnBuild()
        {
            var model = new CreateViewModel
            {
                Templates = GetTemplatesList(),
                Types = GetTypesList()
            };

            return model;
        }

        protected override CreateViewModel OnBuild(int id)
        {
            throw new NotImplementedException();
        }

        protected override CreateViewModel OnRebuild(CreateViewModel model)
        {
            model.Templates = GetTemplatesList();
            model.Types = GetTypesList();

            return model;
        }

        private SelectList GetTemplatesList()
        {
            var templateSelectListItems = new List<SelectListItem>();
            var templates = _templateService.GetAllTemplates();

            foreach (var template in templates)
            {
                var dataAttributes = new List<ValuePair>
                {
                    new ValuePair() {Key = "cash", Value = template.Cash.ToString()},
                    new ValuePair() {Key = "cheque", Value = template.Cheque.ToString()},
                    new ValuePair() {Key = "pdq", Value = template.Pdq.ToString()}
                };


                templateSelectListItems.Add(new SelectListItem()
                {
                    Value = template.Id.ToString(),
                    Text = template.Name,
                    DataAttributes = dataAttributes
                });
            }
            return new SelectList(templateSelectListItems, true);
        }

        private SelectList GetTypesList()
        {
            var typelistItem = new List<SelectListItem>();
            var items = _eReturnTypeService.GetAllEReturnTypes();

            foreach (var item in items)
            {
                var dataAttributes = new List<ValuePair>();

                typelistItem.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName,
                    DataAttributes = dataAttributes
                });
            }
            return new SelectList(typelistItem, true);
        }
    }
}