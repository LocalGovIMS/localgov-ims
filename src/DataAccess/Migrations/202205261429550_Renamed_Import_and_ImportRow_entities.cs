namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_Import_and_ImportRow_entities : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Imports", newName: "FileImports");
            RenameTable(name: "dbo.ImportRows", newName: "FileImportRows");
            RenameColumn(table: "dbo.FileImportRows", name: "ImportId", newName: "FileImportId");
            RenameIndex(table: "dbo.FileImportRows", name: "IX_ImportId", newName: "IX_FileImportId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FileImportRows", name: "IX_FileImportId", newName: "IX_ImportId");
            RenameColumn(table: "dbo.FileImportRows", name: "FileImportId", newName: "ImportId");
            RenameTable(name: "dbo.FileImportRows", newName: "ImportRows");
            RenameTable(name: "dbo.FileImports", newName: "Imports");
        }
    }
}
