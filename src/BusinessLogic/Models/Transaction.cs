﻿using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessLogic.Models
{
    public class Transaction
    {
        #region Private
        private decimal TransactionLinesTotal { get { return TransactionLines.Sum(x => x.Amount).ToTwoDecimalPlaces(); } }
        private decimal PendingRefundsTotal { get { return PendingRefunds.Sum(x => x.Amount).ToTwoDecimalPlaces(); } }
        private decimal ProcessedRefundsTotal { get { return ProcessedRefunds.Sum(x => x.Amount).ToTwoDecimalPlaces(); } }
        private decimal TransfersTotal { get { return Transfers.Where(x => x.Mop.IsAJournal()).Sum(x => x.Amount).ToTwoDecimalPlaces(); } }
        #endregion

        public int? SuspenseId
        {
            get
            {
                if (TransactionLines.FirstOrDefault().SuspenseProcessedTransactions.FirstOrDefault() != null)
                {
                    return TransactionLines.FirstOrDefault().SuspenseProcessedTransactions.FirstOrDefault().SuspenseId;
                }
                else if (TransactionLines.FirstOrDefault().SuspenseProcessedTransactions1.FirstOrDefault() != null)
                {
                    return TransactionLines.FirstOrDefault().SuspenseProcessedTransactions1.FirstOrDefault().SuspenseId;
                }
                return null;
            }
        }

        [Display(Name = "PSP reference")]
        public string PspReference
        {
            get { return TransactionLines.FirstOrDefault().PspReference; }
        }

        public string ParentPspReference { get; private set; }

        [Display(Name = "Entry date")]
        public DateTime? EntryDate
        {
            get { return TransactionLines.FirstOrDefault().EntryDate; }
        }

        [Display(Name = "Last updated date")]
        public DateTime? LastUpdatedDate
        {
            get
            {
                return TransactionLines.Union(ProcessedRefunds).Union(Transfers).OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;
            }
        }

        [Display(Name = "Authorisation code")]
        public string AuthorisationCode
        {
            get { return TransactionLines.FirstOrDefault().AuthorisationCode; }
        }

        [Display(Name = "Method of payment")]
        public string MopName
        {
            get { return string.Format("{0} ({1})", TransactionLines.FirstOrDefault().Mop.MopName, TransactionLines.FirstOrDefault().Mop.MopCode); }
        }

        [Display(Name = "Address line 1")]
        public string AddressLine1
        {
            get { return TransactionLines.FirstOrDefault().CardHolderAddressLine1; }
        }

        [Display(Name = "Address line 1")]
        public string AddressLine2
        {
            get { return TransactionLines.FirstOrDefault().CardHolderAddressLine2; }
        }

        [Display(Name = "Town or City")]
        public string AddressLine3
        {
            get { return TransactionLines.FirstOrDefault().CardHolderAddressLine3; }
        }

        [Display(Name = "County")]
        public string AddressLine4
        {
            get { return TransactionLines.FirstOrDefault().CardHolderAddressLine4; }
        }

        [Display(Name = "Postcode")]
        public string PostCode
        {
            get { return TransactionLines.FirstOrDefault().CardHolderPostCode; }
        }

        [Display(Name = "Internal reference")]
        public string InternalReference
        {
            get { return TransactionLines.FirstOrDefault().InternalReference; }
        }

        [Display(Name = "Payee name")]
        public string PayeeName
        {
            get
            {
                return TransactionLines == null || !TransactionLines.Any()
                    ? string.Empty
                    : TransactionLines.FirstOrDefault().CardHolderName;
            }
        }
        
        [Display(Name = "Created by")]
        public string CreatedBy
        {
            get { return User != null ? User.DisplayName : "System / Customer"; }
        }

        [Display(Name = "User")]
        public Entities.User User
        {
            get { return TransactionLines.FirstOrDefault().User; }
        }

        public List<ProcessedTransaction> TransactionLines { get; private set; }
        public List<ProcessedTransaction> RefundableTransactionLines { get; private set; }
        public List<PendingTransaction> PendingRefunds { get; private set; }
        public List<PendingTransaction> FailedRefunds { get; private set; }
        public List<ProcessedTransaction> ProcessedRefunds { get; private set; }
        public List<ProcessedTransaction> Transfers { get; private set; }
        public List<JournalTransaction> FormattedTransfers { get; private set; }
        public List<ProcessedTransaction> CreditNotes { get; private set; }

        public Dictionary<string, List<JournalItem>> Journals { get { return GetJournals(); } }

        public decimal Total
        {
            get { return TransactionLines.Sum(x => x.Amount).ToTwoDecimalPlaces(); }
        }

        [Display(Name = "Total VAT")]
        public decimal TotalVat
        {
            get { return TransactionLines.Sum(x => x.VatAmount).ToTwoDecimalPlaces(); }
        }

        public bool RefundEnabled
        {
            get
            {
                return !PendingRefunds.Any()
                       && AmountAvailableToTransferOrRefund > 0
                       && RefundableTransactionLines.Any();
            }
        }

        public decimal AmountAvailableToTransferOrRefund
        {
            get
            {
                return (TransactionLinesTotal
                    - Math.Abs(PendingRefundsTotal)
                    - Math.Abs(ProcessedRefundsTotal)
                    - Math.Abs(TransfersTotal)).ToTwoDecimalPlaces();
            }
        }

        public bool ReceiptIssued
        {
            get
            {
                return TransactionLines.Any(x => x.ReceiptIssued == true);
            }
        }

        public Transaction(List<ProcessedTransaction> processedTransactions
            , List<PendingTransaction> pendingRefunds
            , List<PendingTransaction> failedRefunds
            , List<ProcessedTransaction> processedRefunds
            , List<ProcessedTransaction> transfers
            , string parentPspReference)
        {
            if (processedTransactions == null) throw new ArgumentNullException("processedTransactions");
            if (pendingRefunds == null) pendingRefunds = new List<PendingTransaction>();
            if (failedRefunds == null) failedRefunds = new List<PendingTransaction>();
            if (processedRefunds == null) processedRefunds = new List<ProcessedTransaction>();
            if (transfers == null) transfers = new List<ProcessedTransaction>();

            PendingRefunds = pendingRefunds;
            FailedRefunds = failedRefunds;
            ProcessedRefunds = processedRefunds;
            Transfers = transfers;
            TransactionLines = processedTransactions.Where(x => !x.IsACreditNote()).ToList();
            RefundableTransactionLines = processedTransactions.Where(x => x.IsRefundable()).ToList();
            CreditNotes = processedTransactions.Where(x => x.IsACreditNote()).ToList();
            ParentPspReference = parentPspReference;
            FormattedTransfers = FormatTransfers(transfers);
        }

        public decimal AmountAvailableToTransferOrRefundForTransactionLine(string transactionReference)
        {
            var transaction = TransactionLines.FirstOrDefault(x => x.TransactionReference == transactionReference);

            if (transaction == null) return 0;

            var totalRefunded = ProcessedRefunds
                .Where(x => x.AccountReference == transaction.AccountReference && x.FundCode == transaction.FundCode)
                .Sum(x => x.Amount) ?? 0;

            var totalPendingRefunds = PendingRefunds
                .Sum(x => x.Amount) ?? 0;

            var totalTransferred = Transfers
                .Where(x => x.TransferReference == transactionReference && x.Mop.IsAJournal())
                .Sum(x => x.Amount) ?? 0;

            return ((transaction.Amount ?? 0)
                - Math.Abs(totalRefunded)
                - Math.Abs(totalPendingRefunds)
                - Math.Abs(totalTransferred)).ToTwoDecimalPlaces();
        }

        public bool CanTransferBeUndone(string transactionReference)
        {

            return Transfers.Where(x => x.TransactionReference == transactionReference)
                .All(x => x.TransferRollbackGuid == null);
        }

        [Display(Name = "Address")]
        public string FormattedAddress
        {
            get
            {
                var transaction = TransactionLines.FirstOrDefault();

                return $@"{transaction.CardHolderAddressLine1?.Trim()}, {transaction.CardHolderAddressLine2?.Trim()}, {transaction.CardHolderAddressLine3?.Trim()}, {transaction.CardHolderAddressLine4?.Trim()}, {transaction.CardHolderPostCode?.Trim()}"
                    .Replace(", , , , ,", "")
                    .Replace(", , , ,", "")
                    .Replace(", , ,", ",")
                    .Replace(", ,", ",")
                    .Trim(',', ' ');
            }
        }

        private Dictionary<string, List<JournalItem>> GetJournals()
        {
            var journals = new Dictionary<string, List<JournalItem>>();

            var endpoints = new List<ProcessedTransaction>();

            // Get the end-point journals
            foreach (var transfer in Transfers)
            {
                if (Transfers.Any(x => x.TransferReference == transfer.TransactionReference))
                {
                    continue;
                }

                // transfer isn't a parent to any other transfer, so it's an end point.
                endpoints.Add(transfer);
            }

            foreach (var line in TransactionLines)
            {
                var exists = true;
                var audit = new List<JournalItem>();
                var level = 1;
                var transactionReference = line.TransactionReference;

                while (exists)
                {
                    if (Transfers.Any(x => x.TransferReference == transactionReference))
                    {
                        foreach (var transfer in Transfers.Where(x => x.TransferReference == transactionReference).OrderBy(x => x.EntryDate))
                        {
                            audit.Add(new JournalItem() { Level = level, Journal = transfer });
                            level++;
                        }
                    }
                }

                if (audit.Any())
                {
                    journals.Add(line.TransactionReference, audit);
                }
            }

            return null;
        }

        private List<JournalTransaction> FormatTransfers(List<ProcessedTransaction> transfers)
        {
            var formattedTransfers = new List<JournalTransaction>();

            var pspReferences = transfers.Select(x => x.PspReference).Distinct();

            foreach (var pspReference in pspReferences)
            {
                var credit = transfers.OrderBy(x => x.Id)
                    .FirstOrDefault(x => x.PspReference == pspReference && x.IsACredit());

                if (credit == null) throw new NullReferenceException(string.Format("Unable to locate credit record for PSP: {0}", pspReference));

                var debit = transfers.OrderBy(x => x.Id)
                    .FirstOrDefault(x => x.PspReference == pspReference && x.IsADebit());

                if (credit == null) throw new NullReferenceException(string.Format("Unable to locate debit record for PSP: {0}", pspReference));

                formattedTransfers.Add(new JournalTransaction(credit, debit, null));
            }

            return formattedTransfers;
        }
    }

    public class JournalItem
    {
        public int Level { get; set; }

        public ProcessedTransaction Journal { get; set; }
    }
}
