namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_FundMessageMetadata_to_use_MetadataKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FundMessageMetadata", new[] { "FundMessageId" });
            AddColumn("dbo.FundMessageMetadata", "MetadataKeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.FundMessageMetadata", new[] { "FundMessageId", "MetadataKeyId" }, unique: true);
            AddForeignKey("dbo.FundMessageMetadata", "MetadataKeyId", "dbo.MetadataKeys", "Id", cascadeDelete: true);
            DropColumn("dbo.FundMessageMetadata", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FundMessageMetadata", "Key", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.FundMessageMetadata", "MetadataKeyId", "dbo.MetadataKeys");
            DropIndex("dbo.FundMessageMetadata", new[] { "FundMessageId", "MetadataKeyId" });
            DropColumn("dbo.FundMessageMetadata", "MetadataKeyId");
            CreateIndex("dbo.FundMessageMetadata", "FundMessageId");
        }
    }
}
