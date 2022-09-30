using Admin.Models.MetadataKey;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.MetadataKey
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IMetadataKeyService _metadataKeyService;
        
        public EditViewModelBuilder(ILog log
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _metadataKeyService = metadataKeyService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _metadataKeyService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.Description = data.Description;
            model.IsASystemType = data.SystemType;
            model.EntityType = (MetadataKeyEntityType)data.EntityType;

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            return model;
        }
    }
}