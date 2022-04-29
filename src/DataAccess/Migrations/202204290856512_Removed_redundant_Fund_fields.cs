namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_redundant_Fund_fields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Funds", "AccessLevel");
            DropColumn("dbo.Funds", "NarrativeFlag");
            DropColumn("dbo.Funds", "ExportToFund");
            DropColumn("dbo.Funds", "ExportToLedger");
            DropColumn("dbo.Funds", "FundExportFormat");
            DropColumn("dbo.Funds", "UseGeneralLedgerCode");
            DropColumn("dbo.Funds", "GeneralLedgerCode");
            DropColumn("dbo.Funds", "LedgerDetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funds", "LedgerDetail", c => c.Boolean());
            AddColumn("dbo.Funds", "GeneralLedgerCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Funds", "UseGeneralLedgerCode", c => c.Boolean(nullable: false));
            AddColumn("dbo.Funds", "FundExportFormat", c => c.String(maxLength: 10));
            AddColumn("dbo.Funds", "ExportToLedger", c => c.Boolean(nullable: false));
            AddColumn("dbo.Funds", "ExportToFund", c => c.Boolean(nullable: false));
            AddColumn("dbo.Funds", "NarrativeFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Funds", "AccessLevel", c => c.String(maxLength: 2));
        }
    }
}
