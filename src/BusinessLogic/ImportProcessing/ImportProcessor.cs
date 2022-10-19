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
using System.Configuration;
using System.Data;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class ImportProcessor : IImportProcessor
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Func<string, IImportInitialisationStrategy> _importInitialisationStrategyFactory;
        private readonly Func<string, IImportProcessingStrategy> _importProcessingStrategyFactory;
        private readonly Func<string, IValidator<ImportProcessingStrategyValidatorArgs>> _importProcessingStrategyValidatorFactory;

        private ImportProcessorArgs _args;
        private List<string> _processingErrors = new List<string>();
        private IImportInitialisationStrategy _importInitialisationStrategy;
        private IValidator<ImportProcessingStrategyValidatorArgs> _importProcessingStrategyValidator;
        private IImportProcessingStrategy _importProcessingStrategy;

        public ImportProcessor(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , Func<string, IImportInitialisationStrategy> importInitialisationStrategyFactory
            , Func<string, IImportProcessingStrategy> importProcessingStrategyFactory
            , Func<string, IValidator<ImportProcessingStrategyValidatorArgs>> importProcessingStrategyValidatorFactory)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _importInitialisationStrategyFactory = importInitialisationStrategyFactory ?? throw new ArgumentNullException("importInitialisationStrategyFactory");
            _importProcessingStrategyFactory = importProcessingStrategyFactory ?? throw new ArgumentNullException("importProcessingStrategyFactory");
            _importProcessingStrategyValidatorFactory = importProcessingStrategyValidatorFactory ?? throw new ArgumentNullException("importProcessingValidatorFactory");
        }

        public IResult Process(ImportProcessorArgs args)
        {
            try
            {
                _args = args;

                InitialiseImportProcessor();

                InitialiseImport();

                SaveRawData();

                Validate();

                Process();

                return CreateResult();
            }
            catch (ImportProcessingException ex)
            {
                _unitOfWork.ResetChanges();

                _processingErrors.Add(ex.ToString());

                _log.Error(null, ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.ResetChanges();

                _processingErrors.Add(ex.ToString());

                _log.Error(null, ex);

                return new Result("The import is not valid");
            }
            finally
            {
                _args.Import.Complete(_processingErrors, _securityContext.UserId);
                _unitOfWork.Complete(_securityContext.UserId);
            }
        }

        public void InitialiseImportProcessor()
        {
            try
            {
                var importDataType = (ImportDataTypeEnum)_unitOfWork.ImportTypes.Get(_args.Import.ImportTypeId).DataType;

                _importInitialisationStrategy = _importInitialisationStrategyFactory(importDataType.ToString());
                _importProcessingStrategy = _importProcessingStrategyFactory(importDataType.ToString());
                _importProcessingStrategyValidator = _importProcessingStrategyValidatorFactory(importDataType.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(null, ex);

                throw new ImportProcessingException("Unable to initialise the Import. Please check the ImportTypeId is set correctly.");
            }
        }

        public void InitialiseImport()
        {
            _args.Import.Initialise(_securityContext.UserId);

            if(!(_importInitialisationStrategy is null))
            {
                _importInitialisationStrategy.Initialise(new ImportInitialisationStrategyArgs { Import = _args.Import, ImportRows = _args.ImportRows });
            }
        }

        private void SaveRawData()
        {
            try
            {
                _unitOfWork.Imports.Add(_args.Import);
                _unitOfWork.Complete(_securityContext.UserId);

                _args.ImportRows.ForEach(x => x.ImportId = _args.Import.Id);

                foreach (var batch in _args.ImportRows.Batch(Convert.ToInt32(ConfigurationManager.AppSettings["Import.BatchSize"])))
                {
                    _unitOfWork.ImportRows.BulkInsert(batch);
                }
            }
            catch (Exception ex)
            {
                _log.Error(null, ex);

                throw new ImportProcessingException($"Unable to save the Import and Import Row data. {ex.Message}");
            }
        }

        private void Validate()
        {
            if (_args.Import.HasErrors()) return;

            _importProcessingStrategyValidator.Validate(new ImportProcessingStrategyValidatorArgs() { Import = _args.Import, ImportRows = _args.ImportRows });
        }

        private void Process()
        {
            if (_args.Import.HasErrors()) return;

            Start();

            ProcessRows();

            Finish();
        }

        private void Start()
        {
            _args.Import.Start(_securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void ProcessRows()
        {
            _importProcessingStrategy.Process(new ImportProcessingStrategyArgs() { Import = _args.Import, ImportRows = _args.ImportRows });
        }

        private void Finish()
        {
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            if (_args.Import.HasErrors())
                result.AddErrors(_args.Import.Errors().Select(x => x.Message).ToList());

            result.SetData(new ProcessResult()
            {
                NumberOfRowsImported = _importProcessingStrategy.NumberOfSuccessfullyProcessedRows
            });

            return result;
        }
    }

    public class ImportProcessorArgs
    {
        public Import Import { get; set; }
        public List<ImportRow> ImportRows { get; set; }
    }
}
