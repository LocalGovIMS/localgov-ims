namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Import_entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchReference = c.String(nullable: false, maxLength: 30),
                        OriginalFilename = c.String(nullable: false, maxLength: 200),
                        WorkingFilename = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.ImportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportId = c.Int(nullable: false),
                        RowData = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId)
                .Index(t => t.ImportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ImportRows", "ImportId", "dbo.Imports");
            DropIndex("dbo.ImportRows", new[] { "ImportId" });
            DropIndex("dbo.Imports", new[] { "CreatedByUserId" });
            DropTable("dbo.ImportRows");
            DropTable("dbo.Imports");
        }
    }
}
