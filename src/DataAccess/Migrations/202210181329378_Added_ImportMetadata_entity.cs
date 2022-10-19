namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ImportMetadata_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportMetadata",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetadataKeyId = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        ImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId, cascadeDelete: true)
                .ForeignKey("dbo.MetadataKeys", t => t.MetadataKeyId, cascadeDelete: true)
                .Index(t => new { t.ImportId, t.MetadataKeyId }, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImportMetadata", "MetadataKeyId", "dbo.MetadataKeys");
            DropForeignKey("dbo.ImportMetadata", "ImportId", "dbo.Imports");
            DropIndex("dbo.ImportMetadata", new[] { "ImportId", "MetadataKeyId" });
            DropTable("dbo.ImportMetadata");
        }
    }
}
