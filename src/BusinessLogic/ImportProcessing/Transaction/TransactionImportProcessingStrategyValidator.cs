using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessingStrategyValidator : BaseImportProcessingStrategyValidator
    {
        protected override ImportDataTypeEnum DataType => ImportDataTypeEnum.Transaction;

        public TransactionImportProcessingStrategyValidator(IImportTypeService importTypeService)
            : base(importTypeService)
        {
        }
    }
}
