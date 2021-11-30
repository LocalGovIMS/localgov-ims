using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.SystemMessage
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ISystemMessageService _systemMessageService;

        public EditViewModelBuilder(ILog log
            , ISystemMessageService systemMessageService)
            : base(log)
        {
            _systemMessageService = systemMessageService;
        }

        protected override EditViewModel OnBuild()
        {
            var model = new EditViewModel
            {
                Message = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Severity = "INFO",
                SeverityList = GetSeverityList()
            };

            return model;
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _systemMessageService.GetSystemMessage(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Message = data.Message;
            model.StartDate = data.StartDate;
            model.EndDate = data.EndDate;
            model.Severity = data.Severity;
            model.SeverityList = GetSeverityList();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.SeverityList = GetSeverityList();

            return model;
        }

        private SelectList GetSeverityList()
        {
            var severities = _systemMessageService.GetSeverities();

            var severityListItems = severities.Select(x => new SelectListItem() { Text = x.Value, Value = x.Key }).ToList();

            return new SelectList(severityListItems, false);
        }
    }
}