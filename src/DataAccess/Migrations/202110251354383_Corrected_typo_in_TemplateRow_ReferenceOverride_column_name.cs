namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Corrected_typo_in_TemplateRow_ReferenceOverride_column_name : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.TemplateRows", "ReferrenceOverride", "ReferenceOverride");
        }

        public override void Down()
        {
            RenameColumn("dbo.TemplateRows", "ReferenceOverride", "ReferrenceOverride");
        }
    }
}
