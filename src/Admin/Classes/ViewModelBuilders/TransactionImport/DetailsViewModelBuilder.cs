using Admin.Models.TransactionImport;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.TransactionImport
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ITransactionImportService _transactionImportService;

        public DetailsViewModelBuilder(ILog log
            , ITransactionImportService transactionImportService)
            : base(log)
        {
            _transactionImportService = transactionImportService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _transactionImportService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                TransactionImportTypeName = data.TransactionImportType.Name,
                CreatedDate = data.CreatedDate,
                CreatedByUserName = data.CreatedByUser.UserName,
                ExternalReference = data.ExternalReference,
                Description = data.Description,
                TotalAmount = data.TotalAmount,
                TotalNumberOfTransactions = Convert.ToInt32(data.TotalNumberOfTransactions),
                ReversedDate = data.ReversedDate,
                CurrentStatus = data.CurrentStatus(),
                HasErrors = data.HasErrors(),
                StatusHistories = GetStatusHistories(data),
                EventLogs = GetEventLogs(data)
            };
        }

        private List<StatusHistoryViewModel> GetStatusHistories(BusinessLogic.Entities.TransactionImport data)
        {
            if(data.StatusHistories.Any())
            {
                return data.StatusHistories.Select(x => new StatusHistoryViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    CreatedByUserName = x.CreatedByUser.UserName,
                    Status = (TransactionImportStatusEnum)x.StatusId
                }).ToList();
            }

            return new List<StatusHistoryViewModel>();
        }

        private List<EventLogViewModel> GetEventLogs(BusinessLogic.Entities.TransactionImport data)
        {
            if (data.EventLogs.Any())
            {
                return data.EventLogs.Select(x => new EventLogViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    Type = (TransactionImportEventLogTypeEnum)x.Type,
                    Message = x.Message
                }).ToList();
            }

            return new List<EventLogViewModel>();
        }
    }
}