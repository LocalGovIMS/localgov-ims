using Admin.Interfaces.Commands;
using BusinessLogic.Models;
using log4net;
using System;

namespace Admin.Controllers
{
    public class ValidationControllerDependencies : BaseControllerDependencies, IValidationControllerDependencies
    {
        public IModelCommand<TransferItem> ValidateTransferItemCommand { get; private set; }

        public ValidationControllerDependencies(ILog log
            , IModelCommand<TransferItem> validateTransferItemCommand) : base(log)
        {
            ValidateTransferItemCommand = validateTransferItemCommand ?? throw new ArgumentNullException("validateTransferItemCommand");
        }
    }
}