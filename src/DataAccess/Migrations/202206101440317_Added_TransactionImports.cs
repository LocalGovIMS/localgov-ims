namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TransactionImports : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileImports", "FK_dbo.Imports_dbo.Users_CreatedByUserId");
            DropIndex("dbo.FileImports", new[] { "CreatedByUserId" });
            CreateTable(
                "dbo.TransactionImports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionImportTypeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                        ExternalReference = c.String(maxLength: 100),
                        Description = c.String(maxLength: 255),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalNumberOfTransactions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfErrors = c.Int(nullable: false),
                        ReversedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        TransactionImportId = c.Int(nullable: false),
                        TransactionImportRowId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Type = c.String(),
                        Description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionImportRows", t => t.TransactionImportRowId)
                .ForeignKey("dbo.TransactionImports", t => t.TransactionImportId)
                .Index(t => t.TransactionImportId)
                .Index(t => t.TransactionImportRowId);
            
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
            
            AddColumn("dbo.ProcessedTransactions", "TransactionImportId", c => c.Int());
            AddColumn("dbo.Suspenses", "TransactionImportId", c => c.Int());
            AddColumn("dbo.FileImports", "TransactionImportId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessedTransactions", "TransactionImportId");
            CreateIndex("dbo.FileImports", "TransactionImportId");
            AddForeignKey("dbo.FileImports", "TransactionImportId", "dbo.TransactionImports", "Id");
            AddForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports", "Id");
            DropColumn("dbo.ProcessedTransactions", "BatchReference");
            DropColumn("dbo.Suspenses", "BatchReference");
            DropColumn("dbo.FileImports", "BatchReference");
            DropColumn("dbo.FileImports", "CreatedAt");
            DropColumn("dbo.FileImports", "CreatedByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileImports", "CreatedByUserId", c => c.Int(nullable: false));
            AddColumn("dbo.FileImports", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.FileImports", "BatchReference", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Suspenses", "BatchReference", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.ProcessedTransactions", "BatchReference", c => c.String(maxLength: 30));
            DropForeignKey("dbo.TransactionImports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionImports", "TransactionImportTypeId", "dbo.TransactionImportTypes");
            DropForeignKey("dbo.TransactionImportRows", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.ProcessedTransactions", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.FileImports", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportEventLogs", "TransactionImportId", "dbo.TransactionImports");
            DropForeignKey("dbo.TransactionImportEventLogs", "TransactionImportRowId", "dbo.TransactionImportRows");
            DropIndex("dbo.FileImports", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportRows", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImportEventLogs", new[] { "TransactionImportRowId" });
            DropIndex("dbo.TransactionImportEventLogs", new[] { "TransactionImportId" });
            DropIndex("dbo.TransactionImports", new[] { "CreatedByUserId" });
            DropIndex("dbo.TransactionImports", new[] { "TransactionImportTypeId" });
            DropIndex("dbo.ProcessedTransactions", new[] { "TransactionImportId" });
            DropColumn("dbo.FileImports", "TransactionImportId");
            DropColumn("dbo.Suspenses", "TransactionImportId");
            DropColumn("dbo.ProcessedTransactions", "TransactionImportId");
            DropTable("dbo.TransactionImportRows");
            DropTable("dbo.TransactionImportEventLogs");
            DropTable("dbo.TransactionImports");
            CreateIndex("dbo.FileImports", "CreatedByUserId");
            AddForeignKey("dbo.FileImports", "CreatedByUserId", "dbo.Users", "UserId", name: "FK_dbo.Imports_dbo.Users_CreatedByUserId");
        }
    }
}
