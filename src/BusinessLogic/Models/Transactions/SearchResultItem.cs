using System;

namespace BusinessLogic.Models.Transactions
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public string TransactionReference { get; set; }
        public string AccountReference { get; set; }
        public decimal? Amount { get; set; }
        public string FundCode { get; set; }
        public Entities.Fund Fund { get; set; }
        public string MopCode { get; set; }
        public Entities.Mop Mop { get; set; }
        public string Narrative { get; set; }
        public string InternalReference { get; set; }
        public string PspReference { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string AppReference { get; set; }
        public string OfficeCode { get; set; }
        public int UserCode { get; set; }
        public string VatCode { get; set; }
        public float VatRate { get; set; }
        public decimal? VatAmount { get; set; }
        public string CardHolderPremiseNumber { get; set; }
        public string CardHolderStreet { get; set; }
        public string CardHolderTown { get; set; }
        public string CardHolderPostCode { get; set; }
        public bool HasTransfers { get; set; }
        public int? ImportId { get; set; }
    }
}
