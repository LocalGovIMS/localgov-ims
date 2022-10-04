using Admin.Models.FundMessageMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMessageMetadata
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IFundMessageMetadataService _fundMessageMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public EditViewModelBuilder(ILog log
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

        protected override EditViewModel OnBuild(int id)
        {
            var data = _fundMessageMetadataService.Get(id);

            var model = new EditViewModel();

            model.MetadataKeys = GetMetadataKeysList(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.FundMessageId = data.FundMessageId;
            model.MetadataKeyId = data.MetadataKeyId;
            model.MetadataKeyName = data.MetadataKey.Name;
            model.MetadataKeyDescription = data.MetadataKey.Description;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetMetadataKeysList(BusinessLogic.Entities.FundMessageMetadata fundMessageMetadata)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(fundMessageMetadata).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(BusinessLogic.Entities.FundMessageMetadata fundMessageMetadata)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.FundMessage });
            var allUsedKeyIds = _fundMessageMetadataService.Search(new BusinessLogic.Models.FundMessageMetadata.SearchCriteria() { FundMessageId = fundMessageMetadata.FundMessageId })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id) && x.Id != fundMessageMetadata.MetadataKeyId);

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }
}