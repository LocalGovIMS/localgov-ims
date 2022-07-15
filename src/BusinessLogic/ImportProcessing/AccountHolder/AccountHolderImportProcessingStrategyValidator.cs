using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;

namespace BusinessLogic.ImportProcessing
{
    public class AccountHolderImportProcessingStrategyValidator : BaseImportProcessingStrategyValidator
    {
        protected override ImportDataTypeEnum DataType => ImportDataTypeEnum.AccountHolder;

        public AccountHolderImportProcessingStrategyValidator(IImportTypeService importTypeService)
            : base(importTypeService)
        {
        }        
    }
}
