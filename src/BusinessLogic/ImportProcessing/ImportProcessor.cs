using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class ImportProcessor : IImportProcessor
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;
        private readonly IProcessedTransactionModelBuilder _processedTransactionModelBuilder;

        private Import _import;
        private decimal _totalAmountImported = 0M;
        private List<string> _errors = new List<string>();

        public ImportProcessor(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , IRuleEngine ruleEngine
            , ITransactionService transactionService
            , IProcessedTransactionModelBuilder processedTransactionModelBuilder)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _processedTransactionModelBuilder = processedTransactionModelBuilder ?? throw new ArgumentNullException("processedTransactionModelBuilder");
        }

        public IResult Process(ImportProcessorArgs args)
        {
            LoadImport(args.BatchReference);

            ProcessImport();

            Save();

            return CreateResult();
        }

        private void LoadImport(string batchReference)
        {
            _import = _unitOfWork.Imports.GetByBatchReference(batchReference);
        }

        private void ProcessImport()
        {
            var rowNumber = 1;

            foreach (var row in _import.Rows)
            {
                if (_errors.Count >= 10) return;

                ProcessRow(row, rowNumber);

                rowNumber++;
            }
        }
        private void ProcessRow(ImportRow row, int rowNumber)
        {
            try
            {
                var processedTransactionToCreate = BuildProcessedTransaction(row.RowData);

                _ruleEngine.Process(processedTransactionToCreate);

                var result = _transactionService.CreateProcessedTransaction(processedTransactionToCreate, false);

                if (result.Success)
                {
                    _totalAmountImported += processedTransactionToCreate.Amount ?? 0;
                }
                else
                {
                    _errors.Add($"Row {rowNumber} - {result.Error}");
                }
            }
            catch (Exception ex)
            {
                _errors.Add($"Row {rowNumber} - {ex.Message}");
            }
        }

        private ProcessedTransaction BuildProcessedTransaction(string rowData)
        {
            var processedTransactionModel = _processedTransactionModelBuilder.BuildFromCsvRow(rowData, _import.BatchReference);

            return processedTransactionModel.GetProcessedTransaction();
        }

        private void Save()
        {
            if (_errors.Any()) return;

            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            if (_errors.Any())
                result.AddErrors(_errors);

            result.SetData(new ProcessResult()
            {
                Import = _import,
                NumberOfRowsImported = _import.Rows.Count,
                TotalAmountImported = _totalAmountImported,
            });

            return result;
        }
    }

    public class ImportProcessorArgs
    {
        public string BatchReference { get; set; }
    }
}
