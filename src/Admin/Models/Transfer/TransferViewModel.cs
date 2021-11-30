using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Models.Transfer
{
    [Serializable]
    public class TransferViewModel
    {
        public TransferItem SourceItem { get; set; }

        public TransferItem TransferItem { get; set; }

        public List<TransferItem> SourceItems { get; set; }

        public List<TransferItem> TransferItems { get; set; }

        public SelectList Funds { get; set; }
        public SelectList VatCodes { get; set; }

        public TransferViewModel()
        {
            TransferItems = new List<TransferItem>();
        }
    }
}