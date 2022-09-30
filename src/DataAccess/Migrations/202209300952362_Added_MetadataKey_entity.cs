namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_MetadataKey_entity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MopMetadata", new[] { "MopCode" });
            CreateTable(
                "dbo.MetadataKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        SystemType = c.Boolean(nullable: false),
                        EntityType = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Name, t.EntityType }, unique: true);
            
            AddColumn("dbo.MopMetadata", "MetadataKeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.MopMetadata", new[] { "MopCode", "MetadataKeyId" }, unique: true);
            AddForeignKey("dbo.MopMetadata", "MetadataKeyId", "dbo.MetadataKeys", "Id", cascadeDelete: true);
            DropColumn("dbo.MopMetadata", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MopMetadata", "Key", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.MopMetadata", "MetadataKeyId", "dbo.MetadataKeys");
            DropIndex("dbo.MetadataKeys", new[] { "Name", "EntityType" });
            DropIndex("dbo.MopMetadata", new[] { "MopCode", "MetadataKeyId" });
            DropColumn("dbo.MopMetadata", "MetadataKeyId");
            DropTable("dbo.MetadataKeys");
            CreateIndex("dbo.MopMetadata", "MopCode");
        }
    }
}
