using Admin.Models.FundMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.FundMessage
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IFundMessageService _fundMessageService;

        public DetailsViewModelBuilder(ILog log
            , IFundMessageService fundMessageService)
            : base(log)
        {
            _fundMessageService = fundMessageService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _fundMessageService.GetById(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                FundCode = data.FundCode,
                FundName = data.Fund.FundName,
                Message = data.Message
            };
        }
    }
}