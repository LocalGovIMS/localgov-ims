namespace Api.Controllers.Funds
{
    public class FundModel
    {
        public string FundCode { get; set; }

        public string FundName { get; set; }

        public bool UseGeneralLedgerCode { get; set; }

        public string GeneralLedgerCode { get; set; }

        public bool IsGeneralLedgerDetail { get; set; }

        public FundModel(BusinessLogic.Entities.Fund source)
        {
            FundCode = source.FundCode;
            FundName = source.FundName;
            UseGeneralLedgerCode = source.UseGeneralLedgerCode;
            GeneralLedgerCode = source.GeneralLedgerCode;
            IsGeneralLedgerDetail = source.LedgerDetail ?? false;
        }
    }
}