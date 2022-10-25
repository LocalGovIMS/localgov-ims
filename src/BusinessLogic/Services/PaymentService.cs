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
using BusinessLogic.Interfaces.Validators;
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
        private readonly ITransactionFeeValidator _transactionFeeValidator;

        public PaymentService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionService transactionService
            , ICryptographyService cryptographyService
            , IFundService fundService
            , ITransactionFeeValidator transactionFeeValidator)
            : base(logger, unitOfWork, securityContext)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _cryptographyService = cryptographyService ?? throw new ArgumentNullException("cryptographyService");
            _fundService = fundService ?? throw new ArgumentNullException("fundService");
            _transactionFeeValidator = transactionFeeValidator ?? throw new ArgumentNullException("transactionFeeValidator");
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
            Logger.DebugFormat("Processing Payment: {0}", JsonConvert.SerializeObject(paymentResult));

            if (!SecurityContext.IsInRole(Security.Role.Payments)) return null;
            
            var transactions = _transactionService.GetPendingTransactionsByInternalReference(paymentResult.MerchantReference);
            if (transactions == null || !transactions.Any())
                throw new InvalidOperationException($"Unable to load transactions for refernece {paymentResult.MerchantReference}");

            var response = new ProcessPaymentResponse() 
            { 
                RedirectUrl = transactions.FailUrl(),
                IsLegacy = transactions.Legacy() ?? false,
                Success = false
            };

            switch (paymentResult.AuthResult.ToUpper())
            {
                case ResponseCode.Authorised:
                    var authoriseResult = _transactionService.AuthorisePendingTransactionByInternalReference(new Models.Transactions.AuthorisePendingTransactionByInternalReferenceArgs()
                    {
                        InternalReference = paymentResult.MerchantReference,
                        PspReference = paymentResult.PspReference,
                        CardPrefix = paymentResult.CardPrefix,
                        CardSuffix = paymentResult.CardSuffix
                    } );

                    if (!authoriseResult.Success)
                        throw new InvalidOperationException(authoriseResult.ErrorMessage);

                    response.RedirectUrl = $"{transactions.SuccessUrl()}/{paymentResult.PspReference}";
                    response.Success = true;
                    break;

                case ResponseCode.Pending:
                    var pendingResult = _transactionService.SuspendPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        GetCardSelfServiceMopCode()); // HIGH: Why is a MOP code being used as an Auth Result value?

                    if (!pendingResult.Success)
                        throw new InvalidOperationException(pendingResult.Error);

                    response.RedirectUrl = $"{transactions.SuccessUrl()}";
                    response.Success = true;
                    break;

                case ResponseCode.Refused:
                case ResponseCode.Error:
                    var failResult = _transactionService.FailPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        paymentResult.AuthResult);

                    if (!failResult.Success)
                        throw new InvalidOperationException(failResult.Error);

                    response.RedirectUrl = transactions.FailUrl();
                    response.Success = true;
                    break;

                case ResponseCode.Cancelled:
                    var cancelResult = _transactionService.FailPendingTransaction(
                        paymentResult.MerchantReference,
                        paymentResult.PspReference,
                        paymentResult.AuthResult);

                    if (!cancelResult.Success)
                        throw new InvalidOperationException(cancelResult.Error);

                    response.RedirectUrl = transactions.CancelUrl();
                    response.Success = true;
                    break;
            }

            ProcessFee(paymentResult, transactions);
            
            return response;
        }

        public IResult ProcessFee(PaymentResult paymentResult)
        {
            var transactions = _transactionService.GetPendingTransactionsByInternalReference(paymentResult.MerchantReference);

            return ProcessFee(paymentResult, transactions);
        }

        private IResult ProcessFee(PaymentResult paymentResult, List<PendingTransaction> transactions)
        {
            try
            {
                // Initialise
                var cardPaymentFeeMopCode = GetCardPaymentFeeMopCode();
                var mop = UnitOfWork.Mops.GetMop(transactions.First().MopCode);
                var validatorArgs = new Validators.TransactionFeeValidatorArgs()
                {
                    Transactions = transactions,
                    PaymentResult = paymentResult,
                    CardPaymentFeeMopCode = cardPaymentFeeMopCode,
                    Mop = mop
                };

                // Validate
                var validationResult = _transactionFeeValidator.Validate(validatorArgs);

                if (!validationResult.Success)
                    return validationResult;

                // Process
                var feeTransaction = transactions.ToFeeTransaction(paymentResult, mop, cardPaymentFeeMopCode);

                _transactionService.CreateProcessedTransaction(new CreateProcessedTransactionArgs() { ProcessedTransaction = feeTransaction, GenerateNewReference = false });

                return new Result();
            }
            catch(Exception ex)
            {
                return new Result(ex.Message);
            }
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
                    CardHolderAddressLine1 = paymentDetails.AddressLine1,
                    CardHolderAddressLine2 = paymentDetails.AddressLine2,
                    CardHolderAddressLine3 = paymentDetails.AddressLine3,
                    CardHolderAddressLine4 = paymentDetails.AddressLine4,
                    CardHolderPostCode = paymentDetails.Postcode,
                    VatCode = paymentDetails.VatCode,
                    CallRecordingSource = paymentDetails.CallRecordingSource,
                    CallRecordingUserName = paymentDetails.CallRecordingUserName,
                    UserCode = SecurityContext.UserId
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

            var paymentIntegrationId = Convert.ToInt32(mop.Metadata.First(x => x.MetadataKey.Name == MopMetadataKeys.PaymentIntegrationId).Value);

            return UnitOfWork.PaymentIntegrations.SingleOrDefault(x => x.Id == paymentIntegrationId);
        }
    }
}
