namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TransactionImports_and_supporting_entities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImportRows", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.Imports", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.Imports", new[] { "CreatedByUserId" });
            DropIndex("dbo.ImportRows", new[] { "ImportId" });
            CreateTable(
                "dbo.TransactionImports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportTypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        ExternalReference = c.String(maxLength: 100),
                        Description = c.String(maxLength: 255),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalNumberOfTransactions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReversedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImportTypes", t => t.TransactionImportTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.TransactionImportTypeId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.TransactionImportEventLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TransactionImportId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImports", t => t.TransactionImportId, cascadeDelete: true)
                .Index(t => t.TransactionImportId);
            
            CreateTable(
                "dbo.FileImports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportId = c.Int(nullable: false),
                        OriginalFilename = c.String(nullable: false, maxLength: 200),
                        WorkingFilename = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImports", t => t.TransactionImportId)
                .Index(t => t.TransactionImportId);
            
            CreateTable(
                "dbo.FileImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileImportId = c.Int(nullable: false),
                        RowData = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileImports", t => t.FileImportId)
                .Index(t => t.FileImportId);
            
            CreateTable(
                "dbo.TransactionImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportId = c.Int(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImports", t => t.TransactionImportId)
                .Index(t => t.TransactionImportId);
            
            CreateTable(
                "dbo.TransactionImportStatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        TransactionImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImports", t => t.TransactionImportId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.TransactionImportId);
            
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
            
            AddColumn("dbo.ProcessedTransactions", "TransactionImportId", c => c.Int());
            AddColumn("dbo.Suspenses", "TransactionImportId", c => c.Int());
            AddColumn("dbo.ImportProcessingRules", "IsGlobal", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ProcessedTransactions", "TransactionImportId");
            AddForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports", "Id");
            DropColumn("dbo.ProcessedTransactions", "BatchReference");
            DropColumn("dbo.Suspenses", "BatchReference");
            DropTable("dbo.Imports");
            DropTable("dbo.ImportRows");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportId = c.Int(nullable: false),
                        RowData = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchReference = c.String(nullable: false, maxLength: 30),
                        OriginalFilename = c.String(nullable: false, maxLength: 200),
                        WorkingFilename = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Suspenses", "BatchReference", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.ProcessedTransactions", "BatchReference", c => c.String(maxLength: 30));
            DropForeignKey("dbo.TransactionImportStatusHistories", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionImports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionImports", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropForeignKey("dbo.TransactionImportStatusHistories", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportRows", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.FileImports", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.FileImportRows", "FileImportId", "dbo.FileImports");
            DropForeignKey("dbo.TransactionImportEventLogs", "TransactionImportId", "dbo.TransactionImports");
            DropIndex("dbo.TransactionImportTypeImportProcessingRule", new[] { "TransactionImportTypeId", "ImportProcessingRuleId" });
            DropIndex("dbo.TransactionImportStatusHistories", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportStatusHistories", new[] { "CreatedByUserId" });
            DropIndex("dbo.TransactionImportRows", new[] { "TransactionImportId" });
            DropIndex("dbo.FileImportRows", new[] { "FileImportId" });
            DropIndex("dbo.FileImports", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportEventLogs", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImports", new[] { "CreatedByUserId" });
            DropIndex("dbo.TransactionImports", new[] { "TransactionImportTypeId" });
            DropIndex("dbo.ProcessedTransactions", new[] { "TransactionImportId" });
            DropColumn("dbo.ImportProcessingRules", "IsGlobal");
            DropColumn("dbo.Suspenses", "TransactionImportId");
            DropColumn("dbo.ProcessedTransactions", "TransactionImportId");
            DropTable("dbo.TransactionImportTypeImportProcessingRule");
            DropTable("dbo.TransactionImportTypes");
            DropTable("dbo.TransactionImportStatusHistories");
            DropTable("dbo.TransactionImportRows");
            DropTable("dbo.FileImportRows");
            DropTable("dbo.FileImports");
            DropTable("dbo.TransactionImportEventLogs");
            DropTable("dbo.TransactionImports");
            CreateIndex("dbo.ImportRows", "ImportId");
            CreateIndex("dbo.Imports", "CreatedByUserId");
            AddForeignKey("dbo.Imports", "CreatedByUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.ImportRows", "ImportId", "dbo.Imports", "Id");
        }
    }
}
