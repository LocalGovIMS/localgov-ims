namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_and_refactored_TransactionImport_entities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionImportEventLogs", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.FileImports", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportRows", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportStatusHistories", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropForeignKey("dbo.TransactionImportTypeImportProcessingRule", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImports", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionImportStatusHistories", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.ProcessedTransactions", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImports", new[] { "TransactionImportTypeId" });
            DropIndex("dbo.TransactionImports", new[] { "CreatedByUserId" });
            DropIndex("dbo.TransactionImportEventLogs", new[] { "TransactionImportId" });
            DropIndex("dbo.FileImports", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportRows", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportStatusHistories", new[] { "CreatedByUserId" });
            DropIndex("dbo.TransactionImportStatusHistories", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportTypeImportProcessingRule", new[] { "TransactionImportTypeId", "ImportProcessingRuleId" });
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportTypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        Notes = c.String(maxLength: 255),
                        NumberOfRows = c.Int(nullable: false),
                        ReversedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportTypes", t => t.ImportTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.ImportTypeId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.ImportEventLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ImportId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId, cascadeDelete: true)
                .Index(t => t.ImportId);
            
            CreateTable(
                "dbo.ImportTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataType = c.Byte(nullable: false),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 255),
                        ExternalReference = c.String(maxLength: 100),
                        IsReversible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImportTypeImportProcessingRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportTypeId = c.Int(nullable: false),
                        ImportProcessingRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportProcessingRules", t => t.ImportProcessingRuleId, cascadeDelete: true)
                .ForeignKey("dbo.ImportTypes", t => t.ImportTypeId, cascadeDelete: true)
                .Index(t => new { t.ImportTypeId, t.ImportProcessingRuleId }, unique: true);
            
            CreateTable(
                "dbo.ImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportId = c.Int(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId)
                .Index(t => t.ImportId);
            
            CreateTable(
                "dbo.ImportStatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        ImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ImportId);
            
            AddColumn("dbo.ProcessedTransactions", "ImportId", c => c.Int());
            AddColumn("dbo.Suspenses", "ImportId", c => c.Int());
            AddColumn("dbo.FileImports", "ImportId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessedTransactions", "ImportId");
            CreateIndex("dbo.FileImports", "ImportId");
            CreateIndex("dbo.Suspenses", "ImportId");
            AddForeignKey("dbo.FileImports", "ImportId", "dbo.Imports", "Id");
            AddForeignKey("dbo.ProcessedTransactions", "ImportId", "dbo.Imports", "Id");
            AddForeignKey("dbo.Suspenses", "ImportId", "dbo.Imports", "Id");
            DropColumn("dbo.ProcessedTransactions", "TransactionImportId");
            DropColumn("dbo.Suspenses", "TransactionImportId");
            DropColumn("dbo.FileImports", "TransactionImportId");
            DropTable("dbo.TransactionImports");
            DropTable("dbo.TransactionImportEventLogs");
            DropTable("dbo.TransactionImportRows");
            DropTable("dbo.TransactionImportStatusHistories");
            DropTable("dbo.TransactionImportTypes");
            DropTable("dbo.TransactionImportTypeImportProcessingRule");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransactionImportTypeImportProcessingRule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportTypeId = c.Int(nullable: false),
                        ImportProcessingRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.TransactionImportStatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        TransactionImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportId = c.Int(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FileImports", "TransactionImportId", c => c.Int(nullable: false));
            AddColumn("dbo.Suspenses", "TransactionImportId", c => c.Int());
            AddColumn("dbo.ProcessedTransactions", "TransactionImportId", c => c.Int());
            DropForeignKey("dbo.ImportStatusHistories", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Imports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Suspenses", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.ImportStatusHistories", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.ImportRows", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.ProcessedTransactions", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.Imports", "ImportTypeId", "dbo.ImportTypes");
            DropForeignKey("dbo.ImportTypeImportProcessingRules", "ImportTypeId", "dbo.ImportTypes");
            DropForeignKey("dbo.ImportTypeImportProcessingRules", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropForeignKey("dbo.FileImports", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.ImportEventLogs", "ImportId", "dbo.Imports");
            DropIndex("dbo.Suspenses", new[] { "ImportId" });
            DropIndex("dbo.ImportStatusHistories", new[] { "ImportId" });
            DropIndex("dbo.ImportStatusHistories", new[] { "CreatedByUserId" });
            DropIndex("dbo.ImportRows", new[] { "ImportId" });
            DropIndex("dbo.ImportTypeImportProcessingRules", new[] { "ImportTypeId", "ImportProcessingRuleId" });
            DropIndex("dbo.FileImports", new[] { "ImportId" });
            DropIndex("dbo.ImportEventLogs", new[] { "ImportId" });
            DropIndex("dbo.Imports", new[] { "CreatedByUserId" });
            DropIndex("dbo.Imports", new[] { "ImportTypeId" });
            DropIndex("dbo.ProcessedTransactions", new[] { "ImportId" });
            DropColumn("dbo.FileImports", "ImportId");
            DropColumn("dbo.Suspenses", "ImportId");
            DropColumn("dbo.ProcessedTransactions", "ImportId");
            DropTable("dbo.ImportStatusHistories");
            DropTable("dbo.ImportRows");
            DropTable("dbo.ImportTypeImportProcessingRules");
            DropTable("dbo.ImportTypes");
            DropTable("dbo.ImportEventLogs");
            DropTable("dbo.Imports");
            CreateIndex("dbo.TransactionImportTypeImportProcessingRule", new[] { "TransactionImportTypeId", "ImportProcessingRuleId" }, unique: true);
            CreateIndex("dbo.TransactionImportStatusHistories", "TransactionImportId");
            CreateIndex("dbo.TransactionImportStatusHistories", "CreatedByUserId");
            CreateIndex("dbo.TransactionImportRows", "TransactionImportId");
            CreateIndex("dbo.FileImports", "TransactionImportId");
            CreateIndex("dbo.TransactionImportEventLogs", "TransactionImportId");
            CreateIndex("dbo.TransactionImports", "CreatedByUserId");
            CreateIndex("dbo.TransactionImports", "TransactionImportTypeId");
            CreateIndex("dbo.ProcessedTransactions", "TransactionImportId");
            AddForeignKey("dbo.TransactionImportStatusHistories", "CreatedByUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.TransactionImports", "CreatedByUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.TransactionImports", "TransactionImportTypeId", "dbo.TransactionImportTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionImportTypeImportProcessingRule", "TransactionImportTypeId", "dbo.TransactionImportTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionImportTypeImportProcessingRule", "ImportProcessingRuleId", "dbo.ImportProcessingRules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionImportStatusHistories", "TransactionImportId", "dbo.TransactionImports", "Id");
            AddForeignKey("dbo.TransactionImportRows", "TransactionImportId", "dbo.TransactionImports", "Id");
            AddForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports", "Id");
            AddForeignKey("dbo.FileImports", "TransactionImportId", "dbo.TransactionImports", "Id");
            AddForeignKey("dbo.TransactionImportEventLogs", "TransactionImportId", "dbo.TransactionImports", "Id", cascadeDelete: true);
        }
    }
}
