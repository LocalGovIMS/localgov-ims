using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.AccountReferenceValidator
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IAccountReferenceValidatorService _service;

        public DeleteCommand(ILog log
            , IAccountReferenceValidatorService accountReferenceValidatorService)
            : base(log)
        {
            _service = accountReferenceValidatorService;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}