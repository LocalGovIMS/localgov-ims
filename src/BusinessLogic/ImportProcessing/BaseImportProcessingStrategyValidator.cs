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
    public abstract class BaseImportProcessingStrategyValidator : IValidator<ImportProcessingStrategyValidatorArgs>
    {
        private readonly IImportTypeService _importTypeService;

        protected abstract ImportDataTypeEnum DataType { get; }

        public BaseImportProcessingStrategyValidator(IImportTypeService importTypeService)
        {
            _importTypeService = importTypeService ?? throw new ArgumentNullException("importTypeService");
        }

        public virtual void Validate(ImportProcessingStrategyValidatorArgs args)
        {
            ValidateType(args.Import);
            ValidateRowCount(args.Import, args.ImportRows);

            OnValidate(args);
        }

        protected virtual void OnValidate(ImportProcessingStrategyValidatorArgs args) 
        {
            return;
        }

        private void ValidateType(Import import)
        {
            var importType = _importTypeService.Get(import.ImportTypeId);

            if (importType is null)
                throw new ImportProcessingException("The import type specified is not recognised");

            if ((ImportDataTypeEnum)importType.DataType != DataType)
                throw new ImportProcessingException($"Expecting import type {DataType.GetDisplayName()} but received {((ImportDataTypeEnum)importType.DataType).GetDisplayName()}");
        }

        private void ValidateRowCount(Import import, List<ImportRow> importRows)
        {
            if (import.NumberOfRows != (importRows?.Count() ?? 0))
                throw new ImportProcessingException("The number of expected rows does not match the number of rows provided");
        }

        public IValidator<ImportProcessingStrategyValidatorArgs> SetNext(IValidator<ImportProcessingStrategyValidatorArgs> validator)
        {
            throw new NotImplementedException();
        }
    }
}
