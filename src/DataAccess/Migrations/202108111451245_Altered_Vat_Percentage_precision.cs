namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Altered_Vat_Percentage_precision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vat", "Percentage", c => c.Decimal(precision: 18, scale: 3));
        }

        public override void Down()
        {
            AlterColumn("dbo.Vat", "Percentage", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
