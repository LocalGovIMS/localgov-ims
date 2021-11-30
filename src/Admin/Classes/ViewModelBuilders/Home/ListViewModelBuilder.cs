using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Home
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, string>
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
               .GetActiveSystemMessages()
               .Select(x => new DetailsViewModel()
               {
                   Message = x.Message,
                   Severity = x.Severity,
                   StartDate = x.StartDate,
                   EndDate = x.EndDate,
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(string id)
        {
            throw new NotImplementedException();
        }
    }
}