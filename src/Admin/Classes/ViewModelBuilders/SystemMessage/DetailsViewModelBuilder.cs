using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.SystemMessage
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ISystemMessageService _systemMessageService;

        public DetailsViewModelBuilder(ILog log
            , ISystemMessageService systemMessageService)
            : base(log)
        {
            _systemMessageService = systemMessageService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _systemMessageService.GetSystemMessage(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Message = data.Message,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Severity = data.Severity,
                SeverityDisplayName = GetSeverityDisplayName(data.Severity)
            };
        }

        private string GetSeverityDisplayName(string severity)
        {
            return _systemMessageService.GetSeverities().First(x => x.Key == severity).Value;
        }
    }
}