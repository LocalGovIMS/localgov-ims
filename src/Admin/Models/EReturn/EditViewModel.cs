using Admin.Models.Shared;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;
using Web.Mvc.DataAnnotations;

namespace Admin.Models.EReturn
{
    public class EditViewModel
    {
        public EReturnWrapper EReturn { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
        public CashBreakdownViewModel Cash { get; set; }
        public List<ChequeViewModel> Cheques { get; set; }
        public SelectList VatCodes { get; set; }
        public string PspReference { get; set; }
        public Message Message { get; set; }
    }

    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public string VatCode { get; set; }
        public int TemplateRowId { get; set; }
    }

    public class CashBreakdownViewModel
    {
        public int Id { get; set; }
        public string BagNumber { get; set; }

        [CheckDenominationValue(50.00)]
        public decimal? Pounds50 { get; set; }

        [CheckDenominationValue(20.00)]
        public decimal? Pounds20 { get; set; }

        [CheckDenominationValue(10.00)]
        public decimal? Pounds10 { get; set; }

        [CheckDenominationValue(5.00)]
        public decimal? Pounds5 { get; set; }

        [CheckDenominationValue(2.00)]
        public decimal? Pounds2 { get; set; }

        [CheckDenominationValue(1.00)]
        public decimal? Pounds1 { get; set; }

        [CheckDenominationValue(00.50)]
        public decimal? Pence50 { get; set; }

        [CheckDenominationValue(00.20)]
        public decimal? Pence20 { get; set; }

        [CheckDenominationValue(00.10)]
        public decimal? Pence10 { get; set; }

        [CheckDenominationValue(00.05)]
        public decimal? Pence5 { get; set; }

        [CheckDenominationValue(00.02)]
        public decimal? Pence2 { get; set; }

        [CheckDenominationValue(00.01)]
        public decimal? Pence1 { get; set; }

        public decimal? Total { get; set; }
    }

    public class ChequeViewModel
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}