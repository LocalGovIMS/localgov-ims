namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TransactionImportType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionImportTypeImportProcessingRule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportTypeId = c.Int(nullable: false),
                        ImportProcessingRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportProcessingRules", t => t.ImportProcessingRuleId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionImportTypes", t => t.TransactionImportTypeId, cascadeDelete: true)
                .Index(t => new { t.TransactionImportTypeId, t.ImportProcessingRuleId }, unique: true);
            
            CreateTable(
                "dbo.TransactionImportTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 255),
                        ExternalReference = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ImportProcessingRules", "IsGlobal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropIndex("dbo.TransactionImportTypeImportProcessingRule", new[] { "TransactionImportTypeId", "ImportProcessingRuleId" });
            DropColumn("dbo.ImportProcessingRules", "IsGlobal");
            DropTable("dbo.TransactionImportTypes");
            DropTable("dbo.TransactionImportTypeImportProcessingRule");
        }
    }
}
