namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_FundMetadata_to_use_MetadataKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FundMetadata", new[] { "FundCode" });
            AddColumn("dbo.FundMetadata", "MetadataKeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.FundMetadata", new[] { "FundCode", "MetadataKeyId" }, unique: true);
            AddForeignKey("dbo.FundMetadata", "MetadataKeyId", "dbo.MetadataKeys", "Id", cascadeDelete: true);
            DropColumn("dbo.FundMetadata", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FundMetadata", "Key", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.FundMetadata", "MetadataKeyId", "dbo.MetadataKeys");
            DropIndex("dbo.FundMetadata", new[] { "FundCode", "MetadataKeyId" });
            DropColumn("dbo.FundMetadata", "MetadataKeyId");
            CreateIndex("dbo.FundMetadata", "FundCode");
        }
    }
}
