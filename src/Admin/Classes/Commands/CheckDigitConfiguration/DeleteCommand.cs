using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.CheckDigitConfiguration
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly ICheckDigitConfigurationService _service;

        public DeleteCommand(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService ?? throw new ArgumentNullException("checkDigitConfigurationService");
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}