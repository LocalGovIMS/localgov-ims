using Admin.Models.UserFundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.Commands.UserFundGroup
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserFundGroupService _userFundGroupService;

        public EditCommand(ILog log
            , IUserFundGroupService userFundGroupService)
            : base(log)
        {
            _userFundGroupService = userFundGroupService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var items = model.FundGroups
                .Where(x => x.IsChecked == true)
                .Select(x => new BusinessLogic.Entities.UserFundGroup
                {
                    UserId = model.UserId,
                    FundGroupId = Convert.ToInt32(x.Id)
                }).ToList();

            var result = _userFundGroupService.Update(items, model.UserId);

            return new CommandResult(result);
        }
    }
}