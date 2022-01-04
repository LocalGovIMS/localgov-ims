namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_SuspenseNote_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuspenseNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuspenseId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suspenses", t => t.SuspenseId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.SuspenseId)
                .Index(t => t.CreatedByUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuspenseNotes", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.SuspenseNotes", "SuspenseId", "dbo.Suspenses");
            DropIndex("dbo.SuspenseNotes", new[] { "CreatedByUserId" });
            DropIndex("dbo.SuspenseNotes", new[] { "SuspenseId" });
            DropTable("dbo.SuspenseNotes");
        }
    }
}
