using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class AccountHolderImportProcessingStrategyValidator : BaseImportProcessingStrategyValidator
    {
        protected override ImportDataTypeEnum DataType => ImportDataTypeEnum.AccountHolder;

        private readonly IFundService _fundService;
        private readonly IAccountHolderFundMessageValidator _accountHolderFundMessageValidator;
        private int _rowNumber = 1;
        private List<Fund> _allFunds = new List<Fund>();

        public AccountHolderImportProcessingStrategyValidator(
            IImportTypeService importTypeService,
            IAccountHolderFundMessageValidator accountHolderFundMessageValidator,
            IFundService fundService) : base(importTypeService)
        {
            _fundService = fundService ?? throw new ArgumentNullException("fundService");
            _accountHolderFundMessageValidator = accountHolderFundMessageValidator ?? throw new ArgumentNullException("accountHolderFundMessageValidator");
        }        

        protected override void OnValidate(ImportProcessingStrategyValidatorArgs args)
        {
            Initialise();

            foreach (var item in args.ImportRows)
            {
                var accountHolder = item.ToAccountHolder();

                ValidateFundCode(accountHolder);
                ValidateFundMessage(accountHolder);

                _rowNumber++;
            }
        }

        private void Initialise()
        {
            _rowNumber = 1;
            _allFunds = _fundService.GetAllFunds();
        }

        private void ValidateFundCode(AccountHolder accountHolder)
        {
            if (!_allFunds.Any(x => x.FundCode == accountHolder.FundCode))
                throw new ImportProcessingException($"Fund code '{accountHolder.FundCode}' on row {_rowNumber} not recognised");
        }

        private void ValidateFundMessage(AccountHolder accountHolder)
        {
            var result = _accountHolderFundMessageValidator.Validate(accountHolder);

            if(!result.Success)
                throw new ImportProcessingException($"Fund message '{accountHolder.FundMessageId}' on row {_rowNumber} not recognised");
        }
    }
}
