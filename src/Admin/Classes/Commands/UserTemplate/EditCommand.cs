using Admin.Models.UserTemplate;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.Commands.UserTemplate
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserTemplateService _userTemplateService;

        public EditCommand(ILog log
            , IUserTemplateService userTemplateService)
            : base(log)
        {
            _userTemplateService = userTemplateService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var items = model.Items
                .Where(x => x.IsChecked == true)
                .Select(x => new BusinessLogic.Entities.UserTemplate
                {
                    UserId = model.UserId,
                    TemplateId = Convert.ToInt32(x.Id)
                }).ToList();

            var result = _userTemplateService.Update(items, model.UserId);

            return new CommandResult(result);
        }
    }
}