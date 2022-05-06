SET IDENTITY_INSERT PaymentIntegrations ON;
MERGE INTO PaymentIntegrations AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(1, '[[SeedData.UITestData.PaymentIntegration.Name]]', '[[SeedData.UITestData.PaymentIntegration.BaseUri]]')) 
	AS S ([Id], [Name], [BaseUri])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Name], [BaseUri])
	VALUES ([Id], [Name], [BaseUri])
WHEN MATCHED THEN
	UPDATE SET [BaseUri] = [Source].[BaseUri];
SET IDENTITY_INSERT PaymentIntegrations OFF;

MERGE INTO AccountHolders AS [Target]
USING (SELECT * 
		FROM (VALUES
			('61100513680', 5, 375.57, 0.00, 'MRS', NULL, 'MS TESTER', 'M200', '10 Dunn Lane', 'Dredworth', 'Townley', 'South Yorkshire', NULL, 0.00, 'Imported', '33', NULL, NULL, '67', '2021-11-01 05:30:37.800'))
	AS S ([AccountReference], [FundCode], [CurrentBalance], [PeriodDebit], [Title], [Forename], [Surname], [SurnameSoundex], [AddressLine1], [AddressLine2], [AddressLine3], [AddressLine4], [Postcode], [PeriodCredit], [RecordType], [UserField1], [UserField2], [UserField3], [StopMessageReference], [LastUpdated])) AS [Source]
ON [Target].[AccountReference] = [Source].[AccountReference] 
WHEN NOT MATCHED BY TARGET THEN
INSERT ([AccountReference], [FundCode], [CurrentBalance], [PeriodDebit], [Title], [Forename], [Surname], [SurnameSoundex], [AddressLine1], [AddressLine2], [AddressLine3], [AddressLine4], [Postcode], [PeriodCredit], [RecordType], [UserField1], [UserField2], [UserField3], [StopMessageReference], [LastUpdated])
VALUES ([AccountReference], [FundCode], [CurrentBalance], [PeriodDebit], [Title], [Forename], [Surname], [SurnameSoundex], [AddressLine1], [AddressLine2], [AddressLine3], [AddressLine4], [Postcode], [PeriodCredit], [RecordType], [UserField1], [UserField2], [UserField3], [StopMessageReference], [LastUpdated]);