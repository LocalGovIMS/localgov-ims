using Admin.Models.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IMethodOfPaymentMetadataService _methodOfPaymentMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public EditViewModelBuilder(ILog log
            , IMethodOfPaymentMetadataService methodOfPaymentMetadataService
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _methodOfPaymentMetadataService = methodOfPaymentMetadataService;
            _metadataKeyService = metadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _methodOfPaymentMetadataService.Get(id);

            var model = new EditViewModel();

            model.MetadataKeys = GetMetadataKeysList(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.MopCode = data.MopCode;
            model.MetadataKeyId = data.MetadataKeyId;
            model.MetadataKeyName = data.MetadataKey.Name;
            model.MetadataKeyDescription = data.MetadataKey.Description;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetMetadataKeysList(BusinessLogic.Entities.MopMetadata mopMetadata)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(mopMetadata).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(BusinessLogic.Entities.MopMetadata mopMetadata)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Mop });
            var allUsedKeyIds = _methodOfPaymentMetadataService.Search(new BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria() { MopCode = mopMetadata.MopCode })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id) && x.Id != mopMetadata.MetadataKeyId);

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }
}