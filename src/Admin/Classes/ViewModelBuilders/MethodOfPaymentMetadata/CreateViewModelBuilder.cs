using Admin.Models.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IMethodOfPaymentMetadataService _methodOfPaymentMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public CreateViewModelBuilder(ILog log
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

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.MopCode = args.MopCode;
            model.MetadataKeys = GetMetadataKeysList(args.MopCode);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.MetadataKeys = GetMetadataKeysList(model.MopCode);

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

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(string mopCode)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Mop });
            var allUsedKeyIds = _methodOfPaymentMetadataService.Search(new BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria() { MopCode = mopCode })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id));

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }

    public class CreateViewModelBuilderArgs
    {
        public string MopCode{ get; set; }
    }
}