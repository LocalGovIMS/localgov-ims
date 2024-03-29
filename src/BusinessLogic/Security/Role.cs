﻿namespace BusinessLogic.Security
{
    public static class Role
    {
        public const string TransactionList = "Transaction.List";
        public const string TransactionDetails = "Transaction.Details";
        public const string TransactionRefund = "Transaction.Refund";
        public const string SuperUser = "SuperUser";
        public const string TransactionPartialRefund = "Transaction.PartialRefund";
        public const string TransactionCreate = "Transaction.Create";
        public const string RefundsList = "Refund.List";
        public const string TransactionJournal = "Transaction.Journal";
        public const string TransactionSave = "Transaction.Save";
        public const string SystemAdmin = "System.Admin";
        public const string Payments = "Payments";
        public const string Dashboard = "Dashboard";
        public const string ServiceDesk = "ServiceDesk";
        public const string ChequeProcess = "ChequeProcess";
        public const string Finance = "Finance";
        public const string PostPayment = "PostPayment";
        public const string Transfer = "Transfer";
        public const string Reporting = "Reporting";
        public const string EReturns = "EReturns";
        public const string EReturnAuthoriser = "EReturnAuthoriser";
        public const string EReturnDelete = "EReturn.Delete";

        // System roles
        public const string TransactionEdit = "Transaction.Edit"; // would be required if we use pay pal...here for completeness
        public const string RefundsAuthorise = "Refunds.Authorise";
    }
}