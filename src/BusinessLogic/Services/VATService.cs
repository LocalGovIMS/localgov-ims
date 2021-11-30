using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class VatService : BaseService, IVatService
    {
        public VatService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public Vat GetByVatCode(string vatCode)
        {
            try
            {
                return UnitOfWork.Vats.GetVatByVatCode(vatCode);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading VAT code", e);
                return null;
            }
        }

        public Vat GetByFundCode(string fundCode)
        {
            try
            {
                return UnitOfWork.Funds.GetByFundCode(fundCode).Vat;
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading VAT code", e);
                return null;
            }
        }

        public List<Vat> GetAllCodes()
        {
            try
            {
                return GetAllCodes(false);
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading VAT codes", e);
                return null;
            }
        }

        public List<Vat> GetAllCodes(bool includeDisabled)
        {
            try
            {
                return UnitOfWork.Vats.GetAll(includeDisabled).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Exception loading VAT codes", e);
                return null;
            }
        }

        public IResult Create(Vat item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin))
                return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Vats.Add(item);

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to create VAT record");
            }
        }

        public IResult Update(Vat item)
        {
            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)) return new Result("You do not have permission to perform the requested action");

            try
            {
                UnitOfWork.Vats.Update(item);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update this VAT record");
            }
        }
    }
}