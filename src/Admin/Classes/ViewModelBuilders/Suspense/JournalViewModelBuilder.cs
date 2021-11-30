using Admin.Models.Suspense;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Suspense
{
    public class JournalViewModelBuilder : BaseViewModelBuilder<JournalViewModel, string>
    {
        private readonly IFundService _fundService;
        private readonly IVatService _vatService;
        private readonly IMethodOfPaymentService _mopService;

        public JournalViewModelBuilder(ILog log
            , IFundService fundService
            , IMethodOfPaymentService mopService
            , IVatService vatService)
            : base(log)
        {
            _fundService = fundService;
            _vatService = vatService;
            _mopService = mopService;
        }

        protected override JournalViewModel OnBuild()
        {
            return new JournalViewModel()
            {
                Funds = GetFundsList(),
                CreditNoteFunds = GetCreditNoteFundsList(),
                VatCodes = GetVatList(),
                MopCodes = GetMopList(),
                DefaultJournalReallocationMopCode = GetDefaultJournalReallocationMopCode()
            };
        }

        protected override JournalViewModel OnBuild(string criteria)
        {
            return new JournalViewModel()
            {
                Funds = GetFundsList(),
                CreditNoteFunds = GetCreditNoteFundsList(),
                VatCodes = GetVatList(),
                MopCodes = GetMopList(),
                DefaultJournalReallocationMopCode = GetDefaultJournalReallocationMopCode()
            };
        }

        protected override JournalViewModel OnRebuild(JournalViewModel model)
        {
            model.Funds = GetFundsList();
            model.VatCodes = GetVatList();
            model.MopCodes = GetMopList();

            return model;
        }

        private SelectList GetMopList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _mopService.GetAllMops().OrderBy(x => x.MopName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.MopCode,
                    Text = item.MopName
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetVatList()
        {
            var vatSelectListItems = new List<SelectListItem>();
            var vats = _vatService.GetAllCodes().OrderBy(x => x.VatCode);

            foreach (var vat in vats)
            {
                vatSelectListItems.Add(new SelectListItem()
                {
                    Value = vat.VatCode,
                    Text = vat.VatCode + " - " + vat.Percentage
                });
            }

            return new SelectList(vatSelectListItems, true);
        }

        private SelectList GetFundsList()
        {
            var funds = _fundService.GetAllFunds().OrderBy(x => x.FundName);

            return GetFundsList(funds);
        }

        private SelectList GetCreditNoteFundsList()
        {
            var funds = _fundService.GetCreditNoteFunds().OrderBy(x => x.FundName);

            return GetFundsList(funds);
        }

        private SelectList GetFundsList(IOrderedEnumerable<BusinessLogic.Entities.Fund> funds)
        {
            var fundSelectListItems = new List<SelectListItem>();

            foreach (var fund in funds)
            {
                var dataAttributes = new List<ValuePair>();

                dataAttributes.Add(new ValuePair() { Key = "vat-override", Value = fund.VatOverride.ToString() });
                dataAttributes.Add(new ValuePair() { Key = "vat-default-code", Value = fund.VatCode });

                fundSelectListItems.Add(new SelectListItem()
                {
                    Value = fund.FundCode,
                    Text = fund.FundName,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(fundSelectListItems, true);
        }

        private string GetDefaultJournalReallocationMopCode()
        {
            return _mopService.GetAllMops(true)
                .FirstOrDefault(x => x.IsAJournalReallocation())
                .MopCode;
        }
    }
}