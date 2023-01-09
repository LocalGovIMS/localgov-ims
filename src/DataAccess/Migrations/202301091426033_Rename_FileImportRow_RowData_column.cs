namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_FileImportRow_RowData_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileImportRows", "Data", c => c.String(nullable: false));
            DropColumn("dbo.FileImportRows", "RowData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileImportRows", "RowData", c => c.String(nullable: false));
            DropColumn("dbo.FileImportRows", "Data");
        }
    }
}
