using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public abstract class BaseImportProcessingStrategyValidator : IValidator<Import>
    {
        private readonly IImportTypeService _importTypeService;

        protected abstract ImportDataTypeEnum DataType { get; }

        public BaseImportProcessingStrategyValidator(IImportTypeService importTypeService)
        {
            _importTypeService = importTypeService ?? throw new ArgumentNullException("importTypeService");
        }

        public virtual void Validate(Import import)
        {
            ValidateType(import);
            ValidateRowCount(import);

            OnValidate(import);
        }

        protected virtual void OnValidate(Import import) 
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

        private void ValidateRowCount(Import import)
        {
            if ((import.Rows?.Count() ?? 0) != import.NumberOfRows)
                throw new ImportProcessingException("The number of expected rows does not match the number of rows provided");
        }

        public IValidator<Import> SetNext(IValidator<Import> validator)
        {
            throw new NotImplementedException();
        }
    }
}
