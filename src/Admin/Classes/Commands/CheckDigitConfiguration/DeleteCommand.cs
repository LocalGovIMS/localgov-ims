using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.CheckDigitConfiguration
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly ICheckDigitConfigurationService _service;

        public DeleteCommand(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
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