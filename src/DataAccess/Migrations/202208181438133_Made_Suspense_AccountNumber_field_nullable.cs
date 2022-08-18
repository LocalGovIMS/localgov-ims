namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Made_Suspense_AccountNumber_field_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suspenses", "AccountNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suspenses", "AccountNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
