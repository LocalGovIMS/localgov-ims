namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountValidationWeightings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ValidationReference = c.String(nullable: false, maxLength: 3),
                    Digit1Weighting = c.String(maxLength: 2),
                    Digit2Weighting = c.String(maxLength: 2),
                    Digit3Weighting = c.String(maxLength: 2),
                    Digit4Weighting = c.String(maxLength: 2),
                    Digit5Weighting = c.String(maxLength: 2),
                    Digit6Weighting = c.String(maxLength: 2),
                    Digit7Weighting = c.String(maxLength: 2),
                    Digit8Weighting = c.String(maxLength: 2),
                    Digit9Weighting = c.String(maxLength: 2),
                    Digit10Weighting = c.String(maxLength: 2),
                    Digit11Weighting = c.String(maxLength: 2),
                    Digit12Weighting = c.String(maxLength: 2),
                    Digit13Weighting = c.String(maxLength: 2),
                    Digit14Weighting = c.String(maxLength: 2),
                    Digit15Weighting = c.String(maxLength: 2),
                    Digit16Weighting = c.String(maxLength: 2),
                    Digit17Weighting = c.String(maxLength: 2),
                    Digit18Weighting = c.String(maxLength: 2),
                    Digit19Weighting = c.String(maxLength: 2),
                    Digit20Weighting = c.String(maxLength: 2),
                    Digit21Weighting = c.String(maxLength: 2),
                    Digit22Weighting = c.String(maxLength: 2),
                    Digit23Weighting = c.String(maxLength: 2),
                    Digit24Weighting = c.String(maxLength: 2),
                    Digit25Weighting = c.String(maxLength: 2),
                    Digit26Weighting = c.String(maxLength: 2),
                    Digit27Weighting = c.String(maxLength: 2),
                    Digit28Weighting = c.String(maxLength: 2),
                    Digit29Weighting = c.String(maxLength: 2),
                    Digit30Weighting = c.String(maxLength: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountValidations", t => t.ValidationReference)
                .Index(t => t.ValidationReference);

            CreateTable(
                "dbo.AccountValidations",
                c => new
                {
                    ValidationReference = c.String(nullable: false, maxLength: 3),
                    Name = c.String(maxLength: 20),
                    Modulus = c.String(maxLength: 2),
                    TenConversion = c.String(maxLength: 1),
                    InputMask = c.String(maxLength: 30),
                    MinLength = c.String(maxLength: 2),
                    MaxLength = c.String(maxLength: 2),
                    SubtractFlag = c.Boolean(nullable: false),
                    CheckDigitCalcAlphaReplace = c.String(maxLength: 10),
                    CanNotBeNumeric = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ValidationReference);

            CreateTable(
                "dbo.AccountHolders",
                c => new
                {
                    AccountReference = c.String(nullable: false, maxLength: 30),
                    FundCode = c.String(maxLength: 5),
                    CurrentBalance = c.Decimal(precision: 18, scale: 2),
                    PeriodDebit = c.Decimal(precision: 18, scale: 2),
                    Title = c.String(maxLength: 10),
                    Forename = c.String(maxLength: 50),
                    Surname = c.String(maxLength: 50),
                    SurnameSoundex = c.String(maxLength: 50),
                    AddressLine1 = c.String(maxLength: 60),
                    AddressLine2 = c.String(maxLength: 30),
                    AddressLine3 = c.String(maxLength: 30),
                    AddressLine4 = c.String(maxLength: 30),
                    Postcode = c.String(maxLength: 10),
                    PeriodCredit = c.Decimal(precision: 18, scale: 2),
                    RecordType = c.String(maxLength: 10),
                    UserField1 = c.String(maxLength: 50),
                    UserField2 = c.String(maxLength: 50),
                    UserField3 = c.String(maxLength: 50),
                    StopMessageReference = c.String(maxLength: 2),
                    LastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AccountReference)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .ForeignKey("dbo.StopMessages", t => new { t.StopMessageReference, t.FundCode })
                .Index(t => t.FundCode)
                .Index(t => new { t.StopMessageReference, t.FundCode });

            CreateTable(
                "dbo.Funds",
                c => new
                {
                    FundCode = c.String(nullable: false, maxLength: 5),
                    FundName = c.String(nullable: false, maxLength: 50),
                    AccessLevel = c.String(maxLength: 2),
                    ValidationReference = c.String(maxLength: 3),
                    VatCode = c.String(maxLength: 2),
                    MaximumAmount = c.Decimal(precision: 18, scale: 2),
                    NarrativeFlag = c.Boolean(nullable: false),
                    ExportToFund = c.Boolean(nullable: false),
                    ExportToLedger = c.Boolean(nullable: false),
                    FundExportFormat = c.String(maxLength: 10),
                    UseGeneralLedgerCode = c.Boolean(nullable: false),
                    GeneralLedgerCode = c.String(maxLength: 20),
                    OverPayAccount = c.Boolean(nullable: false),
                    AccountExist = c.Boolean(nullable: false),
                    AquireAddress = c.Boolean(nullable: false),
                    DisplayName = c.String(maxLength: 50),
                    VatOverride = c.Boolean(nullable: false),
                    LedgerDetail = c.Boolean(),
                    Disabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.FundCode)
                .ForeignKey("dbo.Vat", t => t.VatCode)
                .Index(t => new { t.FundCode, t.FundName })
                .Index(t => t.VatCode);

            CreateTable(
                "dbo.FundGroupFunds",
                c => new
                {
                    FundGroupFundId = c.Int(nullable: false, identity: true),
                    FundGroupId = c.Int(nullable: false),
                    FundCode = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => t.FundGroupFundId)
                .ForeignKey("dbo.FundGroups", t => t.FundGroupId)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .Index(t => new { t.FundGroupId, t.FundCode }, unique: true);

            CreateTable(
                "dbo.FundGroups",
                c => new
                {
                    FundGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.FundGroupId);

            CreateTable(
                "dbo.UserFundGroups",
                c => new
                {
                    UserFundGroupId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    FundGroupId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserFundGroupId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.FundGroups", t => t.FundGroupId)
                .Index(t => new { t.UserId, t.FundGroupId }, unique: true);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 100),
                    LastLogin = c.DateTime(precision: 7, storeType: "datetime2"),
                    ExpiryDays = c.Int(nullable: false),
                    Disabled = c.Boolean(nullable: false),
                    DisplayName = c.String(maxLength: 150),
                    CreatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                    LastEnabledAt = c.DateTime(precision: 7, storeType: "datetime2"),
                    OfficeCode = c.String(maxLength: 2),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Offices", t => t.OfficeCode)
                .Index(t => new { t.UserName, t.Disabled }, unique: true)
                .Index(t => t.OfficeCode);

            CreateTable(
                "dbo.EReturns",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EReturnNo = c.String(maxLength: 13),
                    ApprovedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                    ApprovedByUserId = c.Int(),
                    TypeId = c.Int(nullable: false),
                    StatusId = c.Int(nullable: false),
                    TemplateId = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CreatedByUserId = c.Int(),
                    SubmittedByUserId = c.Int(),
                    SubmittedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                    ProcessId = c.String(maxLength: 36),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EReturnStatus", t => t.StatusId)
                .ForeignKey("dbo.EReturnTypes", t => t.TypeId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.Users", t => t.ApprovedByUserId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.SubmittedByUserId)
                .Index(t => t.ApprovedByUserId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId)
                .Index(t => t.TemplateId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.SubmittedByUserId);

            CreateTable(
                "dbo.EReturnCash",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EReturnId = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    BagNumber = c.String(maxLength: 50),
                    Pounds50 = c.Decimal(precision: 18, scale: 2),
                    Pounds20 = c.Decimal(precision: 18, scale: 2),
                    Pounds10 = c.Decimal(precision: 18, scale: 2),
                    Pounds5 = c.Decimal(precision: 18, scale: 2),
                    Pounds2 = c.Decimal(precision: 18, scale: 2),
                    Pounds1 = c.Decimal(precision: 18, scale: 2),
                    Pence50 = c.Decimal(precision: 18, scale: 2),
                    Pence20 = c.Decimal(precision: 18, scale: 2),
                    Pence10 = c.Decimal(precision: 18, scale: 2),
                    Pence5 = c.Decimal(precision: 18, scale: 2),
                    Pence2 = c.Decimal(precision: 18, scale: 2),
                    Pence1 = c.Decimal(precision: 18, scale: 2),
                    Total = c.Decimal(precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EReturns", t => t.EReturnId)
                .Index(t => t.EReturnId);

            CreateTable(
                "dbo.EReturnCheques",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EReturnId = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    ItemNo = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EReturns", t => t.EReturnId)
                .Index(t => t.EReturnId);

            CreateTable(
                "dbo.EReturnStatus",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 100),
                    Description = c.String(maxLength: 255),
                    DisplayName = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EReturnTypes",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 100),
                    Description = c.String(maxLength: 255),
                    DisplayName = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PendingTransactions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TransactionReference = c.String(nullable: false, maxLength: 36),
                    InternalReference = c.String(maxLength: 36),
                    OfficeCode = c.String(maxLength: 2),
                    EntryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TransactionDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AccountReference = c.String(maxLength: 30),
                    UserCode = c.Int(nullable: false),
                    FundCode = c.String(maxLength: 5),
                    MopCode = c.String(maxLength: 5),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    VatAmount = c.Decimal(precision: 18, scale: 2),
                    VatCode = c.String(maxLength: 2),
                    Narrative = c.String(maxLength: 100),
                    BatchReference = c.String(maxLength: 30),
                    MerchantNumber = c.String(maxLength: 30),
                    AuthorisationCode = c.String(maxLength: 15),
                    AppReference = c.String(maxLength: 100),
                    CardHolderName = c.String(maxLength: 50),
                    CardHolderBusinessName = c.String(maxLength: 100),
                    CardHolderPremiseNumber = c.String(maxLength: 50),
                    CardHolderPremiseName = c.String(maxLength: 100),
                    CardHolderStreet = c.String(maxLength: 50),
                    CardHolderArea = c.String(maxLength: 50),
                    CardHolderTown = c.String(maxLength: 50),
                    CardHolderCounty = c.String(maxLength: 50),
                    CardHolderPostCode = c.String(maxLength: 10),
                    VatRate = c.Single(nullable: false),
                    SuccessUrl = c.String(maxLength: 255),
                    CancelUrl = c.String(maxLength: 255),
                    FailUrl = c.String(maxLength: 255),
                    Legacy = c.Boolean(),
                    RefundReference = c.String(maxLength: 36),
                    Processed = c.Boolean(),
                    ExpiryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EReturnId = c.Int(),
                    TemplateRowId = c.Int(),
                    StatusId = c.Int(nullable: false),
                    CallRecordingSource = c.String(maxLength: 50),
                    CallRecordingUserName = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id, clustered: false)
                .ForeignKey("dbo.EReturns", t => t.EReturnId)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .ForeignKey("dbo.Mops", t => t.MopCode)
                .ForeignKey("dbo.Offices", t => t.OfficeCode)
                .ForeignKey("dbo.Vat", t => t.VatCode)
                .ForeignKey("dbo.Users", t => t.UserCode)
                .Index(t => t.TransactionReference, unique: true)
                .Index(t => t.OfficeCode)
                .Index(t => t.TransactionDate, clustered: true)
                .Index(t => t.UserCode)
                .Index(t => t.FundCode)
                .Index(t => t.MopCode)
                .Index(t => t.VatCode)
                .Index(t => new { t.RefundReference, t.Processed })
                .Index(t => t.EReturnId);

            CreateTable(
                "dbo.Mops",
                c => new
                {
                    MopCode = c.String(nullable: false, maxLength: 5),
                    MopName = c.String(nullable: false, maxLength: 30),
                    MaximumAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MinimumAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Disabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.MopCode);

            CreateTable(
                "dbo.MopMetaData",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Key = c.String(nullable: false, maxLength: 100),
                    Value = c.String(nullable: false),
                    MopCode = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mops", t => t.MopCode)
                .Index(t => t.MopCode);

            CreateTable(
                "dbo.ProcessedTransactions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TransactionReference = c.String(nullable: false, maxLength: 36),
                    InternalReference = c.String(maxLength: 36),
                    PspReference = c.String(maxLength: 16),
                    OfficeCode = c.String(maxLength: 2),
                    EntryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TransactionDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AccountReference = c.String(maxLength: 30),
                    UserCode = c.Int(nullable: false),
                    FundCode = c.String(maxLength: 5),
                    MopCode = c.String(maxLength: 5),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    VatAmount = c.Decimal(precision: 18, scale: 2),
                    VatCode = c.String(maxLength: 2),
                    Narrative = c.String(maxLength: 100),
                    BatchReference = c.String(maxLength: 30),
                    MerchantNumber = c.String(maxLength: 30),
                    AuthorisationCode = c.String(maxLength: 15),
                    AppReference = c.String(maxLength: 100),
                    CardHolderName = c.String(maxLength: 50),
                    CardHolderBusinessName = c.String(maxLength: 100),
                    CardHolderPremiseNumber = c.String(maxLength: 50),
                    CardHolderPremiseName = c.String(maxLength: 100),
                    CardHolderStreet = c.String(maxLength: 50),
                    CardHolderArea = c.String(maxLength: 50),
                    CardHolderTown = c.String(maxLength: 50),
                    CardHolderCounty = c.String(maxLength: 50),
                    CardHolderPostCode = c.String(maxLength: 10),
                    VatRate = c.Single(nullable: false),
                    RefundReference = c.String(maxLength: 36),
                    TransferReference = c.String(maxLength: 36),
                    TransferGuid = c.String(maxLength: 36),
                    TransferRollbackGuid = c.String(maxLength: 36),
                    ReceiptIssued = c.Boolean(nullable: false),
                    EReturnId = c.Int(),
                    ChequeSortCode = c.String(maxLength: 50),
                    ChequeAccountNumber = c.String(maxLength: 50),
                    ChequeNumber = c.String(maxLength: 50),
                    ChequeName = c.String(maxLength: 50),
                    OriginalTransactionReference = c.String(maxLength: 13),
                })
                .PrimaryKey(t => t.Id, clustered: false)
                .ForeignKey("dbo.EReturns", t => t.EReturnId)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .ForeignKey("dbo.Mops", t => t.MopCode)
                .ForeignKey("dbo.Offices", t => t.OfficeCode)
                .ForeignKey("dbo.Vat", t => t.VatCode)
                .ForeignKey("dbo.Users", t => t.UserCode)
                .Index(t => t.TransactionReference)
                .Index(t => t.InternalReference)
                .Index(t => t.PspReference)
                .Index(t => t.OfficeCode)
                .Index(t => t.EntryDate, clustered: true)
                .Index(t => t.AccountReference)
                .Index(t => t.UserCode)
                .Index(t => t.FundCode)
                .Index(t => t.MopCode)
                .Index(t => t.Amount)
                .Index(t => t.VatCode)
                .Index(t => t.AppReference)
                .Index(t => t.EReturnId);

            CreateTable(
                "dbo.EmailLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    EmailType = c.String(nullable: false, maxLength: 10),
                    TransactionProcessedId = c.Int(),
                    RecipientEmailAddress = c.String(nullable: false, maxLength: 255),
                    Subject = c.String(nullable: false, maxLength: 255),
                    Body = c.String(nullable: false, unicode: false, storeType: "text"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessedTransactions", t => t.TransactionProcessedId)
                .Index(t => t.TransactionProcessedId);

            CreateTable(
                "dbo.Offices",
                c => new
                {
                    OfficeCode = c.String(nullable: false, maxLength: 2),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.OfficeCode);

            CreateTable(
                "dbo.SuspenseProcessedTransactions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SuspenseId = c.Int(nullable: false),
                    TransactionInId = c.Int(nullable: false),
                    TransactionOutId = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CreatedByUserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suspenses", t => t.SuspenseId)
                .ForeignKey("dbo.ProcessedTransactions", t => t.TransactionInId)
                .ForeignKey("dbo.ProcessedTransactions", t => t.TransactionOutId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.SuspenseId)
                .Index(t => t.TransactionInId)
                .Index(t => t.TransactionOutId)
                .Index(t => t.CreatedByUserId);

            CreateTable(
                "dbo.Suspenses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TransactionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    AccountNumber = c.String(nullable: false, maxLength: 50),
                    Narrative = c.String(maxLength: 100),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    BatchReference = c.String(nullable: false, maxLength: 30),
                    ProcessId = c.String(maxLength: 36),
                    Notes = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Vat",
                c => new
                {
                    VatCode = c.String(nullable: false, maxLength: 2),
                    Percentage = c.Decimal(precision: 18, scale: 2),
                    Disabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.VatCode);

            CreateTable(
                "dbo.VatMetaData",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Key = c.String(nullable: false, maxLength: 100),
                    Value = c.String(nullable: false),
                    VatCode = c.String(nullable: false, maxLength: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vat", t => t.VatCode)
                .Index(t => t.VatCode);

            CreateTable(
                "dbo.TemplateRows",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TemplateId = c.Int(nullable: false),
                    Reference = c.String(nullable: false, maxLength: 50),
                    VatCode = c.String(nullable: false, maxLength: 2),
                    Description = c.String(nullable: false, maxLength: 100),
                    VatOverride = c.Boolean(nullable: false),
                    ReferrenceOverride = c.Boolean(nullable: false),
                    DescriptionOverride = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.Vat", t => t.VatCode)
                .Index(t => t.TemplateId)
                .Index(t => t.VatCode);

            CreateTable(
                "dbo.Templates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    ModifyVat = c.Boolean(nullable: false),
                    ModifyReference = c.Boolean(nullable: false),
                    ModifyDescription = c.Boolean(nullable: false),
                    Cheque = c.Boolean(nullable: false),
                    Cash = c.Boolean(nullable: false),
                    Pdq = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserTemplates",
                c => new
                {
                    UserTemplateId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    TemplateId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserTemplateId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TemplateId);

            CreateTable(
                "dbo.UserPostPaymentMopCodes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    MopCode = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mops", t => t.MopCode)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.MopCode }, unique: true);

            CreateTable(
                "dbo.UserRoles",
                c => new
                {
                    UserRoleId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.RoleId }, unique: true);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    RoleId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    DisplayName = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.FundMetaData",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Key = c.String(nullable: false, maxLength: 100),
                    Value = c.String(nullable: false),
                    FundCode = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .Index(t => t.FundCode);

            CreateTable(
                "dbo.Origins",
                c => new
                {
                    OriginId = c.Int(nullable: false, identity: true),
                    OriginName = c.String(nullable: false, maxLength: 50),
                    FundCode = c.String(nullable: false, maxLength: 5),
                    MessageName = c.String(maxLength: 100),
                    ReferenceFieldLabel = c.String(maxLength: 50),
                    ReferenceFieldMessage = c.String(maxLength: 80),
                })
                .PrimaryKey(t => t.OriginId)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .Index(t => t.FundCode);

            CreateTable(
                "dbo.StopMessages",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 2),
                    FundCode = c.String(nullable: false, maxLength: 5),
                    Message = c.String(maxLength: 100),
                })
                .PrimaryKey(t => new { t.Id, t.FundCode })
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .Index(t => t.FundCode);

            CreateTable(
                "dbo.ActivityLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    ObjectId = c.String(maxLength: 50),
                    ObjectType = c.Int(),
                    CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Description = c.String(nullable: false),
                    GroupId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ScheduleLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    JobName = c.String(maxLength: 100),
                    JobRunTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Message = c.String(maxLength: 1000),
                })
                .PrimaryKey(t => t.Id, clustered: false)
                .Index(t => t.JobRunTime, clustered: true);

            CreateTable(
                "dbo.SystemMessages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Severity = c.String(nullable: false, maxLength: 10),
                    StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Message = c.String(nullable: false, maxLength: 160),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TransactionNotifications",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MerchantReference = c.String(maxLength: 100),
                    EventCode = c.String(maxLength: 100),
                    OriginalReference = c.String(maxLength: 1000),
                    PspReference = c.String(maxLength: 1000),
                    EventDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    PaymentMethod = c.String(maxLength: 100),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    Success = c.Boolean(nullable: false),
                    Reason = c.String(maxLength: 1000),
                    Operations = c.String(maxLength: 100),
                    Live = c.Boolean(nullable: false),
                    Processed = c.Boolean(nullable: false),
                    MerchantAccountCode = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TransactionStatus",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 100),
                    Description = c.String(maxLength: 255),
                    DisplayName = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" }, "dbo.StopMessages");
            DropForeignKey("dbo.StopMessages", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.Origins", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.FundMetaData", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.FundGroupFunds", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.UserFundGroups", "FundGroupId", "dbo.FundGroups");
            DropForeignKey("dbo.UserTemplates", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserPostPaymentMopCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFundGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.SuspenseProcessedTransactions", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.EReturns", "SubmittedByUserId", "dbo.Users");
            DropForeignKey("dbo.ProcessedTransactions", "UserCode", "dbo.Users");
            DropForeignKey("dbo.PendingTransactions", "UserCode", "dbo.Users");
            DropForeignKey("dbo.EReturns", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.EReturns", "ApprovedByUserId", "dbo.Users");
            DropForeignKey("dbo.UserPostPaymentMopCodes", "MopCode", "dbo.Mops");
            DropForeignKey("dbo.TemplateRows", "VatCode", "dbo.Vat");
            DropForeignKey("dbo.UserTemplates", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.TemplateRows", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.EReturns", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.ProcessedTransactions", "VatCode", "dbo.Vat");
            DropForeignKey("dbo.PendingTransactions", "VatCode", "dbo.Vat");
            DropForeignKey("dbo.VatMetaData", "VatCode", "dbo.Vat");
            DropForeignKey("dbo.Funds", "VatCode", "dbo.Vat");
            DropForeignKey("dbo.SuspenseProcessedTransactions", "TransactionOutId", "dbo.ProcessedTransactions");
            DropForeignKey("dbo.SuspenseProcessedTransactions", "TransactionInId", "dbo.ProcessedTransactions");
            DropForeignKey("dbo.SuspenseProcessedTransactions", "SuspenseId", "dbo.Suspenses");
            DropForeignKey("dbo.Users", "OfficeCode", "dbo.Offices");
            DropForeignKey("dbo.ProcessedTransactions", "OfficeCode", "dbo.Offices");
            DropForeignKey("dbo.PendingTransactions", "OfficeCode", "dbo.Offices");
            DropForeignKey("dbo.ProcessedTransactions", "MopCode", "dbo.Mops");
            DropForeignKey("dbo.ProcessedTransactions", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.ProcessedTransactions", "EReturnId", "dbo.EReturns");
            DropForeignKey("dbo.EmailLogs", "TransactionProcessedId", "dbo.ProcessedTransactions");
            DropForeignKey("dbo.PendingTransactions", "MopCode", "dbo.Mops");
            DropForeignKey("dbo.MopMetaData", "MopCode", "dbo.Mops");
            DropForeignKey("dbo.PendingTransactions", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.PendingTransactions", "EReturnId", "dbo.EReturns");
            DropForeignKey("dbo.EReturns", "TypeId", "dbo.EReturnTypes");
            DropForeignKey("dbo.EReturns", "StatusId", "dbo.EReturnStatus");
            DropForeignKey("dbo.EReturnCheques", "EReturnId", "dbo.EReturns");
            DropForeignKey("dbo.EReturnCash", "EReturnId", "dbo.EReturns");
            DropForeignKey("dbo.FundGroupFunds", "FundGroupId", "dbo.FundGroups");
            DropForeignKey("dbo.AccountHolders", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.AccountValidationWeightings", "ValidationReference", "dbo.AccountValidations");
            DropIndex("dbo.ScheduleLogs", new[] { "JobRunTime" });
            DropIndex("dbo.StopMessages", new[] { "FundCode" });
            DropIndex("dbo.Origins", new[] { "FundCode" });
            DropIndex("dbo.FundMetaData", new[] { "FundCode" });
            DropIndex("dbo.UserRoles", new[] { "UserId", "RoleId" });
            DropIndex("dbo.UserPostPaymentMopCodes", new[] { "UserId", "MopCode" });
            DropIndex("dbo.UserTemplates", new[] { "TemplateId" });
            DropIndex("dbo.UserTemplates", new[] { "UserId" });
            DropIndex("dbo.TemplateRows", new[] { "VatCode" });
            DropIndex("dbo.TemplateRows", new[] { "TemplateId" });
            DropIndex("dbo.VatMetaData", new[] { "VatCode" });
            DropIndex("dbo.SuspenseProcessedTransactions", new[] { "CreatedByUserId" });
            DropIndex("dbo.SuspenseProcessedTransactions", new[] { "TransactionOutId" });
            DropIndex("dbo.SuspenseProcessedTransactions", new[] { "TransactionInId" });
            DropIndex("dbo.SuspenseProcessedTransactions", new[] { "SuspenseId" });
            DropIndex("dbo.EmailLogs", new[] { "TransactionProcessedId" });
            DropIndex("dbo.ProcessedTransactions", new[] { "EReturnId" });
            DropIndex("dbo.ProcessedTransactions", new[] { "AppReference" });
            DropIndex("dbo.ProcessedTransactions", new[] { "VatCode" });
            DropIndex("dbo.ProcessedTransactions", new[] { "Amount" });
            DropIndex("dbo.ProcessedTransactions", new[] { "MopCode" });
            DropIndex("dbo.ProcessedTransactions", new[] { "FundCode" });
            DropIndex("dbo.ProcessedTransactions", new[] { "UserCode" });
            DropIndex("dbo.ProcessedTransactions", new[] { "AccountReference" });
            DropIndex("dbo.ProcessedTransactions", new[] { "EntryDate" });
            DropIndex("dbo.ProcessedTransactions", new[] { "OfficeCode" });
            DropIndex("dbo.ProcessedTransactions", new[] { "PspReference" });
            DropIndex("dbo.ProcessedTransactions", new[] { "InternalReference" });
            DropIndex("dbo.ProcessedTransactions", new[] { "TransactionReference" });
            DropIndex("dbo.MopMetaData", new[] { "MopCode" });
            DropIndex("dbo.PendingTransactions", new[] { "EReturnId" });
            DropIndex("dbo.PendingTransactions", new[] { "RefundReference", "Processed" });
            DropIndex("dbo.PendingTransactions", new[] { "VatCode" });
            DropIndex("dbo.PendingTransactions", new[] { "MopCode" });
            DropIndex("dbo.PendingTransactions", new[] { "FundCode" });
            DropIndex("dbo.PendingTransactions", new[] { "UserCode" });
            DropIndex("dbo.PendingTransactions", new[] { "TransactionDate" });
            DropIndex("dbo.PendingTransactions", new[] { "OfficeCode" });
            DropIndex("dbo.PendingTransactions", new[] { "TransactionReference" });
            DropIndex("dbo.EReturnCheques", new[] { "EReturnId" });
            DropIndex("dbo.EReturnCash", new[] { "EReturnId" });
            DropIndex("dbo.EReturns", new[] { "SubmittedByUserId" });
            DropIndex("dbo.EReturns", new[] { "CreatedByUserId" });
            DropIndex("dbo.EReturns", new[] { "TemplateId" });
            DropIndex("dbo.EReturns", new[] { "StatusId" });
            DropIndex("dbo.EReturns", new[] { "TypeId" });
            DropIndex("dbo.EReturns", new[] { "ApprovedByUserId" });
            DropIndex("dbo.Users", new[] { "OfficeCode" });
            DropIndex("dbo.Users", new[] { "UserName", "Disabled" });
            DropIndex("dbo.UserFundGroups", new[] { "UserId", "FundGroupId" });
            DropIndex("dbo.FundGroupFunds", new[] { "FundGroupId", "FundCode" });
            DropIndex("dbo.Funds", new[] { "VatCode" });
            DropIndex("dbo.Funds", new[] { "FundCode", "FundName" });
            DropIndex("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" });
            DropIndex("dbo.AccountHolders", new[] { "FundCode" });
            DropIndex("dbo.AccountValidationWeightings", new[] { "ValidationReference" });
            DropTable("dbo.TransactionStatus");
            DropTable("dbo.TransactionNotifications");
            DropTable("dbo.SystemMessages");
            DropTable("dbo.ScheduleLogs");
            DropTable("dbo.ActivityLogs");
            DropTable("dbo.StopMessages");
            DropTable("dbo.Origins");
            DropTable("dbo.FundMetaData");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserPostPaymentMopCodes");
            DropTable("dbo.UserTemplates");
            DropTable("dbo.Templates");
            DropTable("dbo.TemplateRows");
            DropTable("dbo.VatMetaData");
            DropTable("dbo.Vat");
            DropTable("dbo.Suspenses");
            DropTable("dbo.SuspenseProcessedTransactions");
            DropTable("dbo.Offices");
            DropTable("dbo.EmailLogs");
            DropTable("dbo.ProcessedTransactions");
            DropTable("dbo.MopMetaData");
            DropTable("dbo.Mops");
            DropTable("dbo.PendingTransactions");
            DropTable("dbo.EReturnTypes");
            DropTable("dbo.EReturnStatus");
            DropTable("dbo.EReturnCheques");
            DropTable("dbo.EReturnCash");
            DropTable("dbo.EReturns");
            DropTable("dbo.Users");
            DropTable("dbo.UserFundGroups");
            DropTable("dbo.FundGroups");
            DropTable("dbo.FundGroupFunds");
            DropTable("dbo.Funds");
            DropTable("dbo.AccountHolders");
            DropTable("dbo.AccountValidations");
            DropTable("dbo.AccountValidationWeightings");
        }
    }
}
