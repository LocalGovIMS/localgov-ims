namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_from_StopMessages_to_FundMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StopMessages", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" }, "dbo.StopMessages");
            DropIndex("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" });
            DropIndex("dbo.StopMessages", new[] { "FundCode" });
            CreateTable(
                "dbo.FundMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 5),
                        Message = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.FundCode)
                .Index(t => t.FundCode);
            
            CreateTable(
                "dbo.FundMessageMetadata",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false),
                        FundMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FundMessages", t => t.FundMessageId)
                .Index(t => t.FundMessageId);
            
            AddColumn("dbo.AccountHolders", "FundMessageId", c => c.Int());
            CreateIndex("dbo.AccountHolders", "FundMessageId");
            AddForeignKey("dbo.AccountHolders", "FundMessageId", "dbo.FundMessages", "Id");
            DropColumn("dbo.AccountHolders", "StopMessageReference");
            DropTable("dbo.StopMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StopMessages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 2),
                        FundCode = c.String(nullable: false, maxLength: 5),
                        Message = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => new { t.Id, t.FundCode });
            
            AddColumn("dbo.AccountHolders", "StopMessageReference", c => c.String(maxLength: 2));
            DropForeignKey("dbo.AccountHolders", "FundMessageId", "dbo.FundMessages");
            DropForeignKey("dbo.FundMessages", "FundCode", "dbo.Funds");
            DropForeignKey("dbo.FundMessageMetadata", "FundMessageId", "dbo.FundMessages");
            DropIndex("dbo.FundMessageMetadata", new[] { "FundMessageId" });
            DropIndex("dbo.FundMessages", new[] { "FundCode" });
            DropIndex("dbo.AccountHolders", new[] { "FundMessageId" });
            DropColumn("dbo.AccountHolders", "FundMessageId");
            DropTable("dbo.FundMessageMetadata");
            DropTable("dbo.FundMessages");
            CreateIndex("dbo.StopMessages", "FundCode");
            CreateIndex("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" });
            AddForeignKey("dbo.AccountHolders", new[] { "StopMessageReference", "FundCode" }, "dbo.StopMessages", new[] { "Id", "FundCode" });
            AddForeignKey("dbo.StopMessages", "FundCode", "dbo.Funds", "FundCode");
        }
    }
}
