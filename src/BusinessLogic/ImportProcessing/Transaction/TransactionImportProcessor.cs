using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models.Import.TransactionImport;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessor : ITransactionImportProcessor
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;
        private readonly IValidator<TransactionImport> _transactionImportValidator;

        private TransactionImportProcessorArgs _args; 
        private readonly List<string> _processingErrors = new List<string>();

        public TransactionImportProcessor(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , IRuleEngine ruleEngine
            , ITransactionService transactionService
            , IValidator<TransactionImport> transactionImportValidator)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _transactionImportValidator = transactionImportValidator ?? throw new ArgumentNullException("transactionImportValidator");
        }

        public IResult Process(TransactionImportProcessorArgs args)
        {
            try
            {
                _args = args;

                Validate();

                Prepare();

                Process();

                End();

                return CreateResult();
            }
            catch (TransactionImportProcessorException ex)
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

        private void Validate()
        {
            _transactionImportValidator.Validate(_args.TransactionImport);
        }

        private void Prepare()
        {
            _args.TransactionImport.CreatedByUserId = _securityContext.UserId;
            _args.TransactionImport.CreatedDate = DateTime.Now;

            _args.TransactionImport.Initialise(_securityContext.UserId);

            _unitOfWork.TransactionImports.Add(_args.TransactionImport) ;
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void Process()
        {
            if (_args.TransactionImport.HasErrors()) return;
            
            Start();

            ProcessRows();

            Save();
        }

        private void Start()
        {
            _args.TransactionImport.Start(_securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void ProcessRows()
        {
            var rowNumber = 1;

            foreach (var row in _args.TransactionImport.Rows)
            {
                ProcessRow(row, rowNumber);

                rowNumber++;
            }
        }

        private void ProcessRow(TransactionImportRow row, int rowNumber)
        {
            try
            {
                var processedTransactionToCreate = row.ToProcessedTransaction();

                _ruleEngine.Process(processedTransactionToCreate, _args.TransactionImport.TransactionImportTypeId);

                _args.TransactionImport.ProcessedTransactions.Add(processedTransactionToCreate);

                _transactionService.CreateProcessedTransaction(new Services.CreateProcessedTransactionArgs()
                {
                    GenerateNewReference = true,
                    SaveChanges = false,
                    ProcessedTransaction = processedTransactionToCreate
                });
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
                    throw new TransactionImportProcessorException("Errors exist in the import data");

                _unitOfWork.Complete(_securityContext.UserId);
            }
            catch(Exception ex)
            {
                _log.Error($"Error saving processed transactions: {ex.Message}", ex);

                _processingErrors.Add("The import is not valid");

                _unitOfWork.ResetChanges();
            }
        }

        private void End()
        {
            _args.TransactionImport.Complete(_processingErrors, _securityContext.UserId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            if (_args.TransactionImport.HasErrors())
                result.AddErrors(_args.TransactionImport.Errors().Select(x => x.Message).ToList());

            result.SetData(new ProcessResult()
            {
                NumberOfRowsImported = _args.TransactionImport.ProcessedTransactions.Count,
                TotalAmountImported = _args.TransactionImport.ProcessedTransactions.Sum(x => x.Amount ?? 0),
            });

            return result;
        }
    }

    public class TransactionImportProcessorArgs
    {
        public TransactionImport TransactionImport { get; set; }
    }
}
