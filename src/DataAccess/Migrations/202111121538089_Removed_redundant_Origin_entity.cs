namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Removed_redundant_Origin_entity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Origins", "FundCode", "dbo.Funds");
            DropIndex("dbo.Origins", new[] { "FundCode" });
            DropTable("dbo.Origins");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Origins",
                c => new
                {
                    OriginId = c.Int(nullable: false, identity: true),
                    OriginName = c.String(nullable: false, maxLength: 50),
                    FundCode = c.String(nullable: false, maxLength: 5),
                    MessageName = c.String(maxLength: 100),
                    ReferenceFieldLabel = c.String(maxLength: 50),
                    ReferenceFieldMessage = c.String(maxLength: 80),
                })
                .PrimaryKey(t => t.OriginId);

            CreateIndex("dbo.Origins", "FundCode");
            AddForeignKey("dbo.Origins", "FundCode", "dbo.Funds", "FundCode");
        }
    }
}
