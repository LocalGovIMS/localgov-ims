namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_new_account_reference_validation_entities_and_removed_old_ones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountValidationWeightings", "ValidationReference", "dbo.AccountValidations");
            DropIndex("dbo.AccountValidationWeightings", new[] { "ValidationReference" });
            CreateTable(
                "dbo.AccountReferenceValidators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        MinLength = c.Short(nullable: false),
                        MaxLength = c.Short(nullable: false),
                        Regex = c.String(maxLength: 30),
                        InputMask = c.String(maxLength: 30),
                        CharacterType = c.Int(),
                        CheckDigitConfigurationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CheckDigitConfigurations", t => t.CheckDigitConfigurationId)
                .Index(t => t.CheckDigitConfigurationId);
            
            CreateTable(
                "dbo.CheckDigitConfigurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Modulus = c.Short(nullable: false),
                        SourceSubstitutions = c.String(maxLength: 20),
                        Weightings = c.String(maxLength: 30),
                        ResultSubstitutions = c.String(maxLength: 20),
                        ApplySubtraction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Funds", "AccountReferenceValidatorId", c => c.Int());
            CreateIndex("dbo.Funds", "AccountReferenceValidatorId");
            AddForeignKey("dbo.Funds", "AccountReferenceValidatorId", "dbo.AccountReferenceValidators", "Id");
            DropColumn("dbo.Funds", "ValidationReference");
            DropTable("dbo.AccountValidationWeightings");
            DropTable("dbo.AccountValidations");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Funds", "ValidationReference", c => c.String(maxLength: 3));
            DropForeignKey("dbo.Funds", "AccountReferenceValidatorId", "dbo.AccountReferenceValidators");
            DropForeignKey("dbo.AccountReferenceValidators", "CheckDigitConfigurationId", "dbo.CheckDigitConfigurations");
            DropIndex("dbo.AccountReferenceValidators", new[] { "CheckDigitConfigurationId" });
            DropIndex("dbo.Funds", new[] { "AccountReferenceValidatorId" });
            DropColumn("dbo.Funds", "AccountReferenceValidatorId");
            DropTable("dbo.CheckDigitConfigurations");
            DropTable("dbo.AccountReferenceValidators");
            CreateIndex("dbo.AccountValidationWeightings", "ValidationReference");
            AddForeignKey("dbo.AccountValidationWeightings", "ValidationReference", "dbo.AccountValidations", "ValidationReference");
        }
    }
}
