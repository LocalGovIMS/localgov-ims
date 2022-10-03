using Admin.Models.VatMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.VatMetadata
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IVatMetadataService _vatMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public EditViewModelBuilder(ILog log
            , IVatMetadataService vatMetadataService
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _vatMetadataService = vatMetadataService;
            _metadataKeyService = metadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _vatMetadataService.Get(id);

            var model = new EditViewModel();

            model.MetadataKeys = GetMetadataKeysList(model.VatCode);

            if (data == null) return model;

            model.Id = data.Id;
            model.VatCode = data.VatCode;
            model.MetadataKeyId = data.MetadataKeyId;
            model.MetadataKeyName = data.MetadataKey.Name;
            model.MetadataKeyDescription = data.MetadataKey.Description;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetMetadataKeysList(string mopCode)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(mopCode).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(string vatCode)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Vat });
            var allUsedKeyIds = _vatMetadataService.Search(new BusinessLogic.Models.VatMetadata.SearchCriteria() { VatCode = vatCode })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id));

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }
}