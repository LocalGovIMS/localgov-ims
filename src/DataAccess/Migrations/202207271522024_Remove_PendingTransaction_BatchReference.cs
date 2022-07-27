namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_PendingTransaction_BatchReference : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PendingTransactions", "BatchReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PendingTransactions", "BatchReference", c => c.String(maxLength: 30));
        }
    }
}
