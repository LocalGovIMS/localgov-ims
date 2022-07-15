using Admin.Models.Import;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Import
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportService _ImportService;

        public DetailsViewModelBuilder(ILog log
            , IImportService ImportService)
            : base(log)
        {
            _ImportService = ImportService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _ImportService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                DataType = (ImportDataTypeEnum)data.ImportType.DataType,
                ImportTypeName = data.ImportType.Name,
                CreatedDate = data.CreatedDate,
                CreatedByUserName = data.CreatedByUser.UserName,
                Notes = data.Notes,
                NumberOfRows = Convert.ToInt32(data.NumberOfRows),
                ReversedDate = data.ReversedDate,
                CurrentStatus = data.CurrentStatus(),
                HasErrors = data.HasErrors(),
                StatusHistories = GetStatusHistories(data),
                EventLogs = GetEventLogs(data)
            };
        }

        private List<StatusHistoryViewModel> GetStatusHistories(BusinessLogic.Entities.Import data)
        {
            if(data.StatusHistories.Any())
            {
                return data.StatusHistories.Select(x => new StatusHistoryViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser.UserName,
                    Status = (ImportStatusEnum)x.StatusId
                }).ToList();
            }

            return new List<StatusHistoryViewModel>();
        }

        private List<EventLogViewModel> GetEventLogs(BusinessLogic.Entities.Import data)
        {
            if (data.EventLogs.Any())
            {
                return data.EventLogs.Select(x => new EventLogViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    Type = (ImportEventLogTypeEnum)x.Type,
                    Message = x.Message
                }).ToList();
            }

            return new List<EventLogViewModel>();
        }
    }
}