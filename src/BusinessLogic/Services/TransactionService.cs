using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Models;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Transactions;
using log4net;
using Omu.ValueInjecter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly ITransactionVatStrategy _vatStrategy;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public TransactionService(ILog logger
            , IUnitOfWork unitOfWork
            , ITransactionVatStrategy vatStrategy
            , IEmailService emailService
            , ISecurityContext securityContext
            , IUserService userService)
            : base(logger, unitOfWork, securityContext)
        {
            _vatStrategy = vatStrategy;
            _emailService = emailService;
            _userService = userService;
        }

        public List<PendingTransaction> GetPendingTransactionsByInternalReference(string internalReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionDetails)) return new List<PendingTransaction>();

            try
            {
                return UnitOfWork.PendingTransactions.GetByInternalReference(internalReference).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<PendingTransaction>();
            }
        }

        public ProcessedTransaction GetTransactionByAppReference(string appReference)
        {
            try
            {
                return UnitOfWork.Transactions.GetByAppReference(appReference);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public Response AuthorisePendingTransactionByInternalReference(string internalReference, string pspReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionCreate)) return null;

            var response = new Response();
            try
            {
                if (TransactionAlreadyProcessed(internalReference))
                {
                    response.Success = true;
                    return response;
                }

                var pendingTransactions = UnitOfWork.PendingTransactions.GetByInternalReference(internalReference);

                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    pendingTransaction.Processed = true;
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Successful;
                    var transaction = ConvertPendingTransactionToTransaction(pendingTransaction);
                    transaction.PspReference = pspReference;
                    transaction.TransactionDate = DateTime.Now;
                    transactions.Add(transaction);
                }

                UnitOfWork.Transactions.AddRange(transactions);
                UnitOfWork.Complete(SecurityContext.UserId);

                response.Success = true;
                return response;
            }
            catch (DbUpdateException e)
            {
                Logger.Warn(null, e);
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
        }

        public Response SaveChequesToProcessed(List<ProcessedTransaction> transactions)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionSave)) return null;

            var response = new Response();
            try
            {
                var internalReference = GetNextReferenceId();
                foreach (var t in transactions)
                {
                    t.TransactionReference = GetNextReferenceId();
                    t.InternalReference = internalReference;
                    t.PspReference = internalReference;
                    t.EntryDate = DateTime.Now;
                    t.TransactionDate = DateTime.Now;
                    _vatStrategy.AddVatToTransaction(t);
                }

                UnitOfWork.Transactions.AddRange(transactions);
                UnitOfWork.Complete(SecurityContext.UserId);

                response.Success = true;
                response.PaymentId = internalReference;
                return response;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
        }

        private bool TransactionAlreadyProcessed(string internalReference)
        {
            var processedTransactions = UnitOfWork.Transactions.GetByInternalReference(internalReference).ToList();
            return processedTransactions.Any();
        }

        public IResult AuthoriseTransactionByNotification(TransactionNotification notification)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionCreate)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var processedTransactions = UnitOfWork.Transactions.GetByInternalReference(notification.MerchantReference).ToList();

                if (processedTransactions.Any())
                {
                    if (processedTransactions.All(x => x.PspReference != notification.PspReference))
                    {
                        HandleNotificationForDuplicateTransactions(notification, processedTransactions);
                    }
                    else if (processedTransactions.Any(x => x.PspReference == notification.PspReference))
                    {
                        HandleNotificationForExistingTransactions(notification, processedTransactions);
                    }
                }
                else
                {
                    HandleNotificationForNewTransactions(notification, processedTransactions);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (DbUpdateException e)
            {
                UnitOfWork.ResetChanges();
                Logger.Warn(null, e);
                return new Result("Unable to create a authorise Transaction by notification");
            }
            catch (Exception e)
            {
                UnitOfWork.ResetChanges();
                Logger.Error(null, e);
                return new Result("Unable to create a authorise Transaction by notification");
            }
        }

        /// <summary>
        /// Handle notifications which create new processed transactions from a pending transaction.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="processedTransactions"></param>
        private void HandleNotificationForNewTransactions(TransactionNotification notification, List<ProcessedTransaction> processedTransactions)
        {
            if (!processedTransactions.Any())
            {
                var pendingTransactions =
                    UnitOfWork.PendingTransactions.GetByInternalReference(notification.MerchantReference);

                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    pendingTransaction.Processed = true;
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Successful;
                    var transaction = ConvertPendingTransactionToTransaction(pendingTransaction);
                    transaction.PspReference = notification.PspReference;
                    transaction.AuthorisationCode = notification.Reason;
                    transaction.TransactionDate = notification.EventDate;
                    transaction.EntryDate = DateTime.Now;
                    transactions.Add(transaction);
                }

                UnitOfWork.Transactions.AddRange(transactions);
            }
        }

        /// <summary>
        /// Handle notifications which provide additional information for existing processed transactions, for example
        /// the authorisation date to make sure payment provider balances and the Authorisation Code.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="processedTransactions"></param>
        private static void HandleNotificationForExistingTransactions(TransactionNotification notification,
            List<ProcessedTransaction> processedTransactions)
        {
            if (processedTransactions.Any(x => x.PspReference == notification.PspReference))
            {
                foreach (var processedTransaction in processedTransactions.Where(x =>
                    x.PspReference == notification.PspReference))
                {
                    processedTransaction.AuthorisationCode = notification.Reason;
                    processedTransaction.TransactionDate = notification.EventDate;
                }
            }
        }

        /// <summary>
        /// Handle notifications for duplicate transactions - for example if we recieve two authorisation
        /// events for the same payment and the customer has paid twice - this allows us to insert into the
        /// processed table so it can be refunded later.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="processedTransactions"></param>
        private void HandleNotificationForDuplicateTransactions(TransactionNotification notification,
            List<ProcessedTransaction> processedTransactions)
        {
            if (processedTransactions.All(x => x.PspReference != notification.PspReference))
            {
                var pendingTransactions =
                    UnitOfWork.PendingTransactions.GetByInternalReference(notification.MerchantReference);

                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    pendingTransaction.Processed = true;
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Successful;
                    var transaction = ConvertPendingTransactionToTransaction(pendingTransaction);
                    transaction.TransactionReference = GetNextReferenceId();
                    transaction.PspReference = notification.PspReference;
                    transaction.AuthorisationCode = notification.Reason;
                    transaction.TransactionDate = notification.EventDate;
                    transaction.EntryDate = DateTime.Now;
                    transactions.Add(transaction);
                }

                _emailService.SendDuplicateTransactionEmail(transactions);

                UnitOfWork.Transactions.AddRange(transactions);
            }
        }

        // TODO: Move to refund service.
        public IResult AuthoriseRefundByNotification(string internalReference, string pspReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.RefundsAuthorise)) return new Result("You do not have permission to perform the requested action");

            try
            {
                if (string.IsNullOrWhiteSpace(internalReference)) throw new ArgumentException("Internal reference cannot be null or whitespace");

                var pendingTransactions = UnitOfWork.PendingTransactions.Find(x => x.RefundReference == internalReference && x.Processed != true).ToList();

                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    pendingTransaction.Processed = true;
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Successful;
                    var transaction = ConvertPendingTransactionToTransaction(pendingTransaction);
                    transaction.PspReference = pspReference;
                    transaction.TransactionDate = transaction.EntryDate;
                    transaction.EntryDate = DateTime.Now;
                    transactions.Add(transaction);
                }

                UnitOfWork.Transactions.AddRange(transactions);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a authorise Refund by notification");
            }
        }

        private ProcessedTransaction ConvertPendingTransactionToTransaction(PendingTransaction pendingTransaction)
        {
            return Mapper.Map<ProcessedTransaction>(pendingTransaction);
        }

        public Response SavePendingTransactions(IEnumerable<PendingTransaction> pendingTransactions, string source)
        {
            if ((!SecurityContext.IsInRole(Security.Role.TransactionCreate))
                && (!SecurityContext.IsInRole(Security.Role.TransactionRefund))
                && (!SecurityContext.IsInRole(Security.Role.Payments))) return null;

            try
            {
                var referenceId = GetNextReferenceId();
                pendingTransactions = pendingTransactions.ToList();

                PopulatePendingTransactionProperties(pendingTransactions, source, referenceId);
                UnitOfWork.PendingTransactions.AddRange(pendingTransactions);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Response()
                {
                    Success = true,
                    ErrorMessage = string.Empty,
                    PaymentId = referenceId
                };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Response()
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }

        private void PopulatePendingTransactionProperties(
            IEnumerable<PendingTransaction> pendingTransactions,
            string source,
            string referenceId)
        {
            var lookupCache = new Hashtable();

            var count = 1;
            foreach (var pendingTransaction in pendingTransactions.ToList())
            {
                var vat = GetVatForTransaction(pendingTransaction);

                pendingTransaction.TransactionReference = referenceId + "_" + count;
                pendingTransaction.InternalReference = referenceId;
                pendingTransaction.StatusId = (int)Enums.TransactionStatus.Pending;
                pendingTransaction.EntryDate = DateTime.Now;
                pendingTransaction.OfficeCode = SecurityContext.OfficeCode;
                pendingTransaction.VatCode = vat.VatCode;
                pendingTransaction.VatRate = decimal.ToSingle(vat.Percentage ?? 0);

                if (pendingTransaction.Amount != null)
                {
                    pendingTransaction.VatAmount =
                        decimal.Round(
                            pendingTransaction.Amount.Value -
                            pendingTransaction.Amount.Value / (1 + (vat.Percentage ?? 0)), 2);
                }

                count++;
            }

            Vat GetVatForTransaction(PendingTransaction pendingTransaction)
            {
                if (string.IsNullOrWhiteSpace(pendingTransaction.VatCode))
                {
                    if (lookupCache.ContainsKey($"VatForFundCode:{pendingTransaction.FundCode}"))
                        return lookupCache[$"VatForFundCode:{pendingTransaction.FundCode}"] as Vat;

                    var vatForFundCodes = UnitOfWork.Funds.GetByFundCode(pendingTransaction.FundCode).Vat;
                    lookupCache.Add($"VatForFundCode:{pendingTransaction.FundCode}", vatForFundCodes);
                    return vatForFundCodes;
                }
                else
                {
                    if (lookupCache.ContainsKey($"Vat:{pendingTransaction.VatCode}"))
                        return lookupCache[$"Vat:{pendingTransaction.VatCode}"] as Vat;

                    var vat = UnitOfWork.Vats.GetVatByVatCode(pendingTransaction.VatCode);
                    lookupCache.Add($"Vat:{pendingTransaction.VatCode}", vat);
                    return vat;
                }
            }
        }

        public Response SavePendingTransaction(PendingTransaction pendingTransaction, string source)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionSave)) return null;

            var pendingTransactions = new List<PendingTransaction>
            {
                pendingTransaction
            };

            return SavePendingTransactions(pendingTransactions, source);
        }

        public IResult SaveNotification(TransactionNotification notification)
        {
            if (!SecurityContext.IsInRole(Security.Role.NotificationCreate)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.TransactionNotifications.Add(notification);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create a Transaction Notification");
            }
        }

        public IResult FailPendingTransaction(string reference, string pspReference, string authResult)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionEdit)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var pendingTransactions = UnitOfWork.PendingTransactions.GetByInternalReference(reference);

                foreach (var pendingTransaction in pendingTransactions)
                {
                    pendingTransaction.AuthorisationCode = authResult;
                    pendingTransaction.Narrative = pspReference;
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Failed;
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to fail a Pending Transaction");
            }
        }

        public IResult SuspendPendingTransaction(string reference, string pspReference, string authResult)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionEdit)) return new Result("You do not have permission to perform the requested action");

            try
            {
                if (TransactionAlreadyProcessed(reference)) return new Result("The transaction has already been processed");

                var pendingTransactions = UnitOfWork.PendingTransactions.GetByInternalReference(reference);
                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    var transaction = ConvertPendingTransactionToTransaction(pendingTransaction);
                    transaction.PspReference = pspReference;
                    transaction.TransactionDate = DateTime.Now;
                    transactions.Add(transaction);
                }

                UnitOfWork.Transactions.AddRange(transactions);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to suspend a Pending Transaction");
            }
        }

        public List<ProcessedTransaction> GetTransactionsByInternalReference(string internalReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionList)) return new List<ProcessedTransaction>();

            try
            {
                return UnitOfWork.Transactions.Find(x => x.InternalReference == internalReference).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<ProcessedTransaction>();
            }
        }

        public ProcessedTransaction GetTransaction(string transactionReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionDetails)) return null;

            try
            {
                return UnitOfWork.Transactions.GetByTransactionReference(transactionReference);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<PendingTransaction> GetPendingRefunds(string transactionReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.RefundsList)) return new List<PendingTransaction>();

            try
            {
                if (transactionReference == null)
                {
                    throw new ArgumentNullException("transactionReference");
                }
                return UnitOfWork.PendingTransactions
                    .GetPendingRefunds(transactionReference).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<PendingTransaction>();
            }
        }

        public List<PendingTransaction> GetFailedRefunds(string transactionReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.RefundsList)) return new List<PendingTransaction>();

            try
            {
                if (transactionReference == null)
                {
                    throw new ArgumentNullException("transactionReference");
                }
                return UnitOfWork.PendingTransactions
                    .GetFailedRefunds(transactionReference).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<PendingTransaction>();
            }
        }

        public List<ProcessedTransaction> GetProcessedRefunds(string transactionReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.RefundsList)) return new List<ProcessedTransaction>();

            try
            {
                if (transactionReference == null)
                {
                    throw new ArgumentNullException("transactionReference");
                }
                return UnitOfWork.Transactions.GetProcessedRefunds(transactionReference).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<ProcessedTransaction>();
            }
        }

        public List<ProcessedTransaction> GetTransfers(string transferGuid)
        {
            try
            {
                if (string.IsNullOrEmpty(transferGuid))
                {
                    throw new ArgumentNullException("transferGuid");
                }

                return UnitOfWork.Transactions.GetTransfers(transferGuid).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<ProcessedTransaction>();
            }
        }

        public Transaction GetTransactionByPspReference(string pspReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionList)) return null;

            try
            {
                if (pspReference == null)
                {
                    throw new ArgumentNullException("pspReference");
                }

                var transactions = UnitOfWork.Transactions.GetByPspReference(pspReference).ToList();

                if (transactions.IsATransferTransaction())
                {
                    Logger.WarnFormat("Tried to load just a transfer trasaction for PSP Reference: '{0}'", pspReference);
                    return null;
                }

                var processedTransactions = transactions.GetProcessedTransactions();
                var internalReference = processedTransactions.First().InternalReference;
                var pendingRefunds = GetPendingRefunds(internalReference);
                var failedRefunds = GetFailedRefunds(internalReference);
                var processedRefunds = GetProcessedRefunds(internalReference);
                var transfers = UnitOfWork.Transactions.GetJournalsForTransactions(processedTransactions).ToList();

                var parentPspReference = string.Empty;
                var transferReference = transactions.FirstOrDefault().TransferReference;
                if (!string.IsNullOrEmpty(transferReference))
                {
                    var parentTransaction = UnitOfWork.Transactions.GetByTransactionReference(transactions.FirstOrDefault().TransferReference);
                    if (parentTransaction != null) parentPspReference = parentTransaction.PspReference;
                }

                var transaction = new Transaction(processedTransactions, pendingRefunds, failedRefunds, processedRefunds, transfers, parentPspReference);

                return transaction;
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public SearchResult<ProcessedTransaction> SearchTransactions(SearchCriteria criteria)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionList)) return null;

            try
            {
                var result = UnitOfWork.Transactions.Search(criteria.TrimStringProperties(), out int resultCount);

                return new SearchResult<ProcessedTransaction>()
                {
                    Count = resultCount,
                    Items = result,
                    Page = criteria.Page,
                    PageSize = criteria.PageSize
                };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public List<TransactionNotification> GetUnprocessedNotifications()
        {
            // if (!SecurityContext.IsInRole("Notification.List")) return null; todo: check permission for this

            try
            {
                return UnitOfWork.TransactionNotifications.Find(x => x.Processed == false).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new List<TransactionNotification>();
            }
        }

        public IResult MarkNotificationAsProcessed(int transactionId)
        {
            if (!SecurityContext.IsInRole(Security.Role.NotificationEdit)
                && !SecurityContext.IsInRole(Security.Role.SystemAdmin)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var notification = UnitOfWork.TransactionNotifications.SingleOrDefault(x => x.Id == transactionId);
                notification.Processed = true;

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to mark Notification as processed");
            }
        }

        public decimal GetAmountForPendingTransactionByReference(string reference)
        {
            var processedTransactions = GetTransactionsByInternalReference(reference);
            if (processedTransactions.Any()) return decimal.Zero;

            var pendingTransactions = GetPendingTransactionsByInternalReference(reference);

            return pendingTransactions.Sum(x => x.Amount ?? 0);
        }

        public IResult MarkRefundsAsFailed(string refundReference, string reason)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refundReference)) throw new ArgumentException("Refund reference cannot be null or whitespace");

                Logger.DebugFormat("Refund Reference: {0}", refundReference);
                Logger.DebugFormat("Reason: {0}", reason);

                Logger.Debug("Retrieving refunds to process");
                var pendingRefunds = UnitOfWork.PendingTransactions.GetPendingRefunds(refundReference);

                Logger.DebugFormat("Found {0} refunds to process", pendingRefunds == null ? 0 : pendingRefunds.Count());

                foreach (var pendingRefund in pendingRefunds)
                {
                    Logger.DebugFormat("Marking refund (ID:{0}, Refund Reference:{1}), as processed", pendingRefund.Id, pendingRefund.RefundReference);
                    pendingRefund.Processed = true;
                    pendingRefund.Narrative = reason;
                    pendingRefund.StatusId = (int)Enums.TransactionStatus.Failed;
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to mark Refund as processed");
            }
        }

        public IResult ReceiptIssued(string pspReference)
        {
            try
            {
                var transcation = GetTransactionByPspReference(pspReference);

                foreach (var item in transcation.TransactionLines)
                {
                    item.ReceiptIssued = true;
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result($"Unable to mark the transaction as having had a receipt issued. PSP reference: {pspReference}");
            }
        }

        public IResult CreateProcessedTransaction(ProcessedTransaction processedTransaction)
        {
            return CreateProcessedTransaction(processedTransaction, true);
        }

        public IResult CreateProcessedTransaction(ProcessedTransaction processedTransaction, bool saveChagnes)
        {
            return CreateProcessedTransaction(new CreateProcessedTransactionArgs() { ProcessedTransaction = processedTransaction, SaveChanges = saveChagnes });
        }

        public IResult CreateProcessedTransaction(CreateProcessedTransactionArgs args)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionCreate)) return null;

            try
            {
                if (args.GenerateNewReference)
                {
                    args.ProcessedTransaction.TransactionReference = GetNextReferenceId();
                    args.ProcessedTransaction.InternalReference = args.ProcessedTransaction.TransactionReference;
                }

                UnitOfWork.Transactions.Add(args.ProcessedTransaction);

                if (args.SaveChanges)
                {
                    UnitOfWork.Complete(SecurityContext.UserId);
                }

                return new Result() { Data = args.ProcessedTransaction };
            }
            catch (DbUpdateException ex)
            {
                Logger.Warn(null, ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(null, ex);

                return new Result(ex.Message);
            }
        }
    }

    public class CreateProcessedTransactionArgs
    {
        public ProcessedTransaction ProcessedTransaction { get; set; }
        public bool SaveChanges { get; set; } = true;
        public bool GenerateNewReference { get; set; } = true;
    }
}