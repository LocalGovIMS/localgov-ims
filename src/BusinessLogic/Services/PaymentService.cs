using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic.Services
{
    public class PaymentService : BaseService, IPaymentService
    {
        private readonly ITransactionService _transactionService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IFundService _fundService;

        public PaymentService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionService transactionService
            , ICryptographyService cryptographyService
            , IFundService fundService)
            : base(logger, unitOfWork, securityContext)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _cryptographyService = cryptographyService ?? throw new ArgumentNullException("cryptographyService");
            _fundService = fundService ?? throw new ArgumentNullException("fundService");
        }

        public PaymentResponse CreateHppPayment(PaymentDetails paymentDetails)
        {
            if (!SecurityContext.IsInRole(Security.Role.Payments)) return null;

            try
            {
                return SaveHppPayment(paymentDetails);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new PaymentResponse()
                {
                    ErrorMessage = e.Message
                };
            }
        }

        public PaymentResponse CreateHppPayments(List<PaymentDetails> paymentDetails)
        {
            if (!SecurityContext.IsInRole(Security.Role.Payments)
                && !SecurityContext.IsInRole(Security.Role.ChequeProcess)
                && !SecurityContext.IsInRole(Security.Role.PostPayment)) return null;

            try
            {
                return SaveHppPayment(paymentDetails);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new PaymentResponse()
                {
                    ErrorMessage = e.Message
                };
            }
        }

        public ProcessPaymentResponse ProcessPayment(PaymentResult paymentResult)
        {
            Logger.DebugFormat("Processing Payment: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(paymentResult));

            if (!SecurityContext.IsInRole(Security.Role.Payments)) return null;

            var transactions = _transactionService.GetPendingTransactionsByInternalReference(paymentResult.MerchantReference);
            var transaction = transactions.First();

            var response = new ProcessPaymentResponse();

            switch (paymentResult.AuthResult.ToUpper())
            {
                case ResponseCode.Authorised:
                    _transactionService.AuthorisePendingTransactionByInternalReference(paymentResult.MerchantReference, paymentResult.PspReference);
                    response.RedirectUrl = $"{transaction.SuccessUrl}/{paymentResult.PspReference}";
                    response.Success = true;
                    break;

                case ResponseCode.Pending:
                    _transactionService.SuspendPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        GetCardSelfServiceMopCode()); // HIGH: Why is a MOP code being used as an Auth Result value?
                    response.RedirectUrl = $"{transaction.SuccessUrl}";
                    response.Success = true;
                    break;

                case ResponseCode.Refused:
                case ResponseCode.Error:
                    _transactionService.FailPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        paymentResult.AuthResult);
                    response.RedirectUrl = transaction.FailUrl;
                    response.Success = true;
                    break;

                case ResponseCode.Cancelled:
                    _transactionService.FailPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        paymentResult.AuthResult);
                    response.RedirectUrl = transaction.CancelUrl;
                    response.Success = true;
                    break;
            }

            response.IsLegacy = transaction.Legacy ?? false;

            ProcessFee(paymentResult, transactions);

            return response;
        }

        public IResult ProcessFee(PaymentResult paymentResult)
        {
            var transactions = _transactionService.GetPendingTransactionsByInternalReference(paymentResult.MerchantReference);

            ProcessFee(paymentResult, transactions);

            return new Result();
        }

        private void ProcessFee(PaymentResult paymentResult, List<PendingTransaction> transactions)
        {
            var transaction = transactions.First();

            var mop = UnitOfWork.Mops.GetMop(transaction.MopCode);

            if (!mop.IncursAFee()) return;

            var feeTransaction = transactions.ToFeeTransaction(paymentResult.PspReference, paymentResult.Fee, GetCardPaymentFeeMopCode());

            if (mop.IsARechargeFee())
            {
                feeTransaction.AccountReference = transaction.AccountReference;
                feeTransaction.FundCode = transaction.FundCode;
            }
            else if (mop.IsABusinessFee())
            {
                feeTransaction.AccountReference = mop.GetMopMetaDataValue<string>(MopMetaDataKeys.FeeAccountReference);
                feeTransaction.FundCode = mop.GetMopMetaDataValue<string>(MopMetaDataKeys.FeeFundCode);
            }

            _transactionService.CreateProcessedTransaction(new CreateProcessedTransactionArgs() { ProcessedTransaction = feeTransaction, GenerateNewReference = false });
        }

        private string GetCardSelfServiceMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsACardSelfServicePayment()).MopCode;
        }

        private string GetCardPaymentFeeMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsACardPaymentFee()).MopCode;
        }

        public ProcessPaymentResponse ProcessPayments(List<PaymentDetails> payments, PaymentTypeEnum type)
        {
            Logger.DebugFormat("Processing payments for PaymentTypeEnum: {0}", (int)type);

            // User must be in one of these roles
            if (!SecurityContext.IsInRole(Security.Role.Payments)
                && !SecurityContext.IsInRole(Security.Role.ChequeProcess)
                && !SecurityContext.IsInRole(Security.Role.PostPayment)) return null;

            // Type must relate to the correct role if we don't have payments
            if (!SecurityContext.IsInRole(Security.Role.Payments))
            {
                if (type == PaymentTypeEnum.Cheque && !SecurityContext.IsInRole(Security.Role.ChequeProcess)) return null;
                if (type == PaymentTypeEnum.Post && !SecurityContext.IsInRole(Security.Role.PostPayment)) return null;
            }

            try
            {
                if (type == PaymentTypeEnum.Cheque)
                {
                    foreach (var payment in payments)
                    {
                        payment.MopCode = GetChequePaymentMopCode();
                    }
                }

                return SavePayments(payments);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new ProcessPaymentResponse()
                {
                    Success = false
                };
            }
        }

        private string GetChequePaymentMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAChequePayment()).MopCode;
        }

        private PaymentResponse SaveHppPayment(PaymentDetails paymentDetails)
        {
            return SaveHppPayment(new List<PaymentDetails>() { paymentDetails });
        }

        private ProcessPaymentResponse SavePayments(List<PaymentDetails> payments)
        {
            Logger.DebugFormat("Processing {0} payments", payments == null ? "[NULL]" : payments.Count.ToString());

            var processedTransactions = new List<ProcessedTransaction>();
            foreach (var payment in payments)
            {
                // This will return a list of all the funds a user has access to - if they are a super user it will be all, if not, it will be determined by their fund group funds
                var funds = _fundService.GetAllFunds().Select(x => x.FundCode);

                if (!funds.Contains(payment.Fund))
                {
                    return new ProcessPaymentResponse()
                    {
                        Success = false,
                        RedirectUrl = payment.FailUrl
                    };
                }


                if (payment.Amount != Decimal.Round(payment.Amount, 2))
                {
                    Logger.WarnFormat("An invalid payment amount has been discovered: {0}", payment.Amount);
                    return new ProcessPaymentResponse()
                    {
                        Success = false,
                        RedirectUrl = payment.FailUrl
                    };
                }

                processedTransactions.Add(new ProcessedTransaction()
                {
                    AccountReference = payment.AccountReference,
                    Amount = payment.Amount,
                    ChequeAccountNumber = payment.BankAccountNo,
                    ChequeSortCode = payment.SortCode,
                    ChequeNumber = payment.ChequeNumber,
                    FundCode = payment.Fund,
                    VatCode = payment.VatCode,
                    OfficeCode = SecurityContext.OfficeCode,
                    MopCode = payment.MopCode,
                    Narrative = payment.Narrative,
                    ChequeName = payment.ChequeName,
                    UserCode = SecurityContext.UserId
                });
            }

            Logger.Debug("Saving new transactions");
            var result = _transactionService.SaveChequesToProcessed(processedTransactions);
            Logger.DebugFormat("Save result: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(result));

            Logger.Debug("Getting App URL");
            var appUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            Logger.DebugFormat("Got App URL: {0}", appUrl);

            if (result.Success)
            {
                return new ProcessPaymentResponse()
                {
                    Success = result.Success,
                    RedirectUrl = appUrl + "/Transaction/Details/" + result.PaymentId
                };
            }
            return new ProcessPaymentResponse() { Success = result.Success, RedirectUrl = payments[0].FailUrl };

        }

        private PaymentResponse SaveHppPayment(List<PaymentDetails> paymentDetailsList)
        {
            var pendingTransactionList = new List<PendingTransaction>();

            foreach (var paymentDetails in paymentDetailsList)
            {
                // This will return a list of all the funds a user has access to - if they are a super user it will be all, if not, it will be determined by their fund group funds
                var funds = _fundService.GetAllFunds().Select(x => x.FundCode);

                if (!funds.Contains(paymentDetails.Fund))
                {
                    return new PaymentResponse() { ErrorMessage = "A payment for this fund type cannot be processed" };
                }

                if (paymentDetails.Amount < (decimal)0.01)
                {
                    Logger.WarnFormat("An invalid payment amount has been discovered: {0}", paymentDetails.Amount);
                    return new PaymentResponse()
                    {
                        ResponseUrl = string.Empty,
                        ErrorMessage = "A payment for this fund type cannot be processed" // HIGH: is this required by ATP or should it be 
                    };
                }

                if (paymentDetails.Amount != Decimal.Round(paymentDetails.Amount, 2))
                {
                    Logger.WarnFormat("An invalid payment amount has been discovered: {0}", paymentDetails.Amount);
                    return new PaymentResponse() { ErrorMessage = "A payment amount is invalid" };
                }

                if (string.IsNullOrWhiteSpace(paymentDetails.MopCode))
                    paymentDetails.MopCode = GetCardSelfServiceMopCode();

                pendingTransactionList.Add(new PendingTransaction
                {
                    Amount = paymentDetails.Amount,
                    AppReference = paymentDetails.AppReference,
                    FundCode = paymentDetails.Fund,
                    SuccessUrl = paymentDetails.SuccessUrl,
                    CancelUrl = paymentDetails.CancelUrl,
                    FailUrl = paymentDetails.FailUrl,
                    EntryDate = paymentDetails.CreatedAt,
                    ExpiryDate = paymentDetails.ExpiryDate,
                    Narrative = paymentDetails.Narrative,
                    AccountReference = paymentDetails.AccountReference,
                    MopCode = paymentDetails.MopCode,
                    Legacy = paymentDetails.IsLegacy,
                    CardHolderName = paymentDetails.PayeeName,
                    CardHolderPremiseNumber = paymentDetails.HouseNumber,
                    CardHolderStreet = paymentDetails.Street,
                    CardHolderTown = paymentDetails.Town,
                    CardHolderPostCode = paymentDetails.Postcode,
                    VatCode = paymentDetails.VatCode,
                    CallRecordingSource = paymentDetails.CallRecordingSource,
                    CallRecordingUserName = paymentDetails.CallRecordingUserName
                });
            }

            var result = _transactionService.SavePendingTransactions(pendingTransactionList,
                paymentDetailsList.First().Source);

            if (result.Success)
            {
                var paymentIntegration = GetPaymentIntegration(paymentDetailsList.First().MopCode);

                Logger.DebugFormat("paymentIntegration", JsonConvert.SerializeObject(paymentIntegration));

                return new PaymentResponse()
                {
                    // HIGH: Get the correct payment integration URL associated with the MOP Code relating to this transaction
                    ResponseId = result.PaymentId,
                    ResponseUrl = string.Format("{0}/{1}/{2}"
                        , paymentIntegration.BaseUri
                        , result.PaymentId
                        , _cryptographyService.GetHash(result.PaymentId))
                };
            }

            return new PaymentResponse()
            {
                ErrorMessage = result.ErrorMessage
            };
        }

        private PaymentIntegration GetPaymentIntegration(string mopCode)
        {
            var mop = UnitOfWork.Mops.GetMop(mopCode);

            var paymentIntegrationId = Convert.ToInt32(mop.MetaData.First(x => x.Key == "PaymentIntegrationId").Value);

            return UnitOfWork.PaymentIntegrations.SingleOrDefault(x => x.Id == paymentIntegrationId);
        }
    }
}
