using Admin.Models.MetadataKey;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.MetadataKey
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IMetadataKeyService _metadataKeyService;

        public DetailsViewModelBuilder(ILog log
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _metadataKeyService = metadataKeyService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _metadataKeyService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                IsASystemType = data.SystemType,
                EntityType = (MetadataKeyEntityType)data.EntityType
            };
        }
    }
}