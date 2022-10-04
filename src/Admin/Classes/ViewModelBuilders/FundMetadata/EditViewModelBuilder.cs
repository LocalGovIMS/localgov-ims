using Admin.Models.FundMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMetadata
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IFundMetadataService _fundMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public EditViewModelBuilder(ILog log
            , IFundMetadataService fundMetadataService
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _fundMetadataService = fundMetadataService;
            _metadataKeyService = metadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _fundMetadataService.Get(id);

            var model = new EditViewModel();

            model.MetadataKeys = GetMetadataKeysList(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.FundCode = data.FundCode;
            model.MetadataKeyId = data.MetadataKeyId;
            model.MetadataKeyName = data.MetadataKey.Name;
            model.MetadataKeyDescription = data.MetadataKey.Description;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetMetadataKeysList(BusinessLogic.Entities.FundMetadata fundMetadata)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(fundMetadata).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(BusinessLogic.Entities.FundMetadata fundMetadata)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Fund });
            var allUsedKeyIds = _fundMetadataService.Search(new BusinessLogic.Models.FundMetadata.SearchCriteria() { FundCode = fundMetadata.FundCode })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id) && x.Id != fundMetadata.MetadataKeyId);

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }
}