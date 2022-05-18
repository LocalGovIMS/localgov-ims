namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_StopMessage_and_added_StopMessageMetadata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" }, "dbo.StopMessages");
            DropIndex("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" });
            RenameColumn(table: "dbo.AccountHolders", name: "StopMessageReference", newName: "StopMessageId");
            DropPrimaryKey("dbo.StopMessages");
            CreateTable(
                "dbo.StopMessageMetadata",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false),
                        StopMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StopMessages", t => t.StopMessageId)
                .Index(t => t.StopMessageId);
            
            AlterColumn("dbo.AccountHolders", "StopMessageId", c => c.Int());
            AlterColumn("dbo.StopMessages", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StopMessages", "Id");
            CreateIndex("dbo.AccountHolders", "StopMessageId");
            AddForeignKey("dbo.AccountHolders", "StopMessageId", "dbo.StopMessages", "Id");

            RenameTable("dbo.FundMetaData", "FundMetadata");
            RenameTable("dbo.MopMetaData", "MopMetadata");
            RenameTable("dbo.VatMetaData", "VatMetadata");
        }
        
        public override void Down()
        {
            RenameTable("dbo.VatMetadata", "VatMetaData");
            RenameTable("dbo.MopMetadata", "MopMetaData");
            RenameTable("dbo.FundMetadata", "FundMetaData");

            DropForeignKey("dbo.AccountHolders", "StopMessageId", "dbo.StopMessages");
            DropForeignKey("dbo.StopMessageMetadata", "StopMessageId", "dbo.StopMessages");
            DropIndex("dbo.StopMessageMetadata", new[] { "StopMessageId" });
            DropIndex("dbo.AccountHolders", new[] { "StopMessageId" });
            DropPrimaryKey("dbo.StopMessages");
            AlterColumn("dbo.StopMessages", "Id", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.AccountHolders", "StopMessageId", c => c.String(maxLength: 2));
            DropTable("dbo.StopMessageMetadata");
            AddPrimaryKey("dbo.StopMessages", new[] { "Id", "FundCode" });
            RenameColumn(table: "dbo.AccountHolders", name: "StopMessageId", newName: "StopMessageReference");
            CreateIndex("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" });
            AddForeignKey("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" }, "dbo.StopMessages", new[] { "Id", "FundCode" });
        }
    }
}
