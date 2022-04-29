using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.UserTemplate
{
    public class BasicListViewModelBuilder : BaseViewModelBuilder<BasicListViewModel, int>
    {
        private readonly IUserTemplateService _userTemplateService;

        public BasicListViewModelBuilder(ILog log
            , IUserTemplateService userTemplateService)
            : base(log)
        {
            _userTemplateService = userTemplateService;
        }

        protected override BasicListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override BasicListViewModel OnBuild(int id)
        {
            var data = _userTemplateService.GetUserTemplates(id).OrderBy(x => x.Template.Name);

            var model = new BasicListViewModel()
            {
                ListTitle = "User Templates",
                ColumnTitle = "Template",
                NoItemsMessage = "There are no user templates specified",
                Items = data.Select(x => x.Template.Name).ToList()
            };

            return model;
        }
    }
}