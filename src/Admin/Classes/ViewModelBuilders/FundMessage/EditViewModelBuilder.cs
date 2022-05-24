using Admin.Models.FundMessage;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMessage
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IFundMessageService _fundMessageService;
        private readonly IFundService _fundService;
        
        public EditViewModelBuilder(ILog log
            , IFundMessageService vatService
            , IFundService fundService)
            : base(log)
        {
            _fundMessageService = vatService;
            _fundService = fundService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _fundMessageService.GetById(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.FundCode = data.FundCode;
            model.FundName = data.Fund.FundName;
            model.Message = data.Message;
            
            model.Funds = GetFunds();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.Funds = GetFunds();

            return model;
        }

        private SelectList GetFunds()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _fundService.GetAllFunds();

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.FundCode,
                    Text = item.FundName,
                });
            }

            return new SelectList(selectListItems, true);
        }
    }
}