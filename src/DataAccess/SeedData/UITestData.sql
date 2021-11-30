SET IDENTITY_INSERT PaymentIntegrations ON;
MERGE INTO PaymentIntegrations AS [Target]
USING (SELECT * 
		FROM (VALUES 
			(1, '[[UITestInitialisation.PaymentIntegration.Name]]', '[[UITestInitialisation.PaymentIntegration.BaseUri]]')) 
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

MERGE INTO AspNetUsers AS [Target]
USING (SELECT * 
		FROM (VALUES 
			('f0f013ed-bdd1-4d1a-9b01-07b65de272af', '[[UITestInitialisation.User1.EmailAddress]]', 1, '[[UITestInitialisation.User1.PasswordHash]]', 'c024171d-c8a2-459d-af7d-4c1d1483f23a', NULL, 0, 0, NULL, 1, 0, '[[UITestInitialisation.User1.EmailAddress]]'))
	AS S ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
	VALUES ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]);

	MERGE INTO AspNetUsers AS [Target]
USING (SELECT * 
		FROM (VALUES 
			('f0f013ed-bdd1-4d1a-9b01-07b65de272af', '[[UITestInitialisation.User2.EmailAddress]]', 1, '[[UITestInitialisation.User2.PasswordHash]]', 'd034611d-a8a2-859d-ff7d-5c1d1483f24b', NULL, 0, 0, NULL, 1, 0, '[[UITestInitialisation.User2.EmailAddress]]'))
	AS S ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])) AS [Source]
ON [Target].[Id] = [Source].[Id] 
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
	VALUES ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]);