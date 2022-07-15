using Admin.Models.ImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportType
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeService _importTypeService;

        public CreateCommand(ILog log
            , IImportTypeService importTypeService)
            : base(log)
        {
            _importTypeService = importTypeService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportType()
            {
                DataType = (byte)model.DataType,
                Name = model.Name,
                Description = model.Description,
                ExternalReference = model.ExternalReference,
                IsReversible = model.IsReversible
            };

            var result = _importTypeService.Create(item);

            return new CommandResult(result);
        }
    }
}