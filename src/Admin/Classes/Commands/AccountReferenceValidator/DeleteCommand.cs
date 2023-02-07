using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.AccountReferenceValidator
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IAccountReferenceValidatorService _service;

        public DeleteCommand(ILog log
            , IAccountReferenceValidatorService accountReferenceValidatorService)
            : base(log)
        {
            _service = accountReferenceValidatorService ?? throw new ArgumentNullException("accountReferenceValidatorService");
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}