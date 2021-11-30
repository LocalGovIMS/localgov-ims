using Admin.Models.FundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Linq;

namespace Admin.Classes.Commands.FundGroup
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundGroupService _fundGroupService;

        public EditCommand(ILog log
            , IFundGroupService fundGroupService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundGroup
            {
                FundGroupId = model.Id,
                Name = model.FundGroupName,
                FundGroupFunds = model.Funds.Where(x => x.IsChecked).Select(x => new BusinessLogic.Entities.FundGroupFund
                {
                    FundGroupId = model.Id,
                    FundCode = x.Id
                }).ToList()
            };

            var result = _fundGroupService.Update(item);

            return new CommandResult(result);
        }
    }
}