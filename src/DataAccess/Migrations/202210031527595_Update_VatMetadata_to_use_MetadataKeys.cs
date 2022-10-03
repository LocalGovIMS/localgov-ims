namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_VatMetadata_to_use_MetadataKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.VatMetadata", new[] { "VatCode" });
            AddColumn("dbo.VatMetadata", "MetadataKeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.VatMetadata", new[] { "VatCode", "MetadataKeyId" }, unique: true);
            AddForeignKey("dbo.VatMetadata", "MetadataKeyId", "dbo.MetadataKeys", "Id", cascadeDelete: true);
            DropColumn("dbo.VatMetadata", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VatMetadata", "Key", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.VatMetadata", "MetadataKeyId", "dbo.MetadataKeys");
            DropIndex("dbo.VatMetadata", new[] { "VatCode", "MetadataKeyId" });
            DropColumn("dbo.VatMetadata", "MetadataKeyId");
            CreateIndex("dbo.VatMetadata", "VatCode");
        }
    }
}
