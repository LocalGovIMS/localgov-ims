using Admin.Models.ImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportType
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeService _importTypeService;

        public EditCommand(ILog log
            , IImportTypeService importTypeService)
            : base(log)
        {
            _importTypeService = importTypeService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportType
            {
                Id = model.Id,
                DataType = (byte)model.DataType,
                Name = model.Name,
                Description = model.Description,
                ExternalReference = model.ExternalReference,
                IsReversible = model.IsReversible
            };

            var result = _importTypeService.Update(item);

            return new CommandResult(result);
        }
    }
}