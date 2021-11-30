using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.SystemMessage
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, int>
    {
        private readonly ISystemMessageService _systemMessageService;

        public ListViewModelBuilder(ILog log
            , ISystemMessageService systemMessageService)
            : base(log)
        {
            _systemMessageService = systemMessageService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _systemMessageService
               .GetSystemMessages()
               .OrderByDescending(x => x.StartDate)
               .Select(x => new DetailsViewModel()
               {
                   Id = x.Id,
                   Message = x.Message,
                   StartDate = x.StartDate,
                   EndDate = x.EndDate,
                   Severity = x.Severity,
                   SeverityDisplayName = GetSeverityDisplayName(x.Severity)
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(int id)
        {
            throw new NotImplementedException();
        }

        private string GetSeverityDisplayName(string severity)
        {
            return _systemMessageService.GetSeverities().First(x => x.Key == severity).Value;
        }
    }
}