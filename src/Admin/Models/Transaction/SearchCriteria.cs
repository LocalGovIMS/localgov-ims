using Admin.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Transaction
{
    public class SearchCriteria
    {
        [Display(Name = "Account reference")]
        public string AccountReference { get; set; }

        public string AppReference { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Fund code")]
        public string FundCode { get; set; }

        [Display(Name = "User")]
        public int? UserId { get; set; }

        [Display(Name = "MOP code")]
        public string MopCode { get; set; }

        public string Narrative { get; set; }

        public string InternalReference { get; set; }

        [Display(Name = "Receipt number")]
        public string ReceiptNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public int? ImportId { get; set; }

        [Display(Name = "Card Prefix")]
        public string CardPrefix { get; set; }

        [Display(Name = "Card Suffix")]
        public string CardSuffix { get; set; }

        public int Page { get; set; }

        public SelectList Funds { get; set; }
        public SelectList Mops { get; set; }
        public SelectList Users { get; set; }

        public bool WildSearchAccountReference { get; set; }

        public int PageSize { get; set; } = 20;

        public bool IsForAnImport => ImportId.HasValue;

        public string SearchAction => IsForAnImport ? nameof(TransactionController.ListForTransactionImport) : nameof(TransactionController.Search);
    }
}