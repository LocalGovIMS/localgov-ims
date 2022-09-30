using Admin.Models.MetadataKey;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MetadataKey
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IMetadataKeyService _metadataKeyService;

        public CreateCommand(ILog log
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
                SystemType = false, // If we're creating this via Admin, then it's not a system type
                EntityType = (byte)model.EntityType
            };

            var result = _metadataKeyService.Create(item);

            return new CommandResult(result);
        }
    }
}