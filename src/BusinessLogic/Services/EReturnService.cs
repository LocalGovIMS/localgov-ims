using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using BusinessLogic.Models.EReturns;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class EReturnService : BaseService, IEReturnService
    {
        private readonly IApproveEReturnsStrategy _approveEReturnsStrategy;
        private readonly ITemplateService _templateService;
        private readonly IEReturnReferenceValidator _eReturnReferenceValidator;
        private readonly IEReturnDescriptionValidator _eReturnDescriptionValidator;
        private readonly IEmailService _emailService;

        private readonly string _defaultFundCode;

        public EReturnService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IApproveEReturnsStrategy approveEReturnsStrategy
            , ITemplateService templateService
            , IEReturnReferenceValidator eReturnReferenceValidator
            , IEReturnDescriptionValidator eReturnDescriptionValidator
            , IEmailService emailService)
            : base(logger, unitOfWork, securityContext)
        {
            _approveEReturnsStrategy = approveEReturnsStrategy;
            _templateService = templateService;
            _eReturnReferenceValidator = eReturnReferenceValidator;
            _eReturnDescriptionValidator = eReturnDescriptionValidator;
            _emailService = emailService;

            _defaultFundCode = GetDefaultFundCode();
        }

        private string GetDefaultFundCode()
        {
            return UnitOfWork.Funds.GetAll(true).FirstOrDefault(x => x.IsAnEReturnDefaultFund()).FundCode;
        }

        public SearchResult<EReturnWrapper> SearchTransactions(SearchCriteria criteria)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturns)
                && !SecurityContext.IsInRole(Security.Role.EReturnAuthoriser)) return null;
            try
            {
                int eReturnsCount;
                var eReturns = UnitOfWork.EReturns.Search(criteria, out eReturnsCount)
                    .Select(x => new EReturnWrapper(x)).ToList();

                return new SearchResult<EReturnWrapper>()
                {
                    Count = eReturnsCount,
                    Items = eReturns,
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

        public EReturnWrapper GetEReturn(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturns)
                && !SecurityContext.IsInRole(Security.Role.EReturnAuthoriser)) return null;

            try
            {
                var item = UnitOfWork.EReturns.GetEReturn(id);
                return new EReturnWrapper(item);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(EReturn item)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturns))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                item.CreatedByUserId = SecurityContext.UserId;
                item.CreatedAt = DateTime.Now;
                item.StatusId = (int)Enums.EReturnStatus.New;

                UnitOfWork.EReturns.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                UnitOfWork.EReturns.SetEReturnNo(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                // Now Create the pending transations
                var template = _templateService.GetTemplate(item.TemplateId);
                var internalReference = GetNextReferenceId();
                foreach (var templateRow in template.TemplateRows)
                {
                    var pendingTransaction = new PendingTransaction()
                    {
                        TransactionReference = GetNextReferenceId(),
                        TransactionDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        AccountReference = templateRow.Reference,
                        FundCode = _defaultFundCode,
                        VatCode = templateRow.VatCode,
                        Narrative = templateRow.Description,
                        EReturnId = item.Id,
                        InternalReference = internalReference,
                        UserCode = SecurityContext.UserId,
                        MopCode = item.TypeId == (int)Enums.EReturnType.Cash ? GetCashMopCode() : GetEReturnChequeMopCode(),
                        VatRate = (float)templateRow.VAT.Percentage,
                        VatAmount = 0,
                        TemplateRowId = templateRow.Id
                    };

                    item.PendingTransactions.Add(pendingTransaction);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result() { Data = item };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create an eReturn");
            }
        }

        private string GetCashMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsACashPayment()).MopCode;
        }

        private string GetEReturnChequeMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAnEReturnChequePayment()).MopCode;
        }

        public IResult Update(EReturn item)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturns) && !SecurityContext.IsInRole(Security.Role.EReturnAuthoriser))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var existingItem = GetEReturn(item.Id);

                if (existingItem.EReturn.StatusId == (int)Enums.EReturnStatus.Authorised)
                    return new Result(string.Format("eReturn {0} has already been authorised, so cannot be edited", item.EReturnNo));

                if (existingItem.EReturn.StatusId == (int)Enums.EReturnStatus.Deleted)
                    return new Result(string.Format("eReturn {0} has already been deleted, so cannot be edited", item.EReturnNo));

                if (existingItem.EReturn.StatusId == (int)Enums.EReturnStatus.Voided)
                    return new Result(string.Format("eReturn {0} has already been voided, so cannot be edited", item.EReturnNo));

                if (existingItem.EReturn.StatusId == (int)Enums.EReturnStatus.Submitted)
                    return UpdateSubmitted(item, existingItem);

                return UpdateStandard(item, existingItem);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                var eReturnNo = string.Empty;
                if (item != null) eReturnNo = item.EReturnNo;
                return new Result(string.Format("Unable to update eReturn {0}", eReturnNo));
            }
        }

        private IResult UpdateStandard(EReturn item, EReturnWrapper existingItem)
        {
            try
            {
                // TODO: Validate the references on all lines
                if (item.PendingTransactions == null || !item.PendingTransactions.Any()) return new Result(string.Format("Data for eReturn {0} is incomplete", item.EReturnNo));

                foreach (var transaction in item.PendingTransactions)
                {
                    // Only validate if we've got an amount. Any row without an amount eventually gets discarded
                    if (transaction.Amount.HasValue && transaction.Amount != 0)
                    {
                        var existingTransaction = existingItem.EReturn.PendingTransactions.FirstOrDefault(x => x.Id == transaction.Id);
                        if (existingTransaction == null) return new Result(string.Format("Data for eReturn {0} is incomplete", item.EReturnNo));

                        var result = _eReturnReferenceValidator.Validate(transaction.AccountReference, (int)existingTransaction.TemplateRowId);
                        if (!result.Success) return new Result(string.Format("eReturn reference '{0}' is not valid", transaction.AccountReference));

                        result = _eReturnDescriptionValidator.Validate(transaction.Narrative, (int)existingTransaction.TemplateRowId);
                        if (!result.Success) return new Result(string.Format("eReturn description '{0}' is not valid", transaction.Narrative));
                    }
                }

                UnitOfWork.EReturns.Update(item);

                if (item.EReturnCashes != null && item.EReturnCashes.Any())
                {
                    var cash = item.EReturnCashes.FirstOrDefault(); // There is only ever one

                    // TODO: This could be placed into a method of cash, or an extension method - it shouldn't be here though
                    cash.Total = (cash.Pence1 ?? 0)
                        + (cash.Pence2 ?? 0)
                        + (cash.Pence5 ?? 0)
                        + (cash.Pence10 ?? 0)
                        + (cash.Pence20 ?? 0)
                        + (cash.Pence50 ?? 0)
                        + (cash.Pounds1 ?? 0)
                        + (cash.Pounds2 ?? 0)
                        + (cash.Pounds5 ?? 0)
                        + (cash.Pounds10 ?? 0)
                        + (cash.Pounds20 ?? 0)
                        + (cash.Pounds50 ?? 0);

                    UnitOfWork.EReturnCashes.Update(cash, item.Id);
                }

                if (item.EReturnCheques != null && item.EReturnCheques.Any())
                {
                    UnitOfWork.EReturnCheques.Update(item.EReturnCheques.ToList(), item.Id);
                }

                UnitOfWork.PendingTransactions.Update(item.PendingTransactions.ToList(), item.Id);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                var eReturnNo = string.Empty;
                if (item != null) eReturnNo = item.EReturnNo;
                return new Result(string.Format("Unable to update eReturn {0}", eReturnNo));
            }
        }

        private IResult UpdateSubmitted(EReturn item, EReturnWrapper existingItem)
        {
            try
            {
                if (!SecurityContext.IsInRole(Security.Role.EReturnAuthoriser)) return null;

                foreach (var transaction in item.PendingTransactions)
                {
                    var existingTransaction = existingItem.EReturn.PendingTransactions.FirstOrDefault(x => x.Id == transaction.Id);
                    existingTransaction.Amount = transaction.Amount;
                }

                if (item.EReturnCashes != null && item.EReturnCashes.Any())
                {
                    var cash = item.EReturnCashes.FirstOrDefault();  // There is only ever one...

                    cash.Total = (cash.Pence1 ?? 0)
                        + (cash.Pence2 ?? 0)
                        + (cash.Pence5 ?? 0)
                        + (cash.Pence10 ?? 0)
                        + (cash.Pence20 ?? 0)
                        + (cash.Pence50 ?? 0)
                        + (cash.Pounds1 ?? 0)
                        + (cash.Pounds2 ?? 0)
                        + (cash.Pounds5 ?? 0)
                        + (cash.Pounds10 ?? 0)
                        + (cash.Pounds20 ?? 0)
                        + (cash.Pounds50 ?? 0);

                    UnitOfWork.EReturnCashes.Update(cash, item.Id);
                }

                if (item.EReturnCheques != null && item.EReturnCheques.Any())
                {
                    UnitOfWork.EReturnCheques.Update(item.EReturnCheques.ToList(), item.Id);
                }

                UnitOfWork.PendingTransactions.Update(existingItem.EReturn.PendingTransactions.ToList(), item.Id);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                var eReturnNo = string.Empty;
                if (item != null) eReturnNo = item.EReturnNo;
                return new Result(string.Format("Unable to update eReturn {0}", eReturnNo));
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturns)
                 && !SecurityContext.IsInRole(Security.Role.EReturnAuthoriser))
                return new Result("You do not have permission to perform the requested action");

            EReturn item = null;

            try
            {
                item = UnitOfWork.EReturns.GetEReturn(id);

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Authorised)
                    return new Result(string.Format("Unable to delete eReturn {0} - it has already been authorised", item.EReturnNo));

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Deleted)
                    return new Result(string.Format("Unable to delete eReturn {0} - it has already been deleted", item.EReturnNo));

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Voided)
                    return new Result(string.Format("Unable to delete eReturn {0} - it has already been voided", item.EReturnNo));

                // In theory this shouldn't happen - but if anyone messes with the 
                // status of an EReturn, we could end up deleting a processed 
                // transcation, so let's check to see it any exist
                if (item.ProcessedTransactions.Any())
                    return new Result(string.Format("Unable to delete eReturn {0} - processed transactions exist", item.EReturnNo));

                // Soft delete the EReturn
                item.StatusId = item.StatusId == (int)Enums.EReturnStatus.Submitted
                    ? (int)Enums.EReturnStatus.Deleted
                    : (int)Enums.EReturnStatus.Voided;

                item.ApprovedAt = DateTime.Now;
                item.ApprovedByUserId = SecurityContext.UserId;

                if (item.StatusId == (int)Enums.EReturnStatus.Deleted) _emailService.SendEReturnDeletedEmail(item.EReturnNo, SecurityContext.UserName);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                var eReturnNo = string.Empty;
                if (item != null) eReturnNo = item.EReturnNo;
                return new Result(string.Format("Unable to delete eReturn {0}", eReturnNo));
            }
        }

        public IResult Submit(int id)
        {
            EReturn item = null;

            try
            {
                if (!SecurityContext.IsInRole(Security.Role.EReturns))
                    return new Result("You do not have permission to perform the requested action");

                item = UnitOfWork.EReturns.GetEReturn(id);

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Submitted)
                    return new Result(string.Format("Unable to submit eReturn {0} - it has already been submitted", item.EReturnNo));

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Deleted)
                    return new Result(string.Format("Unable to submit eReturn {0} - it has already been deleted", item.EReturnNo));

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Voided)
                    return new Result(string.Format("Unable to submit eReturn {0} - it has already been voided", item.EReturnNo));

                if ((Enums.EReturnStatus)item.StatusId == Enums.EReturnStatus.Authorised)
                    return new Result(string.Format("Unable to submit eReturn {0} - it has already been authorised", item.EReturnNo));

                if ((Enums.EReturnType)item.TypeId == Enums.EReturnType.Cash)
                {
                    var cash = item.EReturnCashes.FirstOrDefault();
                    if (cash != null)
                    {
                        if (string.IsNullOrEmpty(cash.BagNumber))
                            return new Result(string.Format("Unable to submit eReturn {0} - the bag number is missing", item.EReturnNo));

                        if (cash.Total != item.PendingTransactions.Sum(x => x.Amount))
                            return new Result(string.Format("Unable to submit eReturn {0} - the cash analysis balance doesn't match the eReturn balance", item.EReturnNo));
                    }
                    else
                    {
                        return new Result(string.Format("Unable to submit eReturn {0} - the cash analysis isn't set", item.EReturnNo));
                    }
                }

                if ((Enums.EReturnType)item.TypeId == Enums.EReturnType.Cheque)
                {
                    if (item.EReturnCheques == null || !item.EReturnCheques.Any())
                        return new Result(string.Format("Unable to submit eReturn {0} - no cheque details have been provided", item.EReturnNo));

                    var totalChequeAmount = item.EReturnCheques.Sum(x => x.Amount);
                    if (totalChequeAmount <= 0)
                    {
                        return new Result(string.Format("Unable to submit eReturn {0} - the total cheque value must be greater than zero", item.EReturnNo));
                    }
                    if (totalChequeAmount != item.PendingTransactions.Sum(x => x.Amount))
                        return new Result(string.Format("Unable to submit eReturn {0} - the cheque analysis balance doesn't match the eReturn balance", item.EReturnNo));
                }

                // In theory this shouldn't happen - but if anyone messes with the 
                // status of an EReturn, we could end up submitted a processed 
                // transcation, so let's check to see it any exist
                if (item.ProcessedTransactions.Any())
                    return new Result(string.Format("Unable to submit eReturn {0} - processed transcations exist so it must already have been submitted and approved", item.EReturnNo));

                // Set the EReturn as submitted
                item.StatusId = (int)Enums.EReturnStatus.Submitted;
                item.SubmittedByUserId = SecurityContext.UserId;
                item.SubmittedAt = DateTime.Now;

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception)
            {
                var eReturnNo = string.Empty;
                if (item != null) eReturnNo = item.EReturnNo;
                return new Result(string.Format("Unable to submit eReturn {0}", eReturnNo));
            }
        }

        public IResult Approve(List<int> items)
        {
            if (!SecurityContext.IsInRole(Security.Role.EReturnAuthoriser))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                var data = items.Select(x => new Tuple<int, string>(x, GetNextReferenceId())).ToList();
                return _approveEReturnsStrategy.Execute(data);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to approve eReturn");
            }
        }
    }
}