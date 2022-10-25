namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Standardised_address_columns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingTransactions", "CardHolderAddressLine1", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderAddressLine2", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderAddressLine3", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderAddressLine4", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderAddressLine1", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderAddressLine2", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderAddressLine3", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderAddressLine4", c => c.String(maxLength: 50));
            DropColumn("dbo.PendingTransactions", "MerchantNumber");
            DropColumn("dbo.PendingTransactions", "CardHolderBusinessName");
            DropColumn("dbo.PendingTransactions", "CardHolderPremiseNumber");
            DropColumn("dbo.PendingTransactions", "CardHolderPremiseName");
            DropColumn("dbo.PendingTransactions", "CardHolderStreet");
            DropColumn("dbo.PendingTransactions", "CardHolderArea");
            DropColumn("dbo.PendingTransactions", "CardHolderTown");
            DropColumn("dbo.PendingTransactions", "CardHolderCounty");
            DropColumn("dbo.ProcessedTransactions", "MerchantNumber");
            DropColumn("dbo.ProcessedTransactions", "CardHolderBusinessName");
            DropColumn("dbo.ProcessedTransactions", "CardHolderPremiseNumber");
            DropColumn("dbo.ProcessedTransactions", "CardHolderPremiseName");
            DropColumn("dbo.ProcessedTransactions", "CardHolderStreet");
            DropColumn("dbo.ProcessedTransactions", "CardHolderArea");
            DropColumn("dbo.ProcessedTransactions", "CardHolderTown");
            DropColumn("dbo.ProcessedTransactions", "CardHolderCounty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessedTransactions", "CardHolderCounty", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderTown", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderArea", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderStreet", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderPremiseName", c => c.String(maxLength: 100));
            AddColumn("dbo.ProcessedTransactions", "CardHolderPremiseNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.ProcessedTransactions", "CardHolderBusinessName", c => c.String(maxLength: 100));
            AddColumn("dbo.ProcessedTransactions", "MerchantNumber", c => c.String(maxLength: 30));
            AddColumn("dbo.PendingTransactions", "CardHolderCounty", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderTown", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderArea", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderStreet", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderPremiseName", c => c.String(maxLength: 100));
            AddColumn("dbo.PendingTransactions", "CardHolderPremiseNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.PendingTransactions", "CardHolderBusinessName", c => c.String(maxLength: 100));
            AddColumn("dbo.PendingTransactions", "MerchantNumber", c => c.String(maxLength: 30));
            DropColumn("dbo.ProcessedTransactions", "CardHolderAddressLine4");
            DropColumn("dbo.ProcessedTransactions", "CardHolderAddressLine3");
            DropColumn("dbo.ProcessedTransactions", "CardHolderAddressLine2");
            DropColumn("dbo.ProcessedTransactions", "CardHolderAddressLine1");
            DropColumn("dbo.PendingTransactions", "CardHolderAddressLine4");
            DropColumn("dbo.PendingTransactions", "CardHolderAddressLine3");
            DropColumn("dbo.PendingTransactions", "CardHolderAddressLine2");
            DropColumn("dbo.PendingTransactions", "CardHolderAddressLine1");
        }
    }
}
