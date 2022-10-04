using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Import;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class FileImportProcessor : IFileImportProcessor
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;
        private readonly ISuspenseService _suspenseService;
        private readonly IProcessedTransactionModelBuilder _processedTransactionModelBuilder;

        private FileImport _fileImport;
        private int _rowNumber = 0;
        private List<string> _errors = new List<string>();

        public FileImportProcessor(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , IRuleEngine ruleEngine
            , ITransactionService transactionService
            , ISuspenseService suspenseService
            , IProcessedTransactionModelBuilder processedTransactionModelBuilder)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _suspenseService = suspenseService ?? throw new ArgumentNullException("suspenseService");
            _processedTransactionModelBuilder = processedTransactionModelBuilder ?? throw new ArgumentNullException("processedTransactionModelBuilder");
        }

        public IResult Process(FileImportProcessorArgs args)
        {
            try
            {
                LoadImport(args.FileImportId);

                Start();

                ProcessImport();

                Save();

                return CreateResult();
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);

                _log.Error(null, ex);

                return new Result("The import is not valid");
            }
            finally
            {
                _fileImport.Import.Complete(_errors, _securityContext.UserId);
                _unitOfWork.Complete(_securityContext.UserId);
            }
        }

        private void LoadImport(int fileImportId)
        {
            _fileImport = _unitOfWork.FileImports.GetByImportId(fileImportId);
        }

        private void Start()
        {
            _fileImport.Import.Start(_securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void ProcessImport()
        {
            _rowNumber = 1;

            foreach (var row in _fileImport.Import.Rows)
            {
                if (_errors.Count >= 10) return;

                ProcessRow(row);

                _rowNumber++;
            }
        }
        private void ProcessRow(ImportRow row)
        {
            try
            {
                var transaction = BuildProcessedTransaction(row.Data);

                _ruleEngine.Process(transaction);

                if (transaction.IsCreatable())
                {
                    CreateTransaction(transaction);
                }
                else
                {
                    CreateSuspense(transaction);
                }
            }
            catch (Exception ex)
            {
                _errors.Add($"Row {_rowNumber} - {ex.Message}");
            }
        }

        private ProcessedTransaction BuildProcessedTransaction(string rowData)
        {
            var processedTransactionModel = _processedTransactionModelBuilder.BuildFromCsvRow(rowData, _fileImport.ImportId);

            return processedTransactionModel.GetProcessedTransaction();
        }

        private void CreateTransaction(ProcessedTransaction transaction)
        {
            var result = _transactionService.CreateProcessedTransaction(new Services.CreateProcessedTransactionArgs()
            {
                GenerateNewReference = true,
                SaveChanges = false,
                ProcessedTransaction = transaction
            });

            if (!result.Success)
                _errors.Add($"Row {_rowNumber} - {result.Error}");
        }

        private void CreateSuspense(ProcessedTransaction transaction)
        {
            var result = _suspenseService.Create(new Services.CreateSuspenseArgs()
            {
                SaveChanges = false,
                Suspense = transaction.ToSuspense()
            });

            if (!result.Success)
                _errors.Add($"Row {_rowNumber} - {result.Error}");
        }

        private void Save()
        {
            if (_errors.Any())
            {
                _unitOfWork.ResetChanges();
                return;
            }
            
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            if (_errors.Any())
                result.AddErrors(_errors);

            result.SetData(new ProcessResult()
            {
                FileImport = _fileImport,
                NumberOfRowsImported = _errors.Any() ? 0 : _fileImport.Import.NumberOfRows
            });

            return result;
        }
    }

    public class FileImportProcessorArgs
    {
        public int FileImportId { get; set; }
    }
}
