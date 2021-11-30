using Admin.Models.Template;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Template
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, int>
    {
        private readonly ITemplateService _templateService;

        public ListViewModelBuilder(ILog log, ITemplateService templateService) : base(log)
        {
            _templateService = templateService;
        }

        protected override ListViewModel OnBuild()
        {
            var model = new ListViewModel
            {
                Templates = _templateService.GetAllTemplates()
            };

            return model;
        }

        protected override ListViewModel OnBuild(int id)
        {
            throw new NotImplementedException();
        }
    }
}