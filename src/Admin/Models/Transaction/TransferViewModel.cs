using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Models.Transaction
{
    [Serializable]
    public class TransferViewModel
    {
        public string PspReference { get; set; }
        public string TransactionReference { get; set; }

        public TransferItem TransferItem { get; set; }

        public List<TransferItem> TransferItems { get; set; }

        public SelectList Funds { get; set; }

        public SelectList VatCodes { get; set; }

        public TransferViewModel()
        {
            TransferItems = new List<TransferItem>();
        }
    }
}