using Admin.Models.AccountHolder;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.AccountHolder
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IAccountHolderService _accountHolderService;
        private readonly IFundService _fundService;
        private readonly IStopMessageService _stopMessageService;

        public EditViewModelBuilder(ILog log
            , IAccountHolderService accountHolderService
            , IFundService fundService
            , IStopMessageService stopMessageService)
            : base(log)
        {
            _accountHolderService = accountHolderService;
            _fundService = fundService;
            _stopMessageService = stopMessageService;
        }

        protected override EditViewModel OnBuild()
        {
            var model = new EditViewModel();

            model.Funds = GetFundsList();
            model.StopMessages = GetStopMessagesList();

            return model;
        }

        protected override EditViewModel OnBuild(string id)
        {
            var data = _accountHolderService.GetByAccountReference(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.AccountReference = data.AccountReference;
            model.FundCode = data.FundCode;
            model.CurrentBalance = data.CurrentBalance;
            model.PeriodDebit = data.PeriodDebit;
            model.Title = data.Title;
            model.Forename = data.Forename;
            model.Surname = data.Surname;
            model.AddressLine1 = data.AddressLine1;
            model.AddressLine2 = data.AddressLine2;
            model.AddressLine3 = data.AddressLine3;
            model.AddressLine4 = data.AddressLine4;
            model.Postcode = data.Postcode;
            model.PeriodCredit = data.PeriodCredit;
            model.RecordType = data.RecordType;
            model.UserField1 = data.UserField1;
            model.UserField2 = data.UserField2;
            model.UserField3 = data.UserField3;
            model.StopMessageReference = data.StopMessageReference;
            
            model.Funds = GetFundsList();
            model.StopMessages = GetStopMessagesList();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.Funds = GetFundsList();
            model.StopMessages = GetStopMessagesList();

            return base.OnRebuild(model);
        }

        private SelectList GetFundsList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _fundService.GetAllFunds(true).OrderBy(x => x.FundName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.FundCode,
                    Text = string.Format("{0} {1}", item.FundName, item.Disabled ? "(Disabled)" : string.Empty)
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetStopMessagesList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _stopMessageService.GetAll().OrderBy(x => x.Message);

            foreach (var item in items)
            {
                var dataAttributes = new List<ValuePair>
                {
                    new ValuePair() { Key = "fundCode", Value = item.FundCode }
                };

                selectListItems.Add(new SelectListItem()
                {
                    Value = item.FundCode,
                    Text = item.Message,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(selectListItems, true);
        }       
    }
}