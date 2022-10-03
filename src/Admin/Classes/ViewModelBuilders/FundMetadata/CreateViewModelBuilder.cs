using Admin.Models.FundMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMetadata
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IFundMetadataService _fundMetadataService;
        private readonly IMetadataKeyService _metadataKeyService;

        public CreateViewModelBuilder(ILog log
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

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.FundCode = args.FundCode;
            model.MetadataKeys = GetMetadataKeysList(args.FundCode);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.MetadataKeys = GetMetadataKeysList(model.FundCode);

            return model;
        }

        private SelectList GetMetadataKeysList(string fundCode)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMetadataKeys(fundCode).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MetadataKey> GetAvailableMetadataKeys(string fundCode)
        {
            var allKeys = _metadataKeyService.Search(new BusinessLogic.Models.MetadataKey.SearchCriteria() { ApplyPaging = false, EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Fund });
            var allUsedKeyIds = _fundMetadataService.Search(new BusinessLogic.Models.FundMetadata.SearchCriteria() { FundCode = fundCode })
                .Items.Select(x => x.MetadataKeyId);
            var allUsedKeys = allKeys.Items.Where(x => allUsedKeyIds.Contains(x.Id));

            return allKeys.Items.Except(allUsedKeys).ToList();
        }
    }

    public class CreateViewModelBuilderArgs
    {
        public string FundCode { get; set; }
    }
}