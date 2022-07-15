using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleImportType
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IImportTypeImportProcessingRuleService _service;

        public DeleteCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}