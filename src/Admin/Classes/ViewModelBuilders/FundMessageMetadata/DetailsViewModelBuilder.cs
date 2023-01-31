using Admin.Models.FundMessageMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.FundMessageMetadata
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IFundMessageMetadataService _service;

        public DetailsViewModelBuilder(ILog log
            , IFundMessageMetadataService fundMetadataService)
            : base(log)
        {
            _service = fundMetadataService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _service.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                MetadataKeyId = data.MetadataKeyId,
                MetadataKeyName = data.MetadataKey.Name,
                MetadataKeyDescription = data.MetadataKey.Description,
                Value = data.Value
            };
        }
    }
}