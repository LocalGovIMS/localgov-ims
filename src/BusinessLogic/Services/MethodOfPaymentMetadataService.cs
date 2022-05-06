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

        public SearchResult<MopMetaData> Search(SearchCriteria criteria)
        {
            try
            {
                var items = UnitOfWork.MopMetadatas.Search(criteria, out int itemsCount).ToList();

                return new SearchResult<MopMetaData>()
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

        public MopMetaData Get(int id)
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

        public IResult Create(MopMetaData item)
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

        public IResult Update(MopMetaData item)
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
                new Metadata() { Key = "IsAJournal", Description = "Is a Journal" },
                new Metadata() { Key = "IsAJournalReallocation", Description = "Is a Journal Reallocation" },
                new Metadata() { Key = "IsATransferOut", Description = "Is a Transfer Out" },
                new Metadata() { Key = "IsACardSelfServicePayment", Description = "Is a Card Self Service Payment" },
                new Metadata() { Key = "IsRefundable", Description = "Is Refundable" },
                new Metadata() { Key = "IsATransferIn", Description = "Is a Transfer In" },
                new Metadata() { Key = "IsACardAtpPayment", Description = "Is a Card Atp Payment" },
                new Metadata() { Key = "IsAChequePayment", Description = "Is a Cheque Payment" },
                new Metadata() { Key = "IsACardViaStaffPayment", Description = "Is a Card Via Staff Payment" },
                new Metadata() { Key = "IsACashPayment", Description = "Is a Cash Payment" },
                new Metadata() { Key = "IsAnEReturnChequePayment", Description = "Is an EReturn Cheque Payment" },
                new Metadata() { Key = "PaymentIntegrationId", Description = "Payment Integration" },
                new Metadata() { Key = "BackgroundColour", Description = "Background Colour" },
                new Metadata() { Key = "TextColour", Description = "Text Colour" },
                new Metadata() { Key = "IsACardPaymentFee", Description = "Is a Card Payment Fee" },
                new Metadata() { Key = "IncursAFee", Description = "Incurs a Fee" }
            };
        }
    }
}