using Api.Controllers.FundMetadata;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers.Funds
{
    public class FundModel
    {
        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string DisplayName { get; set; }

        public string VatCode { get; set; }

        public decimal? MaximumAmount { get; set; }

        public bool OverPayAccount { get; set; }

        public bool AccountExist { get; set; }

        public bool AquireAddress { get; set; }

        public bool VatOverride { get; set; }

        public List<FundMetadataModel> Metadata { get; set; }

        public FundModel(BusinessLogic.Entities.Fund source)
        {
            FundCode = source.FundCode;
            FundName = source.FundName;
            DisplayName = source.DisplayName;
            VatCode = source.VatCode;
            MaximumAmount = source.MaximumAmount;
            OverPayAccount = source.OverPayAccount;
            AccountExist = source.AccountExist;
            AquireAddress = source.AquireAddress;
            VatOverride = source.VatOverride;

            Metadata = source.Metadata?.Select(x => new FundMetadataModel(x)).ToList();
        }
    }
}