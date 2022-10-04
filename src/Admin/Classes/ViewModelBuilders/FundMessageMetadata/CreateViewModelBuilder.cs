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
        private readonly IFundMessageMetadataService _fundMessageMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public CreateViewModelBuilder(ILog log
            , IFundMessageMetadataService fundMessageMetadataService
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _fundMessageMetadataService = fundMessageMetadataService;
            _metadataKeyService = metadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.FundMessageId = args.FundMessageId;
            model.MetadataKeys = GetMetadataKeysList(model.FundMessageId);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.MetadataKeys = GetMetadataKeysList(model.FundMessageId);

            return model;
        }

        private SelectList GetMetadataKeysList(int fundMessageId)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(fundMessageId).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(int fundMessageId)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.FundMessage });
            var allUsedKeyIds = _fundMessageMetadataService.Search(new BusinessLogic.Models.FundMessageMetadata.SearchCriteria() { FundMessageId = fundMessageId })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id));

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }

    public class CreateViewModelBuilderArgs
    {
        public int FundMessageId { get; set; }
    }
}