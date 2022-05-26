using BusinessLogic.Classes;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPaymentIntegrationClient = BusinessLogic.Clients.PaymentIntegrationClient.IClient;

namespace BusinessLogic.Services
{
    public class RefundService : BaseService, IRefundService
    {
        private readonly ITransactionService _transactionService;
        private readonly IPaymentIntegrationClient _paymentIntegrationClient;

        public RefundService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionService transactionService
            , IPaymentIntegrationClient paymentIntegrationClient)
            : base(logger, unitOfWork, securityContext)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _paymentIntegrationClient = paymentIntegrationClient ?? throw new ArgumentNullException("paymentIntegrationClient");
        }

        // HIGH: We should be checking for refund permission here
        public RefundStatus RefundTransaction(RefundRequest refundRequest, string reason)
        {
            return RefundTransactions(new List<RefundRequest> { refundRequest }, reason);
        }

        public RefundStatus RefundTransactions(List<RefundRequest> refundRequests, string reason)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionRefund)) return null;

            var internalReference = string.Empty;

            try
            {
                decimal totalRefundAmount = 0;
                var pspReference = string.Empty;
                var transactionDate = new DateTime();
                var mopCode = string.Empty;

                var validRefundTransactions = new List<PendingTransaction>();

                if (refundRequests.Sum(x => x.RefundAmount) <= 0)
                {
                    return RefundStatus.ErrorStatus("Refund amount must be greater than 0");
                }

                // TODO: This will be string.Empty - so will always pass
                if (HasRefundsPending(internalReference))
                {
                    return RefundStatus.ErrorStatus("Payment has pending refunds, please wait for them to complete and try again");
                }

                if (string.IsNullOrEmpty(reason))
                {
                    return RefundStatus.ErrorStatus("You must provide a reason for the refund");
                }

                foreach (var refundRequest in refundRequests)
                {
                    if (refundRequest.RefundAmount <= 0) continue;

                    var transaction = _transactionService.GetTransaction(refundRequest.TransactionReference);
                    var transctionHeader = _transactionService.GetTransactionByPspReference(transaction.PspReference);
                    internalReference = transaction.InternalReference;
                    transactionDate = transaction.TransactionDate ?? new DateTime();
                    mopCode = transaction.MopCode;

                    var remainingAmount = transctionHeader.AmountAvailableToTransferOrRefundForTransactionLine(refundRequest.TransactionReference);
                    if (refundRequest.RefundAmount > remainingAmount)
                    {
                        return RefundStatus.ErrorStatus("Refund amount is greater than remaining transaction amount");
                    }

                    if (pspReference != string.Empty && pspReference != transaction.PspReference)
                    {
                        return RefundStatus.ErrorStatus("Transaction contains refunds for multiple PSP References");
                    }

                    if (!transaction.IsRefundable())
                    {
                        return RefundStatus.ErrorStatus("You can't refund a payment that hasn't been processed via the payment provider");
                    }

                    if (refundRequest.RefundAmount < transaction.Amount && !SecurityContext.IsInRole(Security.Role.TransactionPartialRefund))
                    {
                        return RefundStatus.ErrorStatus("You don't have permission to perform a partial refund");
                    }

                    if (refundRequest.RefundAmount > 0)
                    {
                        var refundTransaction = new PendingTransaction()
                        {
                            RefundReference = transaction.InternalReference,
                            Amount = (refundRequest.RefundAmount * -1),
                            AccountReference = transaction.AccountReference,
                            EntryDate = DateTime.Now,
                            FundCode = transaction.FundCode,
                            MopCode = transaction.MopCode,
                            VatCode = transaction.VatCode,
                            UserCode = SecurityContext.UserId,
                            Narrative = reason,
                            StatusId = (int)Enums.TransactionStatus.Pending
                        };

                        totalRefundAmount += refundRequest.RefundAmount;
                        pspReference = transaction.PspReference;

                        validRefundTransactions.Add(refundTransaction);
                    }
                }

                if (!validRefundTransactions.Any())
                {
                    return RefundStatus.ErrorStatus("No valid refunds found to create");
                }

                var savePendingTransactionResult = _transactionService.SavePendingTransactions(validRefundTransactions, "backoffice");
                if (!savePendingTransactionResult.Success)
                {
                    return RefundStatus.ErrorStatus("Refund records could not be saved to database");
                }

                _paymentIntegrationClient.ConfigureSettings(GetPaymentIntegration(mopCode));

                var refundResponse = Task.Run(async () => await _paymentIntegrationClient.RequestRefund(new Clients.PaymentIntegrationClient.RefundRequest()
                {
                    Amount = totalRefundAmount,
                    Reference = pspReference,
                    TransactionDate = transactionDate
                })).Result;

                if (refundResponse.Success)
                {
                    return RefundStatus.AcceptedStatus(totalRefundAmount);
                }

                // TODO: if we fail or have an exception we need to mark those as processed.
                _transactionService.MarkRefundsAsFailed(internalReference, reason);

                return RefundStatus.FailStatus(string.Empty); // HIGH: Sort this out - return refundResponse.response?
            }
            catch (Exception e)
            {
                _transactionService.MarkRefundsAsFailed(internalReference, reason);
                return RefundStatus.ErrorStatus(e.Message);
            }
        }

        private bool HasRefundsPending(string internalReference)
        {
            var refundsPending = _transactionService.GetPendingRefunds(internalReference);
            return refundsPending.Any();
        }

        private PaymentIntegration GetPaymentIntegration(string mopCode)
        {
            var mop = UnitOfWork.Mops.GetMop(mopCode);

            var paymentIntegrationId = Convert.ToInt32(mop.Metadata.First(x => x.Key == MopMetadataKeys.PaymentIntegrationId).Value);

            return UnitOfWork.PaymentIntegrations.SingleOrDefault(x => x.Id == paymentIntegrationId);
        }
    }
}
