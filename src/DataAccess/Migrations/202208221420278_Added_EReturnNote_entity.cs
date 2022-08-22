namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_EReturnNote_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EReturnNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EReturnId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EReturns", t => t.EReturnId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.EReturnId)
                .Index(t => t.CreatedByUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EReturnNotes", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.EReturnNotes", "EReturnId", "dbo.EReturns");
            DropIndex("dbo.EReturnNotes", new[] { "CreatedByUserId" });
            DropIndex("dbo.EReturnNotes", new[] { "EReturnId" });
            DropTable("dbo.EReturnNotes");
        }
    }
}
