MERGE INTO Offices AS [Target]
USING (SELECT * 
		FROM (VALUES
			('99', 'Batch Import'),
			('PR', 'Post Room'),
			('SP', 'Smart Pay'),
			('WP', 'Parking')) 
	AS S ([OfficeCode], [Name])) AS [Source]
ON [Target].[OfficeCode] = [Source].[OfficeCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([OfficeCode], [Name])
VALUES ([OfficeCode], [Name]);    

DECLARE @User1Id INT = NULL;
DECLARE @User1Username VARCHAR(100) = '[[DBInitialiser.User1.Username]]';
MERGE INTO Users AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(@User1Username, GETDATE(), 90, 0, '[[DBInitialiser.User1.Name]]', GETDATE(), NULL, 'SP')) 
	AS S ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])) AS [Source]
ON [Target].[UserName] = [Source].[UserName] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])
VALUES ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode]);
SELECT @User1Id = UserId FROM Users WHERE UserName = @User1Username;

MERGE INTO UserRoles AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@User1Id, 1),
			(@User1Id, 2),
			(@User1Id, 3),
			(@User1Id, 4),
			(@User1Id, 5),
			(@User1Id, 6),
			(@User1Id, 7),
			(@User1Id, 8),
			(@User1Id, 9),
			(@User1Id, 10),
			(@User1Id, 11),
			(@User1Id, 12),
			(@User1Id, 13),
			(@User1Id, 14),
			(@User1Id, 15),
			(@User1Id, 16),
			(@User1Id, 17),
			(@User1Id, 18),
			(@User1Id, 19),
			(@User1Id, 20),
			(@User1Id, 21)) 
	AS S ([UserId], [RoleId])) AS [Source]
ON [Target].[UserId] = [Source].[UserId] 
	AND [Target].[RoleId] = [Source].[RoleId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserId], [RoleId])
VALUES ([UserId], [RoleId]);

DECLARE @User2Id INT = NULL;
DECLARE @User2Username VARCHAR(100) = '[[DBInitialiser.User2.Username]]';
MERGE INTO Users AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(@User2Username, GETDATE(), 90, 0, '[[DBInitialiser.User2.Name]]',	GETDATE(), NULL, 'SP')) 
	AS S ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])) AS [Source]
ON [Target].[UserName] = [Source].[UserName] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode])
VALUES ([UserName], [LastLogin], [ExpiryDays], [Disabled], [DisplayName], [CreatedAt], [LastEnabledAt], [OfficeCode]);
SELECT @User2Id = UserId FROM Users WHERE UserName = @User2Username;

MERGE INTO UserRoles AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@User2Id, 1),
			(@User2Id, 2),
			(@User2Id, 3),
			(@User2Id, 4),
			(@User2Id, 5),
			(@User2Id, 6),
			(@User2Id, 7),
			(@User2Id, 8),
			(@User2Id, 9),
			(@User2Id, 10),
			(@User2Id, 11),
			(@User2Id, 12),
			(@User2Id, 13),
			(@User2Id, 14),
			(@User2Id, 15),
			(@User2Id, 16),
			(@User2Id, 17),
			(@User2Id, 18),
			(@User2Id, 19),
			(@User2Id, 20),
			(@User2Id, 21)) 
	AS S ([UserId], [RoleId])) AS [Source]
ON [Target].[UserId] = [Source].[UserId] 
	AND [Target].[RoleId] = [Source].[RoleId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserId], [RoleId])
VALUES ([UserId], [RoleId]);

MERGE INTO Mops AS [Target]
USING (SELECT * 
		FROM (VALUES
			('2', 'Cheque', 0, 0, 0),
			('24', 'Cheque -Admin/Postal', 999999999, 0, 0),
			('JN', 'Journal', 999999999, 0, 0),
			('JR', 'Journal Re-allocation', 999999999, 0, 0),
			('1', 'Cash', 99999999.99, 0, 0),
			('10', 'Transfer In', 999999999.99, 0, 0),
			('11', 'Transfer Out', 999999999.99, 0, 0),
			('15', 'All Pay - Post Office', 99999.99, 0, 0),
			('16', 'All Pay - Cash Adjustment', 99999.99, 0, 0),
			('17', 'All Pay - Returned Cheque', 99999.99, 0, 0),
			('18', 'All Pay - Pay Point', 99999.99, 0, 0),
			('19', 'All Pay - Payzone', 99999.99, 0, 0),
			('20', 'All Pay - IVR Desktop', 99999.99, 0, 0),
			('21', 'All Pay - Bills Online', 99999.99, 0, 0),
			('28', 'Interfaces', 999999999, 0, 0),
			('90', 'SmartPay SelfService cards', 999999999, 0, 0),
			('91', 'SmartPay ATP cards', 999999999, 0, 0),
			('92', 'SmartPay Staff cards', 999999999, 0, 0)) 
	AS S ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled])) AS [Source]
ON [Target].[MopCode] = [Source].[MopCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled])
VALUES ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled]);

MERGE INTO MopMetaData AS [Target]
USING (SELECT * 
		FROM (VALUES
			('IsAJournal', 'TRUE', 'JN'),
			('IsAJournalReallocation', 'TRUE', 'JR'),
			('IsATransferOut', 'TRUE', '11'),
			('IsACardSelfServicePayment', 'TRUE', '90'),
			('IsRefundable', 'TRUE', '90'),
			('IsRefundable', 'TRUE', '91'),
			('IsRefundable', 'TRUE', '92'),
			('IsATransferIn', 'TRUE', '10'),
			('IsACardAtpPayment', 'TRUE', '91'),
			('IsAChequePayment', 'TRUE', '24'),
			('IsACardViaStaffPayment', 'TRUE', '92'),
			('IsACashPayment', 'TRUE', '1'),
			('IsAnEReturnChequePayment', 'TRUE', '2'),
			('PaymentIntegrationId', '1', '90'),
			('BackgroundColour', '#21ba45', 'JN'),
			('BackgroundColour', '#21ba45', 'JR'),
			('BackgroundColour', '#b5cc18', '10'),
			('BackgroundColour', '#b5cc18', '11'),
			('BackgroundColour', '#a333c8', '28'),
			('BackgroundColour', '#fbbd08', '15'),
			('BackgroundColour', '#fbbd08', '16'),
			('BackgroundColour', '#fbbd08', '17'),
			('BackgroundColour', '#fbbd08', '18'),
			('BackgroundColour', '#fbbd08', '19'),
			('BackgroundColour', '#fbbd08', '20'),
			('BackgroundColour', '#fbbd08', '21'),
			('BackgroundColour', '#2185d0', '90'),
			('BackgroundColour', '#2185d0', '91'),
			('BackgroundColour', '#2185d0', '92'),
			('BackgroundColour', '#a5673f', '24'),
			('TextColour', '#fff', 'JN'),
			('TextColour', '#fff', 'JR'),
			('TextColour', '#fff', '10'),
			('TextColour', '#fff', '11'),
			('TextColour', '#fff', '28'),
			('TextColour', '#fff', '15'),
			('TextColour', '#fff', '16'),
			('TextColour', '#fff', '17'),
			('TextColour', '#fff', '18'),
			('TextColour', '#fff', '19'),
			('TextColour', '#fff', '20'),
			('TextColour', '#fff', '21'),
			('TextColour', '#fff', '90'),
			('TextColour', '#fff', '91'),
			('TextColour', '#fff', '92'),
			('TextColour', '#fff', '24')) 
	AS S ([Key], [Value], [MopCode])) AS [Source]
ON [Target].[MopCode] = [Source].[MopCode] 
	AND [Target].[Key] = [Source].[Key] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Key], [Value], [MopCode])
VALUES ([Key], [Value], [MopCode]);

MERGE INTO Vat AS [Target]
USING (SELECT * 
		FROM (VALUES
			('E0', 0, 0),
			('M0', 0, 0),
			('N0', 0, 0),
			('11', 0, 0),
			('W0', 0.2, 0)) 
	AS S ([VatCode], [Percentage], [Disabled])) AS [Source]
ON [Target].[VatCode] = [Source].[VatCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([VatCode], [Percentage], [Disabled])
VALUES ([VatCode], [Percentage], [Disabled]);

MERGE INTO VatMetaData AS [Target]
USING (SELECT * 
		FROM (VALUES
			('IsASuspenseJournalVatCode', 'True', 'M0')) 
	AS S ([Key], [Value], [VatCode])) AS [Source]
ON [Target].[VatCode] = [Source].[VatCode] 
	AND [Target].[Key] = [Source].[Key] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Key], [Value], [VatCode])
VALUES ([Key], [Value], [VatCode]);

MERGE INTO Funds AS [Target]
USING (SELECT * 
		FROM (VALUES
			('5M', 'Library Charges', '5', '5M', 'N0', 100, 1, 0, 1, '.', 1, 72100710220, 0, 0, 1, 'Library Charges', 0, NULL, 0),
			('52', 'Market Stall Rental', '5', '05D', 'E0', 5000, 1, 0, 1, NULL, 1, 72200850079, 0, 0, 1, 'Market Stall Rental', 0, 1, 0),
			('2R', 'Fixed Penalty Notice', '5', '2R', 'N0', 500, 0, 1, 1, 'E02R', 1, 72100021754, 1, 0, 1, 'Fixed Penalty Notice starting FP', 0, 0, 0),
			('3U', 'Landlord Accreditation Service', '5', '3U', 'W0', 99.99, 0, 0, 1, NULL, 1, 72101580438, 0, 0, 1, 'Landlord Accreditation Service', 0, 1, 0),
			('1Z', 'HMO Premises Licence', '5', '0', 'N0', 99999999, 1, 0, 1, NULL, 1, 72101920227, 0, 0, 1, NULL, 0, 1, 0),
			('ZZ', 'Not Authorised Cards', '20', '0', 'N0', 99999999, 0, 0, 0, NULL, 0, NULL, 0, 0, 1, NULL, 0, 0, 0),
			('1', 'Bank Suspense', '12', '0', 'M0', 99999999.99, 1, 0, 1, 'E930', 1, 964024, 1, 0, 1, NULL, 0, 1, 0),			
			('5', 'Housing Rents', '5', '5', 'N0', 99999.99, 0, 1, 1, 'E005', 1, 964104, 1, 1, 1, 'Housing Rents', 0, 0, 0),
			('13', 'Misc Cash', '5', '13', 'W0', 999999999.99, 1, 0, 1, NULL, 0, NULL, 0, 0, 1, NULL, 1, 1, 0),
			('SP', 'Suspense', '5', '0', 'M0', 99999999, 1, 0, 1, NULL, 1, 964023, 0, 0, 1, NULL, 0, 1, 0),
			('XT', 'Transfers', '12', '0', 'N0', 99999999, 0, 0, 0, NULL, 0, NULL, 0, 0, 1, NULL, 0, 0, 0),
			('19', 'SAP Invoices', '5', '19', '11', 99999999, 0, 1, 0, 'E060', 0, NULL, 1, 1, 1, 'Invoices Starting 3 or 9', 0, 0, 0),
			('20', 'BCT Invoices', '5', '20', '11', 99999999, 0, 1, 0, 'E060', 0, NULL, 1, 1, 1, 'Invoices Starting M or P', 0, 0, 0),
			('11', 'Parking Fines', '5', '11', 'N0', 99999999, 0, 1, 1, 'E011', 1, 72104021624, 1, 0, 1, 'Parking Fines starting BJ', 0, 0, 0),
			('24', 'Business Rates', '5', '22', 'N0', 9999999.99, 0, 1, 1, 'E902', 1, 964102, 1, 1, 1, 'Business Rates', 0, 0, 0),
			('23', 'Council Tax', '5', '22', 'N0', 999999.99, 1, 1, 1, 'E902', 1, 964101, 1, 1, 1, 'Council Tax', 0, 0, 0),
			('25', 'Benefit Overpayments', '5', '25', 'N0', 9999.99, 0, 1, 1, 'E925', 1, 72320550287, 1, 1, 1, 'Benefit Overpayments', 0, 0, 0),
			('32', 'Developmnet Control', '5', '31', 'N0', 99999, 1, 0, 1, '.', 1, 72101750163, 0, 0, 1, 'Planning Fees', 0, 1, 0),
			('31', 'Building Control', '5', '31', 'W0', 99999, 1, 0, 1, '.', 1, 72101750164, 0, 0, 1, 'Building Control Fees', 0, 1, 0))
	AS S ([FundCode], [FundName], [AccessLevel], [ValidationReference], [VatCode], [MaximumAmount], [NarrativeFlag], [ExportToFund], [ExportToLedger], [FundExportFormat], [UseGeneralLedgerCode], [GeneralLedgerCode], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [LedgerDetail], [Disabled])) AS [Source]
ON [Target].[FundCode] = [Source].[FundCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([FundCode], [FundName], [AccessLevel], [ValidationReference], [VatCode], [MaximumAmount], [NarrativeFlag], [ExportToFund], [ExportToLedger], [FundExportFormat], [UseGeneralLedgerCode], [GeneralLedgerCode], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [LedgerDetail], [Disabled])
VALUES ([FundCode], [FundName], [AccessLevel], [ValidationReference], [VatCode], [MaximumAmount], [NarrativeFlag], [ExportToFund], [ExportToLedger], [FundExportFormat], [UseGeneralLedgerCode], [GeneralLedgerCode], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [LedgerDetail], [Disabled]);

MERGE INTO FundMetaData AS [Target]
USING (SELECT * 
		FROM (VALUES
			('IsACreditNoteEnabledFund', 'True', '19'),
			('IsACreditNoteEnabledFund', 'True', '20'),
			('IsACreditNoteEnabledFund', 'True', '5'),
			('IsACreditNoteEnabledFund', 'True', '23'),
			('IsACreditNoteEnabledFund', 'True', '24'),
			('IsACreditNoteEnabledFund', 'True', '25'),
			('IsACreditNoteEnabledFund', 'True', '13'),
			('IsACreditNoteEnabledFund', 'True', '11'),
			('IsAnEReturnDefaultFund', 'True', '13'),
			('IsASuspenseJournalFund', 'True', '1'),
			('IsABasketFund', 'True', '25'),
			('Basket.ReferenceFieldLabel', 'Benefit overpayment reference', '25'),
			('IsABasketFund', 'True', '19'),
			('Basket.ReferenceFieldLabel', 'Invoice reference', '19'),
			('IsABasketFund', 'True', '20'),
			('Basket.ReferenceFieldLabel', 'Invoice reference', '20'),
			('IsABasketFund', 'True', '24'),
			('Basket.ReferenceFieldLabel', 'Business rates reference', '24'),
			('IsABasketFund', 'True', '23'),
			('Basket.ReferenceFieldLabel', 'Council Tax reference', '23'),
			('IsABasketFund', 'True', '11'),
			('Basket.ReferenceFieldLabel', 'Parking fine reference', '11'),
			('IsABasketFund', 'True', '2R'),
			('Basket.ReferenceFieldLabel', 'Fixed penalty notice reference', '2R'),
			('IsABasketFund', 'True', '3U'),
			('Basket.ReferenceFieldLabel', 'Landlord accreditation service reference', '3U'),
			('IsABasketFund', 'True', '5M'),
			('Basket.ReferenceFieldLabel', 'Library card reference', '5M'),
			('Basket.ReferenceFieldMessage', 'If the charge is on a child''s card please enter the child''s reference', '5M'),
			('IsABasketFund', 'True', '31'),
			('Basket.ReferenceFieldLabel', 'Parking fine reference', '31'),
			('IsABasketFund', 'True', '32'),
			('Basket.ReferenceFieldLabel', 'Parking fine reference', '32'),
			('IsABasketFund', 'True', '52'),
			('Basket.ReferenceFieldLabel', 'Traders reference and surname', '52'),
			('Basket.ReferenceFieldMessage', 'Please enter your 4 digit reference followed by your surname', '52'),
			('IsABasketFund', 'True', '5'),
			('Basket.ReferenceFieldLabel', 'Housing rents reference', '5')) 
	AS S ([Key], [Value], [FundCode])) AS [Source]
ON [Target].[FundCode] = [Source].[FundCode] 
	AND [Target].[Key] = [Source].[Key] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Key], [Value], [FundCode])
VALUES ([Key], [Value], [FundCode]);

MERGE INTO AccountValidations AS [Target]
USING (SELECT * 
		FROM (VALUES
			('0', 'Open Validation', '9', '0', '******************************', 1, 30, 0, NULL, 0),
			('5', 'Housing Rents', '10', '0', '##########@', 11, 11, 1, NULL, 0),
			('13', 'Miscellaneous Income', '9', '0', '###########', 11, 11, 0, NULL, 0),
			('19', 'SAP Debtors', '9', '0', '##########', 10, 10, 0, NULL, 0),
			('20', 'Old Debtors', '9', '0', '?##########', 11, 11, 0, NULL, 0),
			('11', 'Parking Fines', '11', 'A', 'BJ#######@', 10, 10, 0, '05', 0),
			('22', 'Academy CTAX/NNDR', '10', '0', '########@', 9, 9, 1, NULL, 0),
			('25', 'Benefit Overpayments', '9', '0', '########', 8, 8, 0, NULL, 0),
			('2R', 'Fixed Penalty Notice', '11', 'A', 'FP#######@', 10, 10, 0, '12', 0),
			('3U', 'Landlord accredit', '9', '0', 'ALS/####/####', 13, 13, 0, NULL, 0),
			('5M', 'Library Charges', 'OE', '0', '#############@', 14, 14, 0, NULL, 0),
			('31', 'Building Control', '10', '0', '******************************', 6, 30, 0, NULL, 1),
			('05D', 'Market stalls', '10', '0', '#*****************************', 5, 30, 0, NULL, 1)) 
	AS S ([ValidationReference], [Name], [Modulus], [TenConversion], [InputMask], [MinLength], [MaxLength], [SubtractFlag], [CheckDigitCalcAlphaReplace], [CanNotBeNumeric])) AS [Source]
ON [Target].[ValidationReference] = [Source].[ValidationReference]
	AND [Target].[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ValidationReference], [Name], [Modulus], [TenConversion], [InputMask], [MinLength], [MaxLength], [SubtractFlag], [CheckDigitCalcAlphaReplace], [CanNotBeNumeric])
VALUES ([ValidationReference], [Name], [Modulus], [TenConversion], [InputMask], [MinLength], [MaxLength], [SubtractFlag], [CheckDigitCalcAlphaReplace], [CanNotBeNumeric]);

MERGE INTO AccountValidationWeightings AS [Target]
USING (SELECT * 
		FROM (VALUES
			('5', 0, 0, 9, 7, 1, 4, 6, 3, 2, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
			('11', 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1),
			('22', 3, 7, 1, 3, 7, 1, 3, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
			('2R', 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
			('5M', 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)) 
	AS S ([ValidationReference], [Digit1Weighting], [Digit2Weighting], [Digit3Weighting], [Digit4Weighting], [Digit5Weighting], [Digit6Weighting], [Digit7Weighting], [Digit8Weighting], [Digit9Weighting], [Digit10Weighting], [Digit11Weighting], [Digit12Weighting], [Digit13Weighting], [Digit14Weighting], [Digit15Weighting], [Digit16Weighting], [Digit17Weighting], [Digit18Weighting], [Digit19Weighting], [Digit20Weighting], [Digit21Weighting], [Digit22Weighting], [Digit23Weighting], [Digit24Weighting], [Digit25Weighting], [Digit26Weighting], [Digit27Weighting], [Digit28Weighting], [Digit29Weighting], [Digit30Weighting])) AS [Source]
ON [Target].[ValidationReference] = [Source].[ValidationReference]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ValidationReference], [Digit1Weighting], [Digit2Weighting], [Digit3Weighting], [Digit4Weighting], [Digit5Weighting], [Digit6Weighting], [Digit7Weighting], [Digit8Weighting], [Digit9Weighting], [Digit10Weighting], [Digit11Weighting], [Digit12Weighting], [Digit13Weighting], [Digit14Weighting], [Digit15Weighting], [Digit16Weighting], [Digit17Weighting], [Digit18Weighting], [Digit19Weighting], [Digit20Weighting], [Digit21Weighting], [Digit22Weighting], [Digit23Weighting], [Digit24Weighting], [Digit25Weighting], [Digit26Weighting], [Digit27Weighting], [Digit28Weighting], [Digit29Weighting], [Digit30Weighting])
VALUES ([ValidationReference], [Digit1Weighting], [Digit2Weighting], [Digit3Weighting], [Digit4Weighting], [Digit5Weighting], [Digit6Weighting], [Digit7Weighting], [Digit8Weighting], [Digit9Weighting], [Digit10Weighting], [Digit11Weighting], [Digit12Weighting], [Digit13Weighting], [Digit14Weighting], [Digit15Weighting], [Digit16Weighting], [Digit17Weighting], [Digit18Weighting], [Digit19Weighting], [Digit20Weighting], [Digit21Weighting], [Digit22Weighting], [Digit23Weighting], [Digit24Weighting], [Digit25Weighting], [Digit26Weighting], [Digit27Weighting], [Digit28Weighting], [Digit29Weighting], [Digit30Weighting]);

SET IDENTITY_INSERT PaymentIntegrations ON;
MERGE INTO PaymentIntegrations AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(1, '[[DBInitialiser.PaymentIntegration.Name]]', '[[DBInitialiser.PaymentIntegration.BaseUri]]')) 
	AS S ([Id], [Name], [BaseUri])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name], [BaseUri])
VALUES ([Id], [Name], [BaseUri]);
SET IDENTITY_INSERT PaymentIntegrations OFF;

MERGE INTO Templates AS [Target]
USING (SELECT * 
		FROM (VALUES
			('Brokerage and support', 0, 0, 0, 1, 1, 0),
			('Car Parks', 0, 0, 0, 0, 1, 0),
			('Great Hall Car Park', 0, 0, 0, 0, 1, 0)) 
	AS S ([Name], [ModifyVat], [ModifyReference], [ModifyDescription], [Cheque], [Cash], [Pdq])) AS [Source]
ON [Target].[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [ModifyVat], [ModifyReference], [ModifyDescription], [Cheque], [Cash], [Pdq])
VALUES ([Name], [ModifyVat], [ModifyReference], [ModifyDescription], [Cheque], [Cash], [Pdq]);

DECLARE @TemplateId1 INT = 0;
SELECT @TemplateId1 = Id FROM Templates WHERE [Name] = 'Brokerage and support';

DECLARE @TemplateId2 INT = 0;
SELECT @TemplateId2 = Id FROM Templates WHERE [Name] = 'Car Parks';

DECLARE @TemplateId3 INT = 0;
SELECT @TemplateId3 = Id FROM Templates WHERE [Name] = 'Great Hall Car Park';

MERGE INTO TemplateRows AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@TemplateId1, '82300630613', 'N0', 'Returned IB money Children Dis', 0, 0, 0),
			(@TemplateId1, '82300630348', 'N0', 'Returned IB money MentalHealth', 0, 0, 0),
			(@TemplateId1, '82300630622', 'N0', 'Returned IB monies SP', 0, 0, 0),
			(@TemplateId1, '82300630744', 'N0', 'Sensory Support 65+ East', 0, 0, 0),
			(@TemplateId1, '82300630789', 'N0', 'MH 18 to 64 MH', 0, 0, 0),
			(@TemplateId1, '82300630732', 'N0', 'Return IB Monies West', 0, 0, 0),
			(@TemplateId1, '82300630752', 'N0', 'Returned IB monies East', 0, 0, 0),
			(@TemplateId1, '82300640750', 'N0', 'North and North East', 0, 0, 0),
			(@TemplateId1, '82300535062', 'N0', 'South', 0, 0, 0),
			(@TemplateId2, '87004025312', 'W0', 'Multi Storey', 0, 0, 0),
			(@TemplateId2, '87004025333', 'W0', 'Lambra Road', 0, 0, 0),
			(@TemplateId2, '87004025342', 'W0', 'High Street', 0, 0, 0),
			(@TemplateId2, '87004025342', 'W0', 'Sackville', 0, 0, 0),
			(@TemplateId2, '87004025344', 'W0', 'Mark Street', 0, 0, 0),
			(@TemplateId2, '87004025343', 'W0', 'Churchfields', 0, 0, 0),
			(@TemplateId2, '87004025345', 'W0', 'Court House', 0, 0, 0),
			(@TemplateId2, '87004025338', 'W0', 'Burleigh Street', 0, 0, 0),
			(@TemplateId2, '87004025332', 'W0', 'John Street', 0, 0, 0),
			(@TemplateId2, '87004025339', 'W0', 'Pitt Street', 0, 0, 0),
			(@TemplateId2, '87004025334', 'W0', 'Wellington House', 0, 0, 0),
			(@TemplateId2, '87004025331', 'W0', 'Market Gate Car Park', 0, 0, 0),
			(@TemplateId2, '87004025347', 'W0', 'Pipers Cottage', 0, 0, 0),
			(@TemplateId2, '87004025335', 'W0', 'Lancaster Gate', 0, 0, 0),
			(@TemplateId2, '87004025336', 'W0', 'St Marys Place', 0, 0, 0),
			(@TemplateId2, '87004025337', 'W0', 'Grahams Orchard', 0, 0, 0),
			(@TemplateId2, '87004050575', 'W0', 'West Road Pogmoor', 0, 0, 0),
			(@TemplateId2, '87004021504', 'N0', 'On-Street Parking', 0, 0, 0),
			(@TemplateId3, '87004050031', 'W0', 'Car Park 1', 0, 0, 0),
			(@TemplateId3, '87004050031', 'W0', 'Car Park 2', 0, 0, 0)) 
	AS S ([TemplateId], [Reference], [VatCode], [Description], [VatOverride], [ReferenceOverride], [DescriptionOverride])) AS [Source]
ON [Target].[TemplateId] = [Source].[TemplateId]
	AND [Target].[Reference] = [Source].[Reference]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([TemplateId], [Reference], [VatCode], [Description], [VatOverride], [ReferenceOverride], [DescriptionOverride])
VALUES ([TemplateId], [Reference], [VatCode], [Description], [VatOverride], [ReferenceOverride], [DescriptionOverride]);

MERGE INTO StopMessages AS [Target]
USING (SELECT * 
		FROM (VALUES
			('62', '5', 'Squatter'),
			('63', '5', 'Do not accept payment - COURT'),
			('64', '5', 'Accept Payment - Refer for Int'),
			('65', '5', 'Do Not Accept CHEQUES'),
			('66', '5', 'Do Not Accept Payment - Refer to Berneslai Homes'),
			('67', '5', 'Do Not Accept Payment - BAILIFF. Refer to Berneslai Homes.'),
			('66', '11', 'Payment should only be accepted after consultation with Parking Services.'))
	AS S ([Id], [FundCode], [Message])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [FundCode], [Message])
VALUES ([Id], [FundCode], [Message]);

BEGIN TRAN

DECLARE @UserId INT = 1;

DECLARE @EReturnType_Cash INT = 1;
DECLARE @EReturnType_Cheque INT = 2;

DECLARE @EReturnStatus_New INT = 1;
DECLARE @EReturnStatus_InProgress INT = 2;
DECLARE @EReturnStatus_Submitted INT = 3;
DECLARE @EReturnStatus_Authorised INT = 4;
DECLARE @EReturnStatus_Voided INT = 5;
DECLARE @EReturnStatus_Deleted INT = 6;

DECLARE @EReturnId INT = 0;

INSERT INTO EReturns ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId]) VALUES ('R1',	NULL, NULL,	@EReturnType_Cash, @EReturnStatus_InProgress, 1, GETDATE(), @UserId, NULL, NULL, NULL)
SELECT @EReturnId = @@IDENTITY;

INSERT INTO EReturnCash ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total]) VALUES (@EReturnId, GETDATE(), '109-5407-8453-7', 0.00, 100.00, 40.00, 5.00, 4.00, 4.00, 0.50, 1.00, 0.50, NULL, NULL, NULL, 155.00)

INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('mjyRT3WPPZ', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530692, @UserId, '13', '1', 100, 0, 'N0', 'Returned IB money Children Dis', 'NULL', 0, @EReturnId, 1, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('q14NUGpk2A', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530359, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB money MentalHealth', 'NULL', 0, @EReturnId, 2, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('kQ24u4ZQEn', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530621, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB monies SP', 'NULL', 0, @EReturnId, 3, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('2rNGCVrrb', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530844, @UserId, '13', '1', 55, 0, 'N0', 'Sensory Support 65+ East', 'NULL', 0, @EReturnId, 4, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Ov7Dcl7JW5', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530889, @UserId, '13', '1', NULL, 0, 'N0', 'MH 18 to 64 MH', 'NULL', 0, @EReturnId, 5, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('RyGEiol4ZV', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530832, @UserId, '13', '1', NULL, 0, 'N0', 'Return IB Monies West', 'NULL', 0, @EReturnId, 6, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('w2jMhQB2GE', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530852, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB monies East', 'NULL', 0, @EReturnId, 7, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('y6d2TkdXJ', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530861, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB money Disabilities', 'NULL', 0, @EReturnId, 8, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('pDoMUdbxb', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530867, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB monies Transitions', 'NULL', 0, @EReturnId, 9, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('op1jc4OZG3', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530840, @UserId, '13', '1', NULL, 0, 'N0', 'MH 65+ West Team', 'NULL', 0, @EReturnId, 10, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('opPKi41Wz5', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530856, @UserId, '13', '1', NULL, 0, 'N0', 'MH 65+ East Team', 'NULL', 0, @EReturnId, 11, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('XRqNS7jbWV', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530842, @UserId, '13', '1', NULL, 0, 'N0', 'Physical Support 65+ East Team', 'NULL', 0, @EReturnId, 12, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('5vbnizonx0', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530868, @UserId, '13', '1', NULL, 0, 'N0', 'Learning Disab 65+', 'NULL', 0, @EReturnId, 13, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Pvl7uDqEl', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530836, @UserId, '13', '1', NULL, 0, 'N0', 'Memory & Cogn 65+(West Team)', 'NULL', 0, @EReturnId, 14, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('83Nbf6b5or', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530152, @UserId, '13', '1', NULL, 0, 'N0', 'Social Support', 'NULL', 0, @EReturnId, 15, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('pDnVHyxy0o', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530251, @UserId, '13', '1', NULL, 0, 'N0', 'Personal Health Budget', 'NULL', 0, @EReturnId, 16, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('QOR0F5Dz5', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300530860, @UserId, '13', '1', NULL, 0, 'N0', 'Mental Health 65+ (East Team)', 'NULL', 0, @EReturnId, 17, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('E5PZCd5O8P', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300535075, @UserId, '13', '1', NULL, 0, 'N0', 'specialist team north ld 18-64 ', 'NULL', 0, @EReturnId, 18, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('XRqXF79Ro1', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300535089, @UserId, '13', '1', NULL, 0, 'N0', 'specialist team south ld 18-64', 'NULL', 0, @EReturnId, 19, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('opE6h4awvV', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300535054, @UserId, '13', '1', NULL, 0, 'N0', 'Central and Penistone', 'NULL', 0, @EReturnId, 20, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Aq2RF49lp', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300540850, @UserId, '13', '1', NULL, 0, 'N0', 'North and North East', 'NULL', 0, @EReturnId, 21, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('M2N9IAQyBo', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300540853, @UserId, '13', '1', NULL, 0, 'N0', 'North and North East', 'NULL', 0, @EReturnId, 22, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('B4GEIxx11v', 'q1bWFGKEWb', GETDATE(), GETDATE(), 72300535062, @UserId, '13', '1', NULL, 0, 'N0', 'South and Dearne', 'NULL', 0, @EReturnId, 23, 0)

INSERT INTO EReturns ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId]) VALUES ('R2',	NULL, NULL,	@EReturnType_Cash, @EReturnStatus_InProgress, 2, GETDATE(), 1, NULL, NULL, NULL)
SELECT @EReturnId = @@IDENTITY;

INSERT INTO EReturnCash ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total]) VALUES (@EReturnId, GETDATE(), '121-3443-9896-5', 0.00, 200.00, 340.00, 85.00, 3856.00, 859.00, 90.50, 90.40, 36.60, 7.25, 0.18, 0.07, 5565.00)

INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('jkQ7UWMB7K', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021312, @UserId, '13', '1', 2456.5, 0, 'W0', 'Multi Storey', 'NULL', 0.2, @EReturnId, 24, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('gq1x2Fvqn', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021346, @UserId, '13', '1', 12, 0, 'W0', 'Phase 111', 'NULL', 0.2, @EReturnId, 25, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('bm25iJBpk', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021333, @UserId, '13', '1', 45, 0, 'W0', 'Lambra Road', 'NULL', 0.2, @EReturnId, 26, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('JmEms4wWDp', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021342, @UserId, '13', '1', 456.9, 0, 'W0', 'High Street', 'NULL', 0.2, @EReturnId, 27, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('op3pS4pO3n', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021342, @UserId, '13', '1', 471.1, 0, 'W0', 'Sackville', 'NULL', 0.2, @EReturnId, 28, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('dPJPUZ9l9', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021344, @UserId, '13', '1', 33, 0, 'W0', 'Mark Street', 'NULL', 0.2, @EReturnId, 29, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('0GXGFGpZdl', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021343, @UserId, '13', '1', 63.2, 0, 'W0', 'Churchfields', 'NULL', 0.2, @EReturnId, 30, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('q1M1SGJPm8', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021345, @UserId, '13', '1', 3.2, 0, 'W0', 'Court House', 'NULL', 0.2, @EReturnId, 31, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('7zwduRpxQ7', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021338, @UserId, '13', '1', 1.5, 0, 'W0', 'Burleigh Street', 'NULL', 0.2, @EReturnId, 32, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('w2MZHJZAa', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021332, @UserId, '13', '1', 1.8, 0, 'W0', 'John Street', 'NULL', 0.2, @EReturnId, 33, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('aRnZCrJxq7', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021339, @UserId, '13', '1', 0.9, 0, 'W0', 'Pitt Street', 'NULL', 0.2, @EReturnId, 34, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Aqd9CdQZjq', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021334, @UserId, '13', '1', 44.5, 0, 'W0', 'Wellington House', 'NULL', 0.2, @EReturnId, 35, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('3DAphx74yy', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021331, @UserId, '13', '1', 789.5, 0, 'W0', 'Market Gate Car Park', 'NULL', 0.2, @EReturnId, 36, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('69vbIxa4vZ', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021347, @UserId, '13', '1', 32.5, 0, 'W0', 'Pipers Cottage', 'NULL', 0.2, @EReturnId, 37, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('npO7Hql2k', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021335, @UserId, '13', '1', 94.8, 0, 'W0', 'Lancaster Gate', 'NULL', 0.2, @EReturnId, 38, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Ov4oujD3kd', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021336, @UserId, '13', '1', 213.9, 0, 'W0', 'St Marys Place', 'NULL', 0.2, @EReturnId, 39, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('zERVH9EPVD', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021337, @UserId, '13', '1', 34.7, 0, 'W0', 'Grahams Orchard', 'NULL', 0.2, @EReturnId, 40, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Z67Buj8wDJ', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104050575, @UserId, '13', '1', 10, 0, 'W0', 'West Road Pogmoor', 'NULL', 0.2, @EReturnId, 41, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('q1Ario26qx', 'jkQ7Uw43K', GETDATE(), GETDATE(), 72104021504, @UserId, '13', '1', 800, 0, 'N0', 'On-Street Parking', 'NULL', 0, @EReturnId, 42, 0)

INSERT INTO EReturns ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId]) VALUES ('R3',	NULL, NULL,	@EReturnType_Cheque, @EReturnStatus_InProgress, 1, GETDATE(), 1, NULL, NULL, NULL)
SELECT @EReturnId = @@IDENTITY;

INSERT INTO EReturnCheques ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount]) VALUES (@EReturnId, GETDATE(), 0, 'Ms J Smith', 200.00)
INSERT INTO EReturnCheques ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount]) VALUES (@EReturnId, GETDATE(), 0, 'Prof J Doe', 80.00)
INSERT INTO EReturnCheques ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount]) VALUES (@EReturnId, GETDATE(), 0, 'Mrs J Bloggs', 35.00)

INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('AqxrHdw479', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530692, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB money Children Dis', 'NULL', 0, @EReturnId, 1, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('AqxxCp60M7', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530359, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB money MentalHealth', 'NULL', 0, @EReturnId, 2, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('zEWWFB2Z3Z', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530621, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB monies SP', 'NULL', 0, @EReturnId, 3, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('RyDDI4ywDv', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530844, @UserId, '13', '2', 200, 0, 'N0', 'Sensory Support 65+ East', 'NULL', 0, @EReturnId, 4, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('B43Dc9jVZ', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530889, @UserId, '13', '2', NULL, 0, 'N0', 'MH 18 to 64 MH', 'NULL', 0, @EReturnId, 5, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('WVp9ToJrQW', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530832, @UserId, '13', '2', NULL, 0, 'N0', 'Return IB Monies West', 'NULL', 0, @EReturnId, 6, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('mjrVI36kBr', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530852, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB monies East', 'NULL', 0, @EReturnId, 7, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Dm8xT4E0o', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530861, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB money Disabilities', 'NULL', 0, @EReturnId, 8, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('RyJDIoB4pW', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530867, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB monies Transitions', 'NULL', 0, @EReturnId, 9, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('XRNDi9wBpv', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530840, @UserId, '13', '2', NULL, 0, 'N0', 'MH 65+ West Team', 'NULL', 0, @EReturnId, 10, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('1rw3T3o0Qv', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530856, @UserId, '13', '2', NULL, 0, 'N0', 'MH 65+ East Team', 'NULL', 0, @EReturnId, 11, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('lkp9C9Z0m0', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530842, @UserId, '13', '2', 80, 0, 'N0', 'Physical Support 65+ East Team', 'NULL', 0, @EReturnId, 12, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('w2Qmsb8A2', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530868, @UserId, '13', '2', NULL, 0, 'N0', 'Learning Disab 65+', 'NULL', 0, @EReturnId, 13, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('NO27iE31or', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530836, @UserId, '13', '2', NULL, 0, 'N0', 'Memory & Cogn 65+(West Team)', 'NULL', 0, @EReturnId, 14, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('JmRDF40rrv', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530152, @UserId, '13', '2', NULL, 0, 'N0', 'Social Support', 'NULL', 0, @EReturnId, 15, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('xGaNh00Zy', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530251, @UserId, '13', '2', NULL, 0, 'N0', 'Personal Health Budget', 'NULL', 0, @EReturnId, 16, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Z65qH66dZ', '9VxEU29JvN', GETDATE(), GETDATE(), 72300530860, @UserId, '13', '2', NULL, 0, 'N0', 'Mental Health 65+ (East Team)', 'NULL', 0, @EReturnId, 17, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('y656TD7z9', '9VxEU29JvN', GETDATE(), GETDATE(), 72300535075, @UserId, '13', '2', NULL, 0, 'N0', 'specialist team north ld 18-64 ', 'NULL', 0, @EReturnId, 18, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('np6pI9EpJK', '9VxEU29JvN', GETDATE(), GETDATE(), 72300535089, @UserId, '13', '2', NULL, 0, 'N0', 'specialist team south ld 18-64', 'NULL', 0, @EReturnId, 19, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('op63iKZwM', '9VxEU29JvN', GETDATE(), GETDATE(), 72300535054, @UserId, '13', '2', 35, 0, 'N0', 'Central and Penistone', 'NULL', 0, @EReturnId, 20, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('M2BEhAkvBG', '9VxEU29JvN', GETDATE(), GETDATE(), 72300540850, @UserId, '13', '2', NULL, 0, 'N0', 'North and North East', 'NULL', 0, @EReturnId, 21, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('Pv5EcEDNRb', '9VxEU29JvN', GETDATE(), GETDATE(), 72300540853, @UserId, '13', '2', NULL, 0, 'N0', 'North and North East', 'NULL', 0, @EReturnId, 22, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('w2zMh7aWj5', '9VxEU29JvN', GETDATE(), GETDATE(), 72300535062, @UserId, '13', '2', NULL, 0, 'N0', 'South and Dearne', 'NULL', 0, @EReturnId, 23, 0)

INSERT INTO EReturns ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId]) VALUES ('R4',	NULL, NULL,	@EReturnType_Cash, @EReturnStatus_InProgress, 3, GETDATE(), 1, NULL, NULL, NULL)
SELECT @EReturnId = @@IDENTITY;

INSERT INTO EReturnCash ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total]) VALUES (@EReturnId, GETDATE(), '111-9573-3478-8', NULL, 260.00, 340.00, 1395.00, 308.00, 197.00, 17.50, 2.60, 0.30, 0.10, 0.40, 0.60, 2521.50)

INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('np10SpRkQ', '5vbJSPEVJ3', GetDate(), GetDate(), 72104050031, @UserId, '13', '1', 495, 0, 'W0', 'Car Park 1', 'NULL', 0.2, @EReturnId, 43, 0)
INSERT INTO PendingTransactions ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [BatchReference], [VatRate], [EReturnId], [TemplateRowId], [StatusId]) VALUES  ('opEVTbJyXW', '5vbJSPEVJ3', GetDate(), GetDate(), 72104050031, @UserId, '13', '1', 2026.5, 0, 'W0', 'Car Park 2', 'NULL', 0.2, @EReturnId, 44, 0)



DECLARE @ImportProcessingRuleId INT = 0;
INSERT INTO ImportProcessingRules ([Name], [Description], [Disabled]) VALUES ('774952620 Transfer', 'Transfer �25.00 to 774952620', 0)
SELECT @ImportProcessingRuleId = @@IDENTITY;

INSERT INTO ImportProcessingRuleConditions ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator]) VALUES (@ImportProcessingRuleId, 0, 1, 7, 567749526, NULL)
INSERT INTO ImportProcessingRuleConditions ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator]) VALUES (@ImportProcessingRuleId, 0, 2, 4, 2, 'AND')
INSERT INTO ImportProcessingRuleConditions ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator]) VALUES (@ImportProcessingRuleId, 1, 3, 4, 22, NULL)

INSERT INTO ImportProcessingRuleActions ([ImportProcessingRuleId], [ImportProcessingRuleFieldId], [Value]) VALUES (@ImportProcessingRuleId, 3, 23)
INSERT INTO ImportProcessingRuleActions ([ImportProcessingRuleId], [ImportProcessingRuleFieldId], [Value]) VALUES (@ImportProcessingRuleId, 1, 774952620)


INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -1, GETDATE()), GETDATE(), 'RF162 0645', 'EFC PAYMENT           RF162 0645', 1804284, 'Barclays', NULL, '99988F662463')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507026510 J', 'DCLG                  1000 6507026510 J', 56096, 'Barclays', NULL, '99988F752806')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507025624 J', 'DCLG                  1000 6507025624 J', 457655, 'Barclays', NULL, '99988F742607')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507025114 J', 'DCLG                  1000 6507025114 J', 235172, 'Barclays', NULL, '99988A560060')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -1, GETDATE()), GETDATE(), '414 527 616 428', 'TREE PLANTERS FLEET SER    414/527/616/428', 682.82, 'Barclays', NULL, 'Sandra to contact the company')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -2, GETDATE()), GETDATE(), 'TD/', '06WENTWORTH QUEEN S    TD/', 800, 'Barclays', NULL, 'Clearing query passed to DL and TJ 26.9 DC 99988D789145')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -2, GETDATE()), GETDATE(), 'CENTRAL', 'ALPHA ACADEMY TRU    CENTRAL', 4872.79, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -2, GETDATE()), GETDATE(), '700001', 'BIRDWELL FOOTBALL SC      700001', 826.07, 'Barclays', NULL, 'Passed to Income Team 12.9 99988B120648')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -2, GETDATE()), GETDATE(), '230135771P - COMP', 'WORSBOROUGH COLLECTION    230135771P - COMP', 30, 'Barclays', NULL, 'All previously woff. Sent to NG for woff approval DC 26.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -3, GETDATE()), GETDATE(), 'MILL GREEN PRIMARY', 'BRETON WARD          MILL GREEN PRIMARY', 480, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -3, GETDATE()), GETDATE(), '400204001', 'EASTPOINT PROP      400204001', 37.2, 'Barclays', NULL, 'Email has been sent no date given on ssheet 99988A875001')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -3, GETDATE()), GETDATE(), 'ST MICHAELS PRIMARY', 'ST MICHAELS CURRENT     ST MICHAELS PRIMARY', 1626.27, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -4, GETDATE()), GETDATE(), '50037', 'THE MONKTON ALC        50037', 6888.95, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -4, GETDATE()), GETDATE(), '61894', 'SPRINGDALE PRIMARY    61894', 1911, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -4, GETDATE()), GETDATE(), '4894', 'SPRINGDALE PRIMARY    4894', 308, 'Barclays', NULL, 'Passed to Income Team 12.9')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -5, GETDATE()), GETDATE(), 'B O DRONVALE METRO', 'METROPOLI    B/O DRONVALE METRO', 25222.88, 'Barclays', NULL, 'Email to remits and income DC 6.3')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -7, GETDATE()), GETDATE(), '100650200484112', '100650200484112       000100650200484112', 1012.31, 'Barclays', NULL, 'DWP no remit unable to process will be woff after 8 weeks 99988C913002')
INSERT INTO Suspenses ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [BatchReference], [ProcessId], [Notes]) VALUES (DATEADD(d, -7, GETDATE()), GETDATE(), 'HENDERSONS PORTFOLIO', 'HENDERSONS PORTFOLIO    HENDERSONS PORTFOLIO', 11.59, 'Barclays', NULL, 'Called Hendersons, they will email remits over sg1409/Nothing on SAP DC 9.9 /// no remit or bb DC 8.9')

COMMIT TRAN