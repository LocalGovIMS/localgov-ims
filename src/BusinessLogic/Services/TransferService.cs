using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransferService : BaseService, ITransferService
    {
        private readonly string _transferInMopCode;
        private readonly string _transferOutMopCode;

        private readonly IFundService _fundService;
        private readonly IVatService _vatService;
        private ITransactionTransferValidator _transactionTransferValidator;

        public TransferService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IFundService fundService
            , IVatService vatService
            , ITransactionTransferValidator transactionTransferValidator)
            : base(logger, unitOfWork, securityContext)
        {
            _fundService = fundService;
            _vatService = vatService;
            _transactionTransferValidator = transactionTransferValidator;

            _transferInMopCode = GetTransferInMopCode();
            _transferOutMopCode = GetTransferOutMopCode();
        }

        private string GetTransferInMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsATransferIn()).MopCode;
        }

        private string GetTransferOutMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsATransferOut()).MopCode;
        }

        public IResult Transfer(List<TransferItem> transferItems, TransferItem sourceItem)
        {
            try
            {
                var validationResult = _transactionTransferValidator.Validate(sourceItem, transferItems);

                if (validationResult.Success == false) return validationResult;

                var pspReference = GetNextReferenceId();

                CreateTransfer(sourceItem, pspReference, true);
                foreach (var transferItem in transferItems)
                {
                    CreateTransfer(transferItem, pspReference, false);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                var result = new Result();
                result.SetData(pspReference);

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("An error occured whilst processing the transfer");
            }
        }

        public IResult Transfer(List<TransferItem> transferItems, List<TransferItem> sourceItems)
        {
            try
            {
                //  If we only have one source item we want different behaviour so return the other transfer method
                if (sourceItems.Count == 1) return Transfer(transferItems, sourceItems.First());

                var validationResult = _transactionTransferValidator.Validate(sourceItems, transferItems);

                if (validationResult.Success == false) return validationResult;

                var pspReference = GetNextReferenceId();

                for (var i = 0; i < sourceItems.Count; i++)
                {
                    CreateTransfer(sourceItems[i], pspReference, true);
                    CreateTransfer(transferItems[i], pspReference, false);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                var result = new Result();
                result.SetData(pspReference);

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("An error occured whilst processing the transfer");
            }
        }

        private void CreateTransfer(TransferItem transferItem, string pspReference, bool debit)
        {
            var transaction = new ProcessedTransaction();
            var targetFund = _fundService.GetByFundCode(transferItem.FundCode);

            var vatCode = targetFund.VatCode;
            var vatRate = decimal.ToSingle(targetFund.Vat.Percentage ?? 0);

            if (targetFund.VatOverride)
            {
                if (transferItem.VatCode != targetFund.VatCode)
                {
                    var vat = _vatService.GetByVatCode(transferItem.VatCode);
                    vatCode = vat.VatCode;
                    vatRate = decimal.ToSingle(vat.Percentage ?? 0);
                }
            }

            var parentTransactionReference = transaction.TransactionReference;
            var transactionDate = DateTime.Now;
            var transferGuid = Guid.NewGuid();

            transaction.InternalReference = pspReference;
            transaction.PspReference = pspReference;
            transaction.TransactionReference = GetNextReferenceId();
            transaction.FundCode = transferItem.FundCode;
            transaction.AccountReference = transferItem.AccountReference;

            if (debit)
            {
                transaction.Amount = -transferItem.Amount;
                transaction.MopCode = _transferOutMopCode;
            }
            else
            {
                transaction.Amount = transferItem.Amount;
                transaction.MopCode = _transferInMopCode;
            }

            transaction.VatCode = vatCode;
            transaction.VatRate = vatRate;

            if (transaction.Amount != null)
            {
                transaction.VatAmount =
                    decimal.Round(
                        transaction.Amount.Value -
                        transaction.Amount.Value / (decimal)(1 + (vatRate)), 2);
            }

            transaction.Narrative = string.IsNullOrWhiteSpace(transferItem.Narrative) ? "Transfer" : transferItem.Narrative;
            transaction.TransferReference = parentTransactionReference;
            transaction.UserCode = SecurityContext.UserId;
            transaction.OfficeCode = SecurityContext.OfficeCode;
            transaction.TransactionDate = transactionDate;
            transaction.EntryDate = transactionDate;
            transaction.TransferGuid = transferGuid.ToString();

            UnitOfWork.Transactions.Add(transaction);
        }
    }
}
