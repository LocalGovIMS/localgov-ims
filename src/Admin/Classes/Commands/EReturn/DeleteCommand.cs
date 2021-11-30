﻿using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturn
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IEReturnService _eReturnService;

        public DeleteCommand(ILog log
            , IEReturnService eReturnService)
            : base(log)
        {
            _eReturnService = eReturnService;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _eReturnService.Delete(id);

            return new CommandResult(result);
        }
    }
}