namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ProcessedTransaction_Card_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessedTransactions", "CardPrefix", c => c.String(maxLength: 6));
            AddColumn("dbo.ProcessedTransactions", "CardSuffix", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessedTransactions", "CardSuffix");
            DropColumn("dbo.ProcessedTransactions", "CardPrefix");
        }
    }
}
