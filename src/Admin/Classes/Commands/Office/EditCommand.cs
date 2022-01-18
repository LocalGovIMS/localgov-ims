using Admin.Models.Office;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Office
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IOfficeService _officeService;

        public EditCommand(ILog log
            , IOfficeService officeService)
            : base(log)
        {
            _officeService = officeService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Office
            {
                OfficeCode = model.Code,
                Name= model.Name
            };

            var result = _officeService.Update(item);

            return new CommandResult(result);
        }
    }
}