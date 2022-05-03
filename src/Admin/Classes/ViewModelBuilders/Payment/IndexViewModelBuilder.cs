using Admin.Models.Payment;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Payments;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Payment
{
    public class IndexViewModelBuilder : BaseViewModelBuilder<IndexViewModel, IndexViewModel>
    {
        private readonly IFundService _fundService;
        private readonly IVatService _vatService;
        private readonly IUserMethodOfPaymentService _userMethodOfPaymentService;
        private readonly ISecurityContext _securityContext;

        public IndexViewModelBuilder(ILog log
            , IFundService fundService
            , IVatService vatService
            , IUserMethodOfPaymentService userMethodOfPaymentService
            , ISecurityContext securityContext) :
            base(log)
        {
            _fundService = fundService;
            _vatService = vatService;
            _userMethodOfPaymentService = userMethodOfPaymentService;
            _securityContext = securityContext;
        }

        protected override IndexViewModel OnBuild()
        {
            var funds = _fundService.GetAllFunds().OrderBy(x => x.FundName);
            var vatCodes = _vatService.GetAllCodes().OrderBy(x => x.VatCode);
            var mopCodes = _userMethodOfPaymentService.GetByUserId(_securityContext.UserId).OrderBy(x => x.MopCode);

            return new IndexViewModel()
            {
                Funds = GetFundsList(funds),
                VatCodes = GetVatCodeList(vatCodes),
                MopCodes = GetMopCodeList(mopCodes),
                SearchEnabledFundCodes = GetSearchEnabledFundCodes(funds),
                Basket = new Basket() // TODO: Potentially build this up from session?
            };
        }

        protected override IndexViewModel OnBuild(IndexViewModel source)
        {
            var funds = _fundService.GetAllFunds().OrderBy(x => x.FundName);
            var vatCodes = _vatService.GetAllCodes().OrderBy(x => x.VatCode);
            var mopCodes = _userMethodOfPaymentService.GetByUserId(_securityContext.UserId).OrderBy(x => x.MopCode);

            source.VatCodes = GetVatCodeList(vatCodes);
            source.Funds = GetFundsList(funds);
            source.MopCodes = GetMopCodeList(mopCodes);
            source.SearchEnabledFundCodes = GetSearchEnabledFundCodes(funds);

            return source;
        }

        private SelectList GetVatCodeList(IOrderedEnumerable<BusinessLogic.Entities.Vat> vatCodes)
        {
            var vatSelectListItems = new List<SelectListItem>();

            foreach (var vatCode in vatCodes)
            {
                var vatLabel = vatCode.VatCode + " (" + Math.Round((vatCode.Percentage ?? 0) * 100, 2) + "%)";

                vatSelectListItems.Add(new SelectListItem()
                {
                    Value = vatCode.VatCode,
                    Text = vatLabel
                });
            }

            return new SelectList(vatSelectListItems, false);
        }

        private SelectList GetFundsList(IOrderedEnumerable<BusinessLogic.Entities.Fund> funds)
        {
            var fundSelectListItems = new List<SelectListItem>();

            foreach (var fund in funds)
            {
                var dataAttributes = new List<ValuePair>
                {
                    new ValuePair() { Key = "vat-override", Value = fund.VatOverride.ToString() },
                    new ValuePair() { Key = "vat-default-code", Value = fund.VatCode }
                };

                fundSelectListItems.Add(new SelectListItem()
                {
                    Value = fund.FundCode,
                    Text = fund.FundName,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(fundSelectListItems, true);
        }

        private SelectList GetMopCodeList(IOrderedEnumerable<BusinessLogic.Entities.UserMethodOfPayment> mopCodes)
        {
            var selectListItems = new List<SelectListItem>();

            if (mopCodes.Any())
            {
                foreach (var item in mopCodes)
                {
                    var dataAttributes = new List<ValuePair>
                    {
                        new ValuePair() { Key = "mop-minimum-amount", Value = item.Mop.MinimumAmount.ToString() },
                        new ValuePair() { Key = "mop-maximum-amount", Value = item.Mop.MaximumAmount.ToString() }
                    };

                    selectListItems.Add(new SelectListItem()
                    {
                        Value = item.MopCode,
                        Text = item.Mop.MopName + " (" + item.MopCode + ")",
                        DataAttributes = dataAttributes
                    });
                }
            }
            else
            {
                var mop = _userMethodOfPaymentService.GetDefaultUserMethodOfPayment();

                var dataAttributes = new List<ValuePair>
                {
                    new ValuePair() { Key = "mop-minimum-amount", Value = mop.MinimumAmount.ToString() },
                    new ValuePair() { Key = "mop-maximum-amount", Value = mop.MaximumAmount.ToString() }
                };

                selectListItems.Add(new SelectListItem()
                {
                    Value = mop.MopCode,
                    Text = string.Format("{0} ({1})", mop.MopName, mop.MopCode),
                    DataAttributes = dataAttributes
                });
            }
            return new SelectList(selectListItems, false);
        }

        private string GetSearchEnabledFundCodes(IOrderedEnumerable<BusinessLogic.Entities.Fund> funds)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                funds.Where(x => x.AccountExist == true).Select(x => x.FundCode).ToList());
        }
    }
}