namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_MopMetadataKey_entity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MopMetadata", new[] { "MopCode" });
            CreateTable(
                "dbo.MopMetadataKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.MopMetadata", "MopMetadataKeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.MopMetadata", new[] { "MopMetadataKeyId", "MopCode" }, unique: true);
            AddForeignKey("dbo.MopMetadata", "MopMetadataKeyId", "dbo.MopMetadataKeys", "Id", cascadeDelete: true);
            DropColumn("dbo.MopMetadata", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MopMetadata", "Key", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.MopMetadata", "MopMetadataKeyId", "dbo.MopMetadataKeys");
            DropIndex("dbo.MopMetadataKeys", new[] { "Name" });
            DropIndex("dbo.MopMetadata", new[] { "MopMetadataKeyId", "MopCode" });
            DropColumn("dbo.MopMetadata", "MopMetadataKeyId");
            DropTable("dbo.MopMetadataKeys");
            CreateIndex("dbo.MopMetadata", "MopCode");
        }
    }
}
