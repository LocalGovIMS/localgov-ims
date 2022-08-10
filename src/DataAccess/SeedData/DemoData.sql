/********************************************************/
/*          THIS SCRIPT NEEDS TO BE RERUNNABLE          */
/********************************************************/

SET IDENTITY_INSERT PaymentIntegrations ON;
MERGE INTO PaymentIntegrations AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(1, '[[SeedData.DemoData.PaymentIntegration.Name]]', '[[SeedData.DemoData.PaymentIntegration.BaseUri]]')) 
	AS S ([Id], [Name], [BaseUri])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name], [BaseUri])
VALUES ([Id], [Name], [BaseUri]);
SET IDENTITY_INSERT PaymentIntegrations OFF;

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
DECLARE @User1Username VARCHAR(100) = '[[SeedData.DemoData.User1.Username]]';
MERGE INTO Users AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(@User1Username, GETDATE(), 90, 0, '[[SeedData.DemoData.User1.Name]]', GETDATE(), NULL, 'SP')) 
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
DECLARE @User2Username VARCHAR(100) = '[[SeedData.DemoData.User2.Username]]';
MERGE INTO Users AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(@User2Username, GETDATE(), 90, 0, '[[SeedData.DemoData.User2.Name]]',	GETDATE(), NULL, 'SP')) 
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

MERGE INTO AspNetUsers AS [Target]
USING (SELECT * 
		FROM (VALUES 
			('a0f013ed-bdd1-4d1a-9b01-07b65de272af', '[[SeedData.DemoData.User1.Username]]', 1, '[[SeedData.DemoData.User1.PasswordHash]]', 'c024171d-c8a2-459d-af7d-4c1d1483f23a', NULL, 0, 0, NULL, 1, 0, '[[SeedData.DemoData.User1.Username]]'))
	AS S ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
	VALUES ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]);

MERGE INTO AspNetUsers AS [Target]
USING (SELECT * 
		FROM (VALUES 
			('b0f013ed-bdd1-4d1a-9b01-07b65de845ed', '[[SeedData.DemoData.User2.Username]]', 1, '[[SeedData.DemoData.User2.PasswordHash]]', 'd034611d-a8a2-859d-ff7d-5c1d1483f24b', NULL, 0, 0, NULL, 1, 0, '[[SeedData.DemoData.User2.Username]]'))
	AS S ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
	VALUES ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]);

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
			('90', 'GOV.UK Pay SelfService cards', 999999999, 0, 0),
			('91', 'GOV.UK Pay ATP cards', 999999999, 0, 0),
			('92', 'GOV.UK Pay Staff cards', 999999999, 0, 0),
			('PF', 'GOV.UK Pay Fee', 999999999, 0, 0)) 
	AS S ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled])) AS [Source]
ON [Target].[MopCode] = [Source].[MopCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled])
VALUES ([MopCode], [MopName], [MaximumAmount], [MinimumAmount], [Disabled]);

MERGE INTO MopMetadata AS [Target]
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
			('TextColour', '#fff', '24'),
			('PaymentIntegrationId', '1', '90'),
			('PaymentIntegrationId', '1', '91'),
			('PaymentIntegrationId', '1', '92'),
			('IncursAFee', 'TRUE', '90'),
			('IncursAFee', 'TRUE', '91'),
			('IncursAFee', 'TRUE', '92'),
			('IsACardPaymentFee', 'TRUE', 'PF')
			) 
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

MERGE INTO VatMetadata AS [Target]
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
			('5M', 'Library Charges', 'N0', 100, 0, 0, 1, 'Library Charges', 0, 0),
			('52', 'Market Stall Rental', 'E0', 5000, 0, 0, 1, 'Market Stall Rental', 0, 0),
			('2R', 'Fixed Penalty Notice', 'N0', 500, 1, 0, 1, 'Fixed Penalty Notice starting FP', 0, 0),
			('3U', 'Landlord Accreditation Service', 'W0', 99.99, 0, 0, 1, 'Landlord Accreditation Service', 0, 0),
			('1Z', 'HMO Premises Licence', 'N0', 99999999, 0, 0, 1, NULL, 0, 0),
			('ZZ', 'Not Authorised Cards', 'N0', 99999999, 0, 0, 1, NULL, 0, 0),
			('1', 'Bank Suspense', 'M0', 99999999.99, 1, 0, 1, NULL, 0, 0),			
			('5', 'Housing Rents', 'N0', 99999.99, 1, 1, 1, 'Housing Rents', 0, 0),
			('13', 'Misc Cash', 'W0', 999999999.99, 0, 0, 1, NULL, 1, 0),
			('SP', 'Suspense', 'M0', 99999999, 0, 0, 1, NULL, 0, 0),
			('XT', 'Transfers', 'N0', 99999999, 0, 0, 1, NULL, 0, 0),
			('19', 'SAP Invoices', '11', 99999999, 1, 1, 1, 'Invoices Starting 3 or 9', 0, 0),
			('20', 'BCT Invoices', '11', 99999999, 1, 1, 1, 'Invoices Starting M or P', 0, 0),
			('11', 'Parking Fines', 'N0', 99999999, 1, 0, 1, 'Parking Fines starting BJ', 0, 0),
			('24', 'Business Rates', 'N0', 9999999.99, 1, 1, 1, 'Business Rates', 0, 0),
			('23', 'Council Tax', 'N0', 999999.99, 1, 1, 1, 'Council Tax', 0, 0),
			('25', 'Benefit Overpayments', 'N0', 9999.99, 1, 1, 1, 'Benefit Overpayments', 0, 0),
			('32', 'Developmnet Control', 'N0', 99999, 0, 0, 1, 'Planning Fees', 0, 0),
			('31', 'Building Control', 'W0', 99999, 0, 0, 1, 'Building Control Fees', 0, 0))
	AS S ([FundCode], [FundName], [VatCode], [MaximumAmount], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [Disabled])) AS [Source]
ON [Target].[FundCode] = [Source].[FundCode] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([FundCode], [FundName], [VatCode], [MaximumAmount], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [Disabled])
VALUES ([FundCode], [FundName], [VatCode], [MaximumAmount], [OverPayAccount], [AccountExist], [AquireAddress], [DisplayName], [VatOverride], [Disabled]);

MERGE INTO FundMetadata AS [Target]
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
			('Basket.ReferenceFieldLabel', 'Housing rents reference', '5'), 
			('ExportToLedger', 'True', '5M'),
			('UseGeneralLedgerCode', 'True', '5M'),
			('GeneralLedgerCode', '72100710220', '5M'),
			('ExportToLedger', 'True', '52'),
			('UseGeneralLedgerCode', 'True', '52'),
			('GeneralLedgerCode', '72200850079', '52'),
			('ExportToLedger', 'True', '2R'),
			('UseGeneralLedgerCode', 'True', '2R'),
			('GeneralLedgerCode', '72100021754', '2R'),
			('ExportToLedger', 'True', '3U'),
			('UseGeneralLedgerCode', 'True', '3U'),
			('GeneralLedgerCode', '72101580438', '3U'),
			('ExportToLedger', 'True', '1Z'),
			('UseGeneralLedgerCode', 'True', '1Z'),
			('GeneralLedgerCode', '72101920227', '1Z'),
			('ExportToLedger', 'False', 'ZZ'),
			('UseGeneralLedgerCode', 'False', 'ZZ'),
			('ExportToLedger', 'True', '1'),
			('UseGeneralLedgerCode', 'True', '1'),
			('GeneralLedgerCode', '964024', '1'),
			('ExportToLedger', 'True', '5'),
			('UseGeneralLedgerCode', 'True', '5'),
			('GeneralLedgerCode', '964104', '5'),
			('ExportToLedger', 'True', '13'),
			('UseGeneralLedgerCode', 'False', '13'),
			('ExportToLedger', 'True', 'SP'),
			('UseGeneralLedgerCode', 'True', 'SP'),
			('GeneralLedgerCode', '964023', 'SP'),
			('ExportToLedger', 'False', 'XT'),
			('UseGeneralLedgerCode', 'False', 'XT'),
			('ExportToLedger', 'False', '19'),
			('UseGeneralLedgerCode', 'False', '19'),
			('ExportToLedger', 'False', '20'),
			('UseGeneralLedgerCode', 'False', '20'),
			('ExportToLedger', 'True', '11'),
			('UseGeneralLedgerCode', 'True', '11'),
			('GeneralLedgerCode', '72104021624', '11'),
			('ExportToLedger', 'True', '24'),
			('UseGeneralLedgerCode', 'True', '24'),
			('GeneralLedgerCode', '964102', '24'),
			('ExportToLedger', 'True', '23'),
			('UseGeneralLedgerCode', 'True', '23'),
			('GeneralLedgerCode', '964101', '23'),
			('ExportToLedger', 'True', '25'),
			('UseGeneralLedgerCode', 'True', '25'),
			('GeneralLedgerCode', '72320550287', '25'),
			('ExportToLedger', 'True', '32'),
			('UseGeneralLedgerCode', 'True', '32'),
			('GeneralLedgerCode', '72101750163', '32'),
			('ExportToLedger', 'True', '31'),
			('UseGeneralLedgerCode', 'True', '31'),
			('GeneralLedgerCode', '72101750164', '31'),
			('[[SeedData.DemoData.FundMetadata.Key1]]', '[[SeedData.DemoData.FundMetadata.Value1]]', '[[SeedData.DemoData.FundMetadata.FundCode1]]')
			)
	AS S ([Key], [Value], [FundCode])) AS [Source]
ON [Target].[FundCode] = [Source].[FundCode] 
	AND [Target].[Key] = [Source].[Key] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Key], [Value], [FundCode])
VALUES ([Key], [Value], [FundCode]);

DECLARE @CheckDigitConfigurationId_ParkingFines INT = 1;
DECLARE @CheckDigitConfigurationId_AcademyCtax INT = 2;
DECLARE @CheckDigitConfigurationId_FixedPenaltyNotice INT = 3;
DECLARE @CheckDigitConfigurationId_HousingRents INT = 4;
DECLARE @CheckDigitConfigurationId_LibraryCharges INT = 5;

DECLARE @Type_WeightedSum INT = 1;
DECLARE @Type_Dynix INT = 2;

SET IDENTITY_INSERT CheckDigitConfigurations ON;

MERGE INTO CheckDigitConfigurations AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@CheckDigitConfigurationId_ParkingFines, @Type_WeightedSum, 11, '05', '9,8,7,6,5,4,3,2,1,0', '10:A', 0),
			(@CheckDigitConfigurationId_AcademyCtax, @Type_WeightedSum, 10, NULL, '3,7,1,3,7,1,3,7,0', '10:0', 1),
			(@CheckDigitConfigurationId_FixedPenaltyNotice, @Type_WeightedSum, 11, '12', '9,8,7,6,5,4,3,2,1,0', '10:A', 0),
			(@CheckDigitConfigurationId_HousingRents, @Type_WeightedSum, 10, NULL, '0,0,9,7,1,4,6,3,2,8,0', '10:0', 1),
			(@CheckDigitConfigurationId_LibraryCharges, @Type_Dynix, 10, NULL, '2,0,2,0,2,0,2,0,2,0,2,0,2,0', '10:0', 0))
	AS S ([Id], [Type], [Modulus], [SourceSubstitutions], [Weightings], [ResultSubstitutions], [ApplySubtraction])) AS [Source]
ON [Target].[Id] = [Source].[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Type], [Modulus], [SourceSubstitutions], [Weightings], [ResultSubstitutions], [ApplySubtraction])
VALUES ([Id], [Type], [Modulus], [SourceSubstitutions], [Weightings], [ResultSubstitutions], [ApplySubtraction]);

SET IDENTITY_INSERT CheckDigitConfigurations OFF;

MERGE INTO AccountReferenceValidators AS [Target]
USING (SELECT * 
		FROM (VALUES
			('Open Validation', 1, 30, NULL,  '******************************', 1, NULL),
			('Market stalls', 5, 30, NULL,  '#*****************************', 1, NULL),
			('Parking Fines', 10, 10, NULL,  'BJ#######@', 5, @CheckDigitConfigurationId_ParkingFines),
			('Miscellaneous Income', 11, 11, NULL,  '###########', 1, NULL),
			('SAP Debtors', 10, 10, NULL,  '##########', 1, NULL),
			('Old Debtors', 11, 11, NULL,  '?##########', 1, NULL),
			('Academy CTAX/NNDR', 9, 9, NULL,  '########@', 1, @CheckDigitConfigurationId_AcademyCtax),
			('Benefit Overpayments', 8, 8, NULL,  '########', 1, NULL),
			('Fixed Penalty Notice', 10, 10, NULL,  'FP#######@', 5, @CheckDigitConfigurationId_FixedPenaltyNotice),
			('Building Control', 6, 30, NULL,  '******************************', 1, NULL),
			('Landlord accredit', 13, 13, NULL,  'ALS/####/####', 5, NULL),
			('Housing Rents', 11, 11, NULL,  '##########@', 1, @CheckDigitConfigurationId_HousingRents),
			('Library Charges', 14, 14, NULL,  '#############@', 1, @CheckDigitConfigurationId_LibraryCharges)) 
	AS S ([Name], [MinLength], [MaxLength], [Regex], [InputMask], [CharacterType], [CheckDigitConfigurationId])) AS [Source]
ON [Target].[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [MinLength], [MaxLength], [Regex], [InputMask], [CharacterType], [CheckDigitConfigurationId])
VALUES ([Name], [MinLength], [MaxLength], [Regex], [InputMask], [CharacterType], [CheckDigitConfigurationId]);

UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Market stalls') WHERE FundCode = '52'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Parking Fines') WHERE FundCode = '11'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Miscellaneous Income') WHERE FundCode = '13'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'SAP Debtors') WHERE FundCode = '19'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Old Debtors') WHERE FundCode = '20'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Academy CTAX/NNDR') WHERE FundCode = '22'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Academy CTAX/NNDR') WHERE FundCode = '23'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Academy CTAX/NNDR') WHERE FundCode = '24'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Benefit Overpayments') WHERE FundCode = '25'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Fixed Penalty Notice') WHERE FundCode = '2R'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Building Control') WHERE FundCode = '31'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Building Control') WHERE FundCode = '32'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Landlord accredit') WHERE FundCode = '3U'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Housing Rents') WHERE FundCode = '5'
UPDATE Funds SET AccountReferenceValidatorId = (SELECT Id FROM AccountReferenceValidators WHERE [Name] = 'Library Charges') WHERE FundCode = '5M'

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

DECLARE @FundMessageId_Squatter INT = 1;
DECLARE @FundMessageId_DoNotAcceptPayment INT = 2;
DECLARE @FundMessageId_AcceptPayment INT = 3;
DECLARE @FundMessageId_DoNotAcceptCheques INT = 4;
DECLARE @FundMessageId_ReferToBerneslaiHomes INT = 5;
DECLARE @FundMessageId_Bailiff INT = 6;
DECLARE @FundMessageId_RequiresConsultation INT = 7;

SET IDENTITY_INSERT FundMessages ON;

MERGE INTO FundMessages AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@FundMessageId_Squatter, '5', 'Squatter'),
			(@FundMessageId_DoNotAcceptPayment, '5', 'Do not accept payment - COURT'),
			(@FundMessageId_AcceptPayment, '5', 'Accept Payment - Refer for Int'),
			(@FundMessageId_DoNotAcceptCheques, '5', 'Do Not Accept CHEQUES'),
			(@FundMessageId_ReferToBerneslaiHomes, '5', 'Do Not Accept Payment - Refer to Berneslai Homes'),
			(@FundMessageId_Bailiff, '5', 'Do Not Accept Payment - BAILIFF. Refer to Berneslai Homes.'),
			(@FundMessageId_RequiresConsultation, '11', 'Payment should only be accepted after consultation with Parking Services.'))
	AS S ([Id], [FundCode], [Message])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [FundCode], [Message])
VALUES ([Id], [FundCode], [Message]);

SET IDENTITY_INSERT FundMessages OFF;

MERGE INTO FundMessageMetadata AS [Target]
USING (SELECT * 
		FROM (VALUES
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_Squatter),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_DoNotAcceptPayment),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_AcceptPayment),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_DoNotAcceptCheques),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_ReferToBerneslaiHomes),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_Bailiff),
			('IsOnStopForAdmin', 'TRUE', @FundMessageId_RequiresConsultation),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_Squatter),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_DoNotAcceptPayment),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_AcceptPayment),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_DoNotAcceptCheques),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_ReferToBerneslaiHomes),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_Bailiff),
			('IsOnStopForPaymentPortal', 'TRUE', @FundMessageId_RequiresConsultation),
			('ExternalCode', '62', @FundMessageId_Squatter),
			('ExternalCode', '63', @FundMessageId_DoNotAcceptPayment),
			('ExternalCode', '64', @FundMessageId_AcceptPayment),
			('ExternalCode', '65', @FundMessageId_DoNotAcceptCheques),
			('ExternalCode', '66', @FundMessageId_ReferToBerneslaiHomes),
			('ExternalCode', '67', @FundMessageId_Bailiff),
			('ExternalCode', '66', @FundMessageId_RequiresConsultation)) 
	AS S ([Key], [Value], [FundMessageId])) AS [Source]
ON [Target].[FundMessageId] = [Source].[FundMessageId] 
	AND [Target].[Key] = [Source].[Key] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Key], [Value], [FundMessageId])
VALUES ([Key], [Value], [FundMessageId]);

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

DECLARE @EReturn1Id INT = 0;
DECLARE @EReturn1No NVARCHAR(13) = 'R1';
DECLARE @EReturn2Id INT = 0;
DECLARE @EReturn2No NVARCHAR(13) = 'R2';
DECLARE @EReturn3Id INT = 0;
DECLARE @EReturn3No NVARCHAR(13) = 'R3';
DECLARE @EReturn4Id INT = 0;
DECLARE @EReturn4No NVARCHAR(13) = 'R4';

MERGE INTO EReturns AS [Target]
USING (SELECT * 
		FROM (VALUES
			(@EReturn1No, NULL, NULL, @EReturnType_Cash, @EReturnStatus_InProgress, 1, GETDATE(), @UserId, NULL, NULL, NULL),
			(@EReturn2No, NULL, NULL, @EReturnType_Cash, @EReturnStatus_InProgress, 2, GETDATE(), @UserId, NULL, NULL, NULL),
			(@EReturn3No, NULL, NULL, @EReturnType_Cheque, @EReturnStatus_InProgress, 1, GETDATE(), @UserId, NULL, NULL, NULL),
			(@EReturn4No, NULL, NULL, @EReturnType_Cash, @EReturnStatus_InProgress, 3, GETDATE(), @UserId, NULL, NULL, NULL)) 
	AS S ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId])) AS [Source]
ON [Target].[EReturnNo] = [Source].[EReturnNo] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId])
VALUES ([EReturnNo], [ApprovedAt], [ApprovedByUserId], [TypeId], [StatusId], [TemplateId], [CreatedAt], [CreatedByUserId], [SubmittedByUserId], [SubmittedAt], [ProcessId]);

SELECT @EReturn1Id = Id FROM EReturns WHERE EReturnNo = @EReturn1No;
SELECT @EReturn2Id = Id FROM EReturns WHERE EReturnNo = @EReturn2No;
SELECT @EReturn3Id = Id FROM EReturns WHERE EReturnNo = @EReturn3No;
SELECT @EReturn4Id = Id FROM EReturns WHERE EReturnNo = @EReturn4No;

MERGE INTO EReturnCash AS [Target]
USING (SELECT *
		FROM (VALUES
			(@EReturn1Id, GETDATE(), '109-5407-8453-7', 0.00, 100.00, 40.00, 5.00, 4.00, 4.00, 0.50, 1.00, 0.50, NULL, NULL, NULL, 155.00),
			(@EReturn2Id, GETDATE(), '121-3443-9896-5', 0.00, 200.00, 340.00, 85.00, 3856.00, 859.00, 90.50, 90.40, 36.60, 7.25, 0.18, 0.07, 5565.00),
			(@EReturn4Id, GETDATE(), '111-9573-3478-8', NULL, 260.00, 340.00, 1395.00, 308.00, 197.00, 17.50, 2.60, 0.30, 0.10, 0.40, 0.60, 2521.50)) 
	AS S ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total])) AS [Source]
ON [Target].[EReturnId] = [Source].[EReturnId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total])
VALUES ([EReturnId], [CreatedAt], [BagNumber], [Pounds50], [Pounds20], [Pounds10], [Pounds5], [Pounds2], [Pounds1], [Pence50], [Pence20], [Pence10], [Pence5], [Pence2], [Pence1], [Total]);

MERGE INTO EReturnCheques AS [Target]
USING (SELECT *
		FROM (VALUES
			(@EReturn3Id, GETDATE(), 0, 'Ms J Smith', 200.00),
			(@EReturn3Id, GETDATE(), 0, 'Prof J Doe', 80.00),
			(@EReturn3Id, GETDATE(), 0, 'Mrs J Bloggs', 35.00)) 
	AS S ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount])) AS [Source]
ON [Target].[EReturnId] = [Source].[EReturnId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount])
VALUES ([EReturnId], [CreatedAt], [ItemNo], [Name], [Amount]);

DECLARE @EReturn1InteralReference NVARCHAR(36) = 'q1bWFGKEWb' + CONVERT(NVARCHAR(100), @EReturn1Id);
DECLARE @EReturn2InteralReference NVARCHAR(36) = 'jkQ7Uw43Kr' + CONVERT(NVARCHAR(100), @EReturn2Id);
DECLARE @EReturn3InteralReference NVARCHAR(36) = '9VxEU29JvN' + CONVERT(NVARCHAR(100), @EReturn3Id);
DECLARE @EReturn4InteralReference NVARCHAR(36) = '5vbJSPEVJ3' + CONVERT(NVARCHAR(100), @EReturn4Id);

DECLARE @TemplateData TABLE(
	EReturnId INT, 
	TemplateId INT,
	TemplateRowId INT,
	[Description] NVARCHAR(100))
 
INSERT INTO @TemplateData
SELECT EReturns.Id AS EReturnId, 
	Templates.Id AS TemplateId,
	TemplateRows.Id AS TemplateRowId,
	TemplateRows.Description
FROM TemplateRows 
	INNER JOIN Templates ON TemplateRows.TemplateId = Templates.Id 
	INNER JOIN EReturns ON Templates.Id = EReturns.TemplateId 

MERGE INTO PendingTransactions AS [Target]
USING (SELECT *
		FROM (VALUES

			('mjyRT3WPPZ' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530692, @UserId, '13', '1', 100, 0, 'N0', 'Returned IB money Children Dis', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Returned IB money Children Dis'), 0),
			('q14NUGpk2A' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530359, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB money MentalHealth', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Returned IB money MentalHealth'), 0),
			('kQ24u4ZQEn' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530621, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB monies SP', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Returned IB monies SP'), 0),
			('2rNGCVrrbs' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530844, @UserId, '13', '1', 55, 0, 'N0', 'Sensory Support 65+ East', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Sensory Support 65+ East'), 0),
			('Ov7Dcl7JW5' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530889, @UserId, '13', '1', NULL, 0, 'N0', 'MH 18 to 64 MH', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'MH 18 to 64 MH'), 0),
			('RyGEiol4ZV' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530832, @UserId, '13', '1', NULL, 0, 'N0', 'Return IB Monies West', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Return IB Monies West'), 0),
			('w2jMhQB2GE' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300530852, @UserId, '13', '1', NULL, 0, 'N0', 'Returned IB monies East', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'Returned IB monies East'), 0),
			('M2N9IAQyBo' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300540853, @UserId, '13', '1', NULL, 0, 'N0', 'North and North East', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'North and North East'), 0),
			('B4GEIxx11v' + CONVERT(NVARCHAR(100), @EReturn1Id), @EReturn1InteralReference, GETDATE(), GETDATE(), 72300535062, @UserId, '13', '1', NULL, 0, 'N0', 'South', 0, @EReturn1Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn1Id AND [Description] = 'South'), 0),
			
			('jkQ7UWMB7K' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021312, @UserId, '13', '1', 2456.5, 0, 'W0', 'Multi Storey', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Multi Storey'), 0),
			('bm25iJBpks' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021333, @UserId, '13', '1', 45, 0, 'W0', 'Lambra Road', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Lambra Road'), 0),
			('JmEms4wWDp' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021342, @UserId, '13', '1', 456.9, 0, 'W0', 'High Street', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'High Street'), 0),
			('op3pS4pO3n' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021342, @UserId, '13', '1', 471.1, 0, 'W0', 'Sackville', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Sackville'), 0),
			('dPJPUZ9l9s' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021344, @UserId, '13', '1', 33, 0, 'W0', 'Mark Street', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Mark Street'), 0),
			('0GXGFGpZdl' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021343, @UserId, '13', '1', 63.2, 0, 'W0', 'Churchfields', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Churchfields'), 0),
			('q1M1SGJPm8' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021345, @UserId, '13', '1', 3.2, 0, 'W0', 'Court House', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Court House'), 0),
			('7zwduRpxQ7' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021338, @UserId, '13', '1', 1.5, 0, 'W0', 'Burleigh Street', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Burleigh Street'), 0),
			('w2MZHJZAac' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021332, @UserId, '13', '1', 1.8, 0, 'W0', 'John Street', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'John Street'), 0),
			('aRnZCrJxq7' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021339, @UserId, '13', '1', 0.9, 0, 'W0', 'Pitt Street', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Pitt Street'), 0),
			('Aqd9CdQZjq' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021334, @UserId, '13', '1', 44.5, 0, 'W0', 'Wellington House', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Wellington House'), 0),
			('3DAphx74yy' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021331, @UserId, '13', '1', 789.5, 0, 'W0', 'Market Gate Car Park', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Market Gate Car Park'), 0),
			('69vbIxa4vZ' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021347, @UserId, '13', '1', 32.5, 0, 'W0', 'Pipers Cottage', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Pipers Cottage'), 0),
			('npO7Hql2kc' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021335, @UserId, '13', '1', 94.8, 0, 'W0', 'Lancaster Gate', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Lancaster Gate'), 0),
			('Ov4oujD3kd' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021336, @UserId, '13', '1', 213.9, 0, 'W0', 'St Marys Place', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'St Marys Place'), 0),
			('zERVH9EPVD' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021337, @UserId, '13', '1', 34.7, 0, 'W0', 'Grahams Orchard', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'Grahams Orchard'), 0),
			('Z67Buj8wDJ' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104050575, @UserId, '13', '1', 10, 0, 'W0', 'West Road Pogmoor', 0.2, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'West Road Pogmoor'), 0),
			('q1Ario26qx' + CONVERT(NVARCHAR(100), @EReturn2Id), @EReturn2InteralReference, GETDATE(), GETDATE(), 72104021504, @UserId, '13', '1', 800, 0, 'N0', 'On-Street Parking', 0, @EReturn2Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn2Id AND [Description] = 'On-Street Parking'), 0),

			('AqxrHdw479' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530692, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB money Children Dis', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Returned IB money Children Dis'), 0),
			('AqxxCp60M7' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530359, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB money MentalHealth', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Returned IB money MentalHealth'), 0),
			('zEWWFB2Z3Z' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530621, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB monies SP', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Returned IB monies SP'), 0),
			('RyDDI4ywDv' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530844, @UserId, '13', '2', 200, 0, 'N0', 'Sensory Support 65+ East', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Sensory Support 65+ East'), 0),
			('B43Dc9jVZg' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530889, @UserId, '13', '2', NULL, 0, 'N0', 'MH 18 to 64 MH', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'MH 18 to 64 MH'), 0),
			('WVp9ToJrQW' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530832, @UserId, '13', '2', NULL, 0, 'N0', 'Return IB Monies West', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Return IB Monies West'), 0),
			('mjrVI36kBr' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300530852, @UserId, '13', '2', NULL, 0, 'N0', 'Returned IB monies East', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'Returned IB monies East'), 0),
			('Pv5EcEDNRb' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300540853, @UserId, '13', '2', NULL, 0, 'N0', 'North and North', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'North and North'), 0),
			('w2zMh7aWj5' + CONVERT(NVARCHAR(100), @EReturn3Id), @EReturn3InteralReference, GETDATE(), GETDATE(), 72300535062, @UserId, '13', '2', NULL, 0, 'N0', 'South and Dearne', 0, @EReturn3Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn3Id AND [Description] = 'South and Dearne'), 0),
			
			('np10SpRkQf' + CONVERT(NVARCHAR(100), @EReturn4Id), @EReturn4InteralReference, GetDate(), GetDate(), 72104050031, @UserId, '13', '1', 495, 0, 'W0', 'Car Park 1', 0.2, @EReturn4Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn4Id AND [Description] = 'Car Park 1'), 0),
			('opEVTbJyXW' + CONVERT(NVARCHAR(100), @EReturn4Id), @EReturn4InteralReference, GetDate(), GetDate(), 72104050031, @UserId, '13', '1', 2026.5, 0, 'W0', 'Car Park 2', 0.2, @EReturn4Id, (SELECT TemplateRowId FROM @TemplateData WHERE EReturnId = @EReturn4Id AND [Description] = 'Car Park 2'), 0))
AS S ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [VatRate], [EReturnId], [TemplateRowId], [StatusId])) AS [Source]
ON [Target].[TransactionReference] = [Source].[TransactionReference] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [VatRate], [EReturnId], [TemplateRowId], [StatusId])
VALUES ([TransactionReference], [InternalReference], [EntryDate], [TransactionDate], [AccountReference], [UserCode], [FundCode], [MopCode], [Amount], [VatAmount], [VatCode], [Narrative], [VatRate], [EReturnId], [TemplateRowId], [StatusId]);

DECLARE @ImportProcessingRuleId INT = 0;

MERGE INTO ImportProcessingRules AS [Target]
USING (SELECT *
		FROM (VALUES
			('774952620 Transfer', 'Transfer £25.00 to 774952620', 0))
	AS S ([Name], [Description], [Disabled])) AS [Source]
ON [Target].[Name] = [Source].[Name] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [Description], [Disabled])
VALUES ([Name], [Description], [Disabled]);

SELECT @ImportProcessingRuleId = Id FROM ImportProcessingRules WHERE Name = '774952620 Transfer';

MERGE INTO ImportProcessingRuleConditions AS [Target]
USING (SELECT *
		FROM (VALUES
			(@ImportProcessingRuleId, 0, 1, 7, 567749526, NULL),
			(@ImportProcessingRuleId, 0, 2, 4, 2, 'AND'),
			(@ImportProcessingRuleId, 1, 3, 4, 22, NULL))
	AS S ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator])) AS [Source]
ON [Target].[ImportProcessingRuleId] = [Source].[ImportProcessingRuleId] 
	AND [Target].[Group] = [Source].[Group] 
	AND [Target].[ImportProcessingRuleFieldId] = [Source].[ImportProcessingRuleFieldId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator])
VALUES ([ImportProcessingRuleId], [Group], [ImportProcessingRuleFieldId], [ImportProcessingRuleOperatorId], [Value], [LogicalOperator]);

MERGE INTO ImportProcessingRuleActions AS [Target]
USING (SELECT *
		FROM (VALUES
			(@ImportProcessingRuleId, 3, 23),
			(@ImportProcessingRuleId, 1, 774952620))
	AS S ([ImportProcessingRuleId], [ImportProcessingRuleFieldId], [Value])) AS [Source]
ON [Target].[ImportProcessingRuleId] = [Source].[ImportProcessingRuleId] 
	AND [Target].[ImportProcessingRuleFieldId] = [Source].[ImportProcessingRuleFieldId] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ImportProcessingRuleId], [ImportProcessingRuleFieldId], [Value])
VALUES ([ImportProcessingRuleId], [ImportProcessingRuleFieldId], [Value]);


-- setup import type
MERGE INTO ImportTypes AS [Target]
USING (SELECT *
		FROM (VALUES
			(1, 'Transaction Import', 'A transaction import', 'TT1', 0),
			(1, 'Account Holder Import', 'An account holder import', 'TT2', 0))
	AS S ([DataType], [Name], [Description], [ExternalReference], [IsReversible])) AS [Source]
ON [Target].[Name] = [Source].[Name] 
	AND [Target].[DataType] = [Source].[DataType] 
	AND [Target].[ExternalReference] = [Source].[ExternalReference] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([DataType], [Name], [Description], [ExternalReference], [IsReversible])
VALUES ([DataType], [Name], [Description], [ExternalReference], [IsReversible]);

MERGE INTO Suspenses AS [Target]
USING (SELECT *
		FROM (VALUES
			(DATEADD(d, -1, GETDATE()), GETDATE(), 'RF162 0645', 'EFC PAYMENT           RF162 0645', 1804284, NULL, '99988F662463', NULL),
			(DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507026510 J', 'DCLG                  1000 6507026510 J', 56096, NULL, '99988F752806', NULL),
			(DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507025624 J', 'DCLG                  1000 6507025624 J', 457655, NULL, '99988F742607', NULL),
			(DATEADD(d, -1, GETDATE()), GETDATE(), '1000 6507025114 J', 'DCLG                  1000 6507025114 J', 235172, NULL, '99988A560060', NULL),
			(DATEADD(d, -1, GETDATE()), GETDATE(), '414 527 616 428', 'TREE PLANTERS FLEET SER    414/527/616/428', 682.82, NULL, 'Sandra to contact the company', NULL),
			(DATEADD(d, -2, GETDATE()), GETDATE(), 'TD/', '06WENTWORTH QUEEN S    TD/', 800, NULL, 'Clearing query passed to DL and TJ 26.9 DC 99988D789145', NULL),
			(DATEADD(d, -2, GETDATE()), GETDATE(), 'CENTRAL', 'ALPHA ACADEMY TRU    CENTRAL', 4872.79, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -2, GETDATE()), GETDATE(), '700001', 'BIRDWELL FOOTBALL SC      700001', 826.07, NULL, 'Passed to Income Team 12.9 99988B120648', NULL),
			(DATEADD(d, -2, GETDATE()), GETDATE(), '230135771P - COMP', 'WORSBOROUGH COLLECTION    230135771P - COMP', 30, NULL, 'All previously woff. Sent to NG for woff approval DC 26.9', NULL),
			(DATEADD(d, -3, GETDATE()), GETDATE(), 'MILL GREEN PRIMARY', 'BRETON WARD          MILL GREEN PRIMARY', 480, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -3, GETDATE()), GETDATE(), '400204001', 'EASTPOINT PROP      400204001', 37.2, NULL, 'Email has been sent no date given on ssheet 99988A875001', NULL),
			(DATEADD(d, -3, GETDATE()), GETDATE(), 'ST MICHAELS PRIMARY', 'ST MICHAELS CURRENT     ST MICHAELS PRIMARY', 1626.27, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -4, GETDATE()), GETDATE(), '50037', 'THE MONKTON ALC        50037', 6888.95, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -4, GETDATE()), GETDATE(), '61894', 'SPRINGDALE PRIMARY    61894', 1911, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -4, GETDATE()), GETDATE(), '4894', 'SPRINGDALE PRIMARY    4894', 308, NULL, 'Passed to Income Team 12.9', NULL),
			(DATEADD(d, -5, GETDATE()), GETDATE(), 'B O DRONVALE METRO', 'METROPOLI    B/O DRONVALE METRO', 25222.88, NULL, 'Email to remits and income DC 6.3', NULL),
			(DATEADD(d, -7, GETDATE()), GETDATE(), '100650200484112', '100650200484112       000100650200484112', 1012.31, NULL, 'DWP no remit unable to process will be woff after 8 weeks 99988C913002', NULL),
			(DATEADD(d, -7, GETDATE()), GETDATE(), 'HENDERSONS PORTFOLIO', 'HENDERSONS PORTFOLIO    HENDERSONS PORTFOLIO', 11.59, NULL, 'Called Hendersons, they will email remits over sg1409/Nothing on SAP DC 9.9 /// no remit or bb DC 8.9', NULL))
	AS S ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [ProcessId], [Notes], [ImportId])) AS [Source]
ON [Target].[AccountNumber] = [Source].[AccountNumber] 
	AND ISNULL([Target].[ImportId], 1) = ISNULL([Source].[ImportId], 1)
WHEN NOT MATCHED BY TARGET THEN
INSERT ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [ProcessId], [Notes], [ImportId])
VALUES ([TransactionDate], [CreatedAt], [AccountNumber], [Narrative], [Amount], [ProcessId], [Notes], [ImportId]);

COMMIT TRAN