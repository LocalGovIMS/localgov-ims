using Admin.Models.VatMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.VatMetadata
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IVatMetadataService _service;

        public DetailsViewModelBuilder(ILog log
            , IVatMetadataService fundMetadataService)
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