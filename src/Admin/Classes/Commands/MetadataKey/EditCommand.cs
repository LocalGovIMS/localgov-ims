using Admin.Models.MetadataKey;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MetadataKey
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IMetadataKeyService _metadataKeyService;

        public EditCommand(ILog log
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _metadataKeyService = metadataKeyService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.MetadataKey()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                SystemType = model.IsASystemType,
                EntityType = (byte)model.EntityType
            };

            var result = _metadataKeyService.Update(item);

            return new CommandResult(result);
        }
    }
}