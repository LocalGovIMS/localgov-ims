using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using BusinessLogic.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class MethodOfPaymentMetadataService : BaseService, IMethodOfPaymentMetadataService
    {
        public MethodOfPaymentMetadataService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public SearchResult<MopMetadata> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.MopMetadatas.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<MopMetadata>()
                {
                    Count = itemsCount,
                    Items = items,
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

        public MopMetadata Get(int id)
        {
            try
            {
                return UnitOfWork.MopMetadatas.Get(id);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Create(MopMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MopMetadatas.Add(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create the MOP Metadata record");
            }
        }

        public IResult Update(MopMetadata item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.MopMetadatas.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this MOP Metadata record");
            }
        }

        public IResult Delete(int id)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var item = UnitOfWork.MopMetadatas.Get(id);

                UnitOfWork.MopMetadatas.Remove(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to delete this MOP Metadata record");
            }
        }

        public List<Metadata> GetMetadata()
        {
            return new List<Metadata>()
            {
                new Metadata() { Key = "IsAJournal", Description = "Is a journal" },
                new Metadata() { Key = "IsAJournalReallocation", Description = "Is a journal reallocation" },
                new Metadata() { Key = "IsATransferOut", Description = "Is a transfer out" },
                new Metadata() { Key = "IsACardSelfServicePayment", Description = "Is a card self-service payment" },
                new Metadata() { Key = "IsRefundable", Description = "Is refundable" },
                new Metadata() { Key = "IsATransferIn", Description = "Is a transfer in" },
                new Metadata() { Key = "IsACardAtpPayment", Description = "Is a card ATP payment" },
                new Metadata() { Key = "IsAChequePayment", Description = "Is a cheque payment" },
                new Metadata() { Key = "IsACardViaStaffPayment", Description = "Is a card via staff payment" },
                new Metadata() { Key = "IsACashPayment", Description = "Is a cash payment" },
                new Metadata() { Key = "IsAnEReturnChequePayment", Description = "Is an eReturn cheque payment" },
                new Metadata() { Key = "PaymentIntegrationId", Description = "Payment integration" },
                new Metadata() { Key = "BackgroundColour", Description = "Background colour" },
                new Metadata() { Key = "TextColour", Description = "Text colour" },
                new Metadata() { Key = "IsACardPaymentFee", Description = "Is a card payment fee" },
                new Metadata() { Key = "IncursAFee", Description = "Incurs a fee" },
                new Metadata() { Key = "IsACardPaymentFee", Description = "Is a card payment fee" },
                new Metadata() { Key = "IsAPaymentReversal", Description = "Is a payment reversal" }
            };
        }
    }
}