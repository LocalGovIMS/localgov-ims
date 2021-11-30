using Admin.Models.FundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Linq;

namespace Admin.Classes.Commands.FundGroup
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundGroupService _fundGroupService;

        public CreateCommand(ILog log
            , IFundGroupService fundGroupService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundGroup()
            {
                Name = model.FundGroupName,
                FundGroupFunds = model.Funds.Where(x => x.IsChecked).Select(x => new BusinessLogic.Entities.FundGroupFund
                {
                    FundCode = x.Id
                }).ToList()
            };

            var result = _fundGroupService.Create(item);

            return new CommandResult(result);
        }
    }
}