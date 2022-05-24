using Admin.Models.FundMessageMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMessageMetadata
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IFundMessageMetadataService _service;

        public CreateViewModelBuilder(ILog log
            , IFundMessageMetadataService fundMetadataService)
            : base(log)
        {
            _service = fundMetadataService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.FundMessageId = args.FundMessageId;
            model.Keys = GetKeyList();

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.Keys = GetKeyList();

            return model;
        }

        private SelectList GetKeyList()
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in _service.GetMetadata().OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Key.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }
    }

    public class CreateViewModelBuilderArgs
    {
        public int FundMessageId { get; set; }
    }
}