namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveredundantPendingTransactionfield : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PendingTransactions", "Legacy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PendingTransactions", "Legacy", c => c.Boolean());
        }
    }
}
