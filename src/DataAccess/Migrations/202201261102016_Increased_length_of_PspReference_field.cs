namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Increased_length_of_PspReference_field : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProcessedTransactions", new[] { "PspReference" });
            AlterColumn("dbo.ProcessedTransactions", "PspReference", c => c.String(maxLength: 30));
            CreateIndex("dbo.ProcessedTransactions", "PspReference");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProcessedTransactions", new[] { "PspReference" });
            AlterColumn("dbo.ProcessedTransactions", "PspReference", c => c.String(maxLength: 16));
            CreateIndex("dbo.ProcessedTransactions", "PspReference");
        }
    }
}
