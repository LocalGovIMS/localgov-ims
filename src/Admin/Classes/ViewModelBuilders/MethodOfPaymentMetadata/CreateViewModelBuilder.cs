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
        private readonly IMethodOfPaymentMetadataKeyService _methodOfPaymentMetadataKeyService;

        public CreateViewModelBuilder(ILog log
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

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.MopCode = args.MopCode;
            model.MopMetadataKeys = GetMopMetadataKeysList(args.MopCode);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.MopMetadataKeys = GetMopMetadataKeysList(model.MopCode);

            return model;
        }

        private SelectList GetMopMetadataKeysList(string mopCode)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in GetAvailableMopMetadataKeys(mopCode).OrderBy(x => x.Description))
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Description
                });
            }

            return new SelectList(selectListItems, false);
        }

        private IList<BusinessLogic.Entities.MopMetadataKey> GetAvailableMopMetadataKeys(string mopCode)
        {
            var allKeys = _methodOfPaymentMetadataKeyService.GetAll();
            var allUsedKeyIds = _methodOfPaymentMetadataService.Search(new BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria() { MopCode = mopCode })
                .Items.Select(x => x.MopMetadataKeyId);
            var allUsedKeys = allKeys.Where(x => allUsedKeyIds.Contains(x.Id));

            return allKeys.Except(allUsedKeys).ToList();
        }
    }

    public class CreateViewModelBuilderArgs
    {
        public string MopCode{ get; set; }
    }
}