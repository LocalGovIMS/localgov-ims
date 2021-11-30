-- Indexes that we can't create using EF6 and a code first approach
IF NOT EXISTS (SELECT 1
    FROM sys.indexes AS si
    JOIN sys.objects AS so on si.object_id=so.object_id
    JOIN sys.schemas AS sc on so.schema_id=sc.schema_id
    WHERE
        so.name ='AccountHolders'
        AND si.name='IX_Search')
CREATE NONCLUSTERED INDEX [IX_Search] ON [dbo].[AccountHolders]
(
	AccountReference ASC, 
	FundCode ASC, 
	AddressLine1 ASC, 
	AddressLine2 ASC, 
	AddressLine3 ASC, 
	AddressLine4 ASC, 
	Postcode ASC, 
	Surname ASC
) 
INCLUDE 
(
	CurrentBalance, 
	Title, 
	Forename
) WITH (PAD_INDEX = OFF, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, SORT_IN_TEMPDB = OFF) ON [PRIMARY];
 
 IF NOT EXISTS (SELECT 1
    FROM sys.indexes AS si
    JOIN sys.objects AS so on si.object_id=so.object_id
    JOIN sys.schemas AS sc on so.schema_id=sc.schema_id
    WHERE
        so.name ='ProcessedTransactions'
        AND si.name='IX_TransferReference')
CREATE NONCLUSTERED INDEX [IX_TransferReference] ON [dbo].[ProcessedTransactions]
(
	TransferReference ASC
) 
INCLUDE 
(
	UserCode, 
	FundCode
) WITH (PAD_INDEX = OFF, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, SORT_IN_TEMPDB = OFF) ON [PRIMARY];

IF NOT EXISTS (SELECT 1
    FROM sys.indexes AS si
    JOIN sys.objects AS so on si.object_id=so.object_id
    JOIN sys.schemas AS sc on so.schema_id=sc.schema_id
    WHERE
        so.name ='ProcessedTransactions'
        AND si.name='IX_RefundReference')
CREATE NONCLUSTERED INDEX [IX_RefundReference] ON [dbo].[ProcessedTransactions] 
(
	[RefundReference] ASC
) 
INCLUDE
(
	[UserCode],
	[FundCode]
)
WHERE ([RefundReference] IS NOT NULL) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
