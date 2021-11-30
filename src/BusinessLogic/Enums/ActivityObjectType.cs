namespace BusinessLogic.Enums
{
    public enum ActivityObjectType
    {
        // enums are given explicit values to bind to the activity log against the
        // correct event without being source order dependent
        FundGroup = 0,
        FundGroupFund = 1,
        Funds = 2,
        Mop = 3,
        Origins = 4,
        Role = 5,
        StopMessage = 6,
        TransactionNotifications = 7,
        PendingTransactions = 8,
        ProcessedTransactions = 9,
        UserFundGroup = 10,
        UserRole = 11,
        Users = 12,
        Vat = 13,
        Template = 14,
    }
}
