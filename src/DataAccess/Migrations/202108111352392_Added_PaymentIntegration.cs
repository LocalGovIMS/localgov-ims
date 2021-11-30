namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Added_PaymentIntegration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentIntegrations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 100),
                    BaseUri = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.PaymentIntegrations");
        }
    }
}
