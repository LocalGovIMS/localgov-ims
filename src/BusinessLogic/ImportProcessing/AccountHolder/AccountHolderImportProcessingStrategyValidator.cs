using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using System;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class AccountHolderImportProcessingStrategyValidator : BaseImportProcessingStrategyValidator
    {
        protected override ImportDataTypeEnum DataType => ImportDataTypeEnum.AccountHolder;

        private readonly IFundService _fundService;

        public AccountHolderImportProcessingStrategyValidator(
            IImportTypeService importTypeService,
            IFundService fundService) : base(importTypeService)
        {
            _fundService = fundService ?? throw new ArgumentNullException("fundService");
        }        

        protected override void OnValidate(Import import)
        {
            ValidateFundCodes(import);
        }

        private void ValidateFundCodes(Import import)
        {
            var allFunds = _fundService.GetAllFunds();
            var rowNumber = 1;

            foreach(var item in import.Rows)
            {
                var accountHolder = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountHolder>(item.Data);

                if (!allFunds.Any(x => x.FundCode == accountHolder.FundCode))
                    throw new ImportProcessingException($"Fund code '{accountHolder.FundCode}' on row {rowNumber} not recognised");

                rowNumber++;
            }
        }
    }
}
