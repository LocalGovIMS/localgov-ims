using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.AccountReferenceValidator
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IAccountReferenceValidatorService _service;

        public DeleteCommand(ILog log
            , IAccountReferenceValidatorService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}