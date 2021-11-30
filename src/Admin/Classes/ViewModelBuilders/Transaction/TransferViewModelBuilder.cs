using Admin.Models.Transaction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Transaction
{
    public class TransferViewModelBuilder : BaseViewModelBuilder<TransferViewModel, string>
    {
        private readonly IFundService _fundService;
        private readonly IVatService _vatService;

        public TransferViewModelBuilder(ILog log
            , IFundService fundService
            , IVatService vatService)
            : base(log)
        {
            _fundService = fundService;
            _vatService = vatService;
        }

        protected override TransferViewModel OnBuild()
        {
            return new TransferViewModel()
            {
                Funds = GetFundsList(),
                VatCodes = GetVatList(),
            };
        }

        protected override TransferViewModel OnBuild(string criteria)
        {
            return new TransferViewModel()
            {
                Funds = GetFundsList(),
                VatCodes = GetVatList(),
            };
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
            var fundSelectListItems = new List<SelectListItem>();
            var funds = _fundService.GetAllFunds().OrderBy(x => x.FundName);

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
    }
}