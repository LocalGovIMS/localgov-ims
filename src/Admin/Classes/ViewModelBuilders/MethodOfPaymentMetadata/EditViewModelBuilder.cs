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
        private readonly IMethodOfPaymentMetadataKeyService _methodOfPaymentMetadataKeyService;

        public EditViewModelBuilder(ILog log
            , IMethodOfPaymentMetadataService methodOfPaymentMetadataService
            , IMethodOfPaymentMetadataKeyService methodOfPaymentMetadataKeyService)
            : base(log)
        {
            _methodOfPaymentMetadataService = methodOfPaymentMetadataService;
            _methodOfPaymentMetadataKeyService = methodOfPaymentMetadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _methodOfPaymentMetadataService.Get(id);

            var model = new EditViewModel();

            model.MopMetadataKeys = GetMopMetadataKeysList(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.MopCode = data.MopCode;
            model.MopMetadataKeyId = data.MopMetadataKeyId;
            model.MopMetadataKeyName = data.MopMetadataKey.Name;
            model.MopMetadataKeyDescription = data.MopMetadataKey.Description;
            model.Value = data.Value;

            return model;
        }

        private SelectList GetMopMetadataKeysList(BusinessLogic.Entities.MopMetadata mopMetadata)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMopMetadataKeys(mopMetadata).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MopMetadataKey> GetAvailableMopMetadataKeys(BusinessLogic.Entities.MopMetadata mopMetadata)
        {
            var allKeys = _methodOfPaymentMetadataKeyService.GetAll();
            var allUsedKeyIds = _methodOfPaymentMetadataService.Search(new BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria() { MopCode = mopMetadata.MopCode })
                .Items.Select(x => x.MopMetadataKeyId);
            var allUsedKeys = allKeys.Where(x => allUsedKeyIds.Contains(x.Id) && x.Id != mopMetadata.MopMetadataKeyId);

            return allKeys.Except(allUsedKeys).ToList();
        }
    }
}