using Admin.Models.Office;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Office
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IOfficeService _officeService;

        public CreateCommand(ILog log
            , IOfficeService officeService)
            : base(log)
        {
            _officeService = officeService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Office()
            {
                OfficeCode = model.Code,
                Name = model.Name
            };

            var result = _officeService.Create(item);

            return new CommandResult(result);
        }
    }
}