using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models.Import;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class ImportProcessor : IImportProcessor
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Func<string, IImportProcessingStrategy> _importProcessingStrategyFactory;
        private readonly Func<string, IValidator<Import>> _importProcessingValidatorFactory;

        private ImportProcessorArgs _args;
        private List<string> _processingErrors = new List<string>();
        private int _numberOfSuccessfullyProcessedRows = 0;
        private IValidator<Import> _importProcessingValidator;
        private IImportProcessingStrategy _importProcessingStrategy;

        public ImportProcessor(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , Func<string, IImportProcessingStrategy> importProcessingStrategyFactory
            , Func<string, IValidator<Import>> importProcessingValidatorFactory)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _importProcessingStrategyFactory = importProcessingStrategyFactory ?? throw new ArgumentNullException("importProcessingStrategyFactory");
            _importProcessingValidatorFactory = importProcessingValidatorFactory ?? throw new ArgumentNullException("importProcessingValidatorFactory");
        }

        public IResult Process(ImportProcessorArgs args)
        {
            try
            {
                _args = args;

                InitialiseClassVariables();

                InitialiseImport();

                Process();

                End();

                return CreateResult();
            }
            catch (ImportProcessingException ex)
            {
                _log.Error(null, ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(null, ex);

                return new Result("The import is not valid");
            }
        }

        public void InitialiseClassVariables()
        {
            var importDataType = (ImportDataTypeEnum)_unitOfWork.ImportTypes.Get(_args.Import.ImportTypeId).DataType;

            _importProcessingValidator = _importProcessingValidatorFactory(importDataType.ToString());
            _importProcessingStrategy = _importProcessingStrategyFactory(importDataType.ToString());

            _processingErrors = new List<string>();
            _numberOfSuccessfullyProcessedRows = 0;
        }

        private void InitialiseImport()
        {
            var rows = _args.Import.Rows;
            _args.Import.Rows = null;

            _unitOfWork.Imports.Add(_args.Import);
            _unitOfWork.CompleteWithoutAudit(_securityContext.UserId);

            _unitOfWork.ImportRows.BulkInsert(rows);
        }

        private void Process()
        {
            if (_args.Import.HasErrors()) return;

            Validate();

            Start();

            ProcessRows();

            Save();
        }


        private void Validate()
        {
            _importProcessingValidator.Validate(_args.Import);
        }

        private void Start()
        {
            _args.Import.Start(_securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void ProcessRows()
        {
            var rowNumber = 1;

            foreach (var row in _args.Import.Rows)
            {
                ProcessRow(row, rowNumber);

                rowNumber++;
            }
        }

        private void ProcessRow(ImportRow row, int rowNumber)
        {
            try
            {
                _importProcessingStrategy.Process(new ImportProcessingStrategyArgs() { Import = _args.Import, Row = row });

                _numberOfSuccessfullyProcessedRows++;
            }
            catch (Exception ex)
            {
                _log.Error($"Row {rowNumber} - {ex.Message}", ex);
                _processingErrors.Add($"Row {rowNumber} - {ex.Message}");
            }
        }

        private void Save()
        {
            try
            {
                if (_processingErrors.Any()) 
                    throw new ImportProcessingException("Errors exist in the import data");

                _unitOfWork.Complete(_securityContext.UserId);
            }
            catch(Exception ex)
            {
                _log.Error($"Error saving import: {ex.Message}", ex);

                _processingErrors.Add("The import is not valid");

                _unitOfWork.ResetChanges();
            }
        }

        private void End()
        {
            _args.Import.Complete(_processingErrors, _securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            if (_args.Import.HasErrors())
                result.AddErrors(_args.Import.Errors().Select(x => x.Message).ToList());

            result.SetData(new ProcessResult()
            {
                NumberOfRowsImported = _numberOfSuccessfullyProcessedRows
            });

            return result;
        }
    }

    public class ImportProcessorArgs
    {
        public Import Import { get; set; }
    }
}
