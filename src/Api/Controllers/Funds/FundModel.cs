using Api.Controllers.FundMetadata;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers.Funds
{
    public class FundModel
    {
        public string FundCode { get; set; }

        public string FundName { get; set; }

        public List<FundMetadataModel> Metadata { get; set; }

        public FundModel(BusinessLogic.Entities.Fund source)
        {
            FundCode = source.FundCode;
            FundName = source.FundName;
            Metadata = source.Metadata?.Select(x => new FundMetadataModel(x)).ToList();
        }
    }
}