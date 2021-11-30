MERGE INTO Offices AS [Target]
USING (SELECT * 
		FROM (VALUES ('S', 'System')) 
	AS S ([OfficeCode], [Name])) AS [Source]
ON [Target].[OfficeCode] = [Source].[OfficeCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([OfficeCode], [Name])
VALUES ([OfficeCode], [Name]);  

MERGE INTO Roles AS [Target]
USING (SELECT * 
		FROM (VALUES
			('Transaction.List', 'Transaction - List'),
			('Transaction.Details', 'Transaction - Details'),
			('Transaction.Refund', 'Transaction - Refund'),
			('SuperUser', 'Super User - Access to all data'),
			('Transaction.PartialRefund', 'Transaction - Partial Refund'),
			('Transaction.Create', 'Transaction - Create'),
			('Refund.List', 'Refund - List'),
			('Transaction.Journal', 'Transaction - Journal'),
			('Transaction.Save', 'Transaction - Save'),
			('System.Admin', 'System Administrator'),
			('Payments', 'Payments'),
			('Dashboard', 'Dashboard'),
			('ServiceDesk', 'ServiceDesk'),
			('ChequeProcess', 'Cheque Processer'),
			('Finance', 'Finance'),
			('EReturnAuthoriser', 'eReturn Authoriser'),
			('PostPayment', 'Post Payment'),
			('EReturns', 'EReturns'),
			('Transfer', 'Transfer'),
			('Reporting', 'Reporting')) 
	AS S ([Name], [DisplayName])) AS [Source]
ON [Target].[Name] = [Source].[Name] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [DisplayName])
VALUES ([Name], [DisplayName]);

SET IDENTITY_INSERT Users ON;
MERGE INTO Users AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(0, 'System/Import', GETDATE(), 90, 0, 'System/Import',	GETDATE(), NULL, 'S')) 
	AS S ([UserId], [UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])) AS [Source]
ON [Target].[UserId] = [Source].[UserId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserId], [UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])
VALUES ([UserId], [UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode]);
SET IDENTITY_INSERT Users OFF;

MERGE INTO TransactionStatus AS [Target]
USING (SELECT * 
		FROM (VALUES
			(1, 'Pending', 'Pending', 'Pending'),
			(2, 'Failed', 'Failed', 'Failed'),
			(3, 'Successful', 'Successful', 'Successful')) 
	AS S ([Id], [Name], [Description], [DisplayName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name], [Description], [DisplayName])
VALUES ([Id], [Name], [Description], [DisplayName]);

MERGE INTO EReturnStatus AS [Target]
USING (SELECT * 
		FROM (VALUES
			(1, 'New', 'New', 'New'),
			(2, 'InProgress', 'In Progress', 'In Progress'),
			(3, 'Submitted', 'Submitted', 'Submitted'),
			(4, 'Authorised', 'Authorised', 'Authorised'),
			(5, 'Voided', 'Voided', 'Voided'),
			(6, 'Deleted', 'Deleted', 'Deleted')) 
	AS S ([Id], [Name], [Description], [DisplayName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name], [Description], [DisplayName])
VALUES ([Id], [Name], [Description], [DisplayName]);

MERGE INTO EReturnTypes AS [Target]
USING (SELECT * 
		FROM (VALUES
			(1, 'Cash', 'Cash', 'Cash'),
			(2, 'Cheque', 'Cheque', 'Cheque')) 
	AS S ([Id], [Name], [Description], [DisplayName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name], [Description], [DisplayName])
VALUES ([Id], [Name], [Description], [DisplayName]);

MERGE INTO ImportProcessingRuleFields AS [Target]
USING (SELECT * 
		FROM (VALUES
			('AccountReference', 'Account Reference', 'Text', 1),
			('Amount', 'Amount', 'Text', 2),
			('FundCode', 'Fund Code', 'Text', 3),
			('Narrative', 'Narrative', 'Text', 4)) 
	AS S ([Name], [DisplayName], [Type], [DisplayOrder])) AS [Source]
ON [Target].[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [DisplayName], [Type], [DisplayOrder])
VALUES ([Name], [DisplayName], [Type], [DisplayOrder]);

MERGE INTO ImportProcessingRuleOperators AS [Target]
USING (SELECT * 
		FROM (VALUES
			('Contain', 'Contain', 'Text', 1),
			('NotContain', 'Not Contain', 'Text', 2),
			('StartWith', 'Start With', 'Text', 3),
			('EndWith', 'End With', 'Text', 4),
			('BeGreaterThan', 'Be Greater Than', 'Text', 5),
			('BeLessThan', 'Be Less Than', 'Text', 6),
			('Equals', 'Equal', 'Text', 7)) 
	AS S ([Name], [DisplayName], [Type], [DisplayOrder])) AS [Source]
ON [Target].[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [DisplayName], [Type], [DisplayOrder])
VALUES ([Name], [DisplayName], [Type], [DisplayOrder]);