namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Added_ImportProcessingRules_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportProcessingRuleActions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ImportProcessingRuleId = c.Int(nullable: false),
                    ImportProcessingRuleFieldId = c.Int(nullable: false),
                    Value = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportProcessingRuleFields", t => t.ImportProcessingRuleFieldId)
                .ForeignKey("dbo.ImportProcessingRules", t => t.ImportProcessingRuleId)
                .Index(t => t.ImportProcessingRuleId)
                .Index(t => t.ImportProcessingRuleFieldId);

            CreateTable(
                "dbo.ImportProcessingRuleFields",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    DisplayName = c.String(nullable: false, maxLength: 50),
                    Type = c.String(nullable: false, maxLength: 50),
                    DisplayOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ImportProcessingRuleConditions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ImportProcessingRuleId = c.Int(nullable: false),
                    Group = c.Int(nullable: false),
                    ImportProcessingRuleFieldId = c.Int(nullable: false),
                    ImportProcessingRuleOperatorId = c.Int(nullable: false),
                    Value = c.String(),
                    LogicalOperator = c.String(maxLength: 3),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportProcessingRuleOperators", t => t.ImportProcessingRuleOperatorId)
                .ForeignKey("dbo.ImportProcessingRules", t => t.ImportProcessingRuleId)
                .ForeignKey("dbo.ImportProcessingRuleFields", t => t.ImportProcessingRuleFieldId)
                .Index(t => t.ImportProcessingRuleId)
                .Index(t => t.ImportProcessingRuleFieldId)
                .Index(t => t.ImportProcessingRuleOperatorId);

            CreateTable(
                "dbo.ImportProcessingRuleOperators",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    DisplayName = c.String(nullable: false, maxLength: 50),
                    Type = c.String(nullable: false, maxLength: 50),
                    DisplayOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ImportProcessingRules",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(nullable: false),
                    Disabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ImportProcessingRuleConditions", "ImportProcessingRuleFieldId", "dbo.ImportProcessingRuleFields");
            DropForeignKey("dbo.ImportProcessingRuleConditions", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropForeignKey("dbo.ImportProcessingRuleActions", "ImportProcessingRuleId", "dbo.ImportProcessingRules");
            DropForeignKey("dbo.ImportProcessingRuleConditions", "ImportProcessingRuleOperatorId", "dbo.ImportProcessingRuleOperators");
            DropForeignKey("dbo.ImportProcessingRuleActions", "ImportProcessingRuleFieldId", "dbo.ImportProcessingRuleFields");
            DropIndex("dbo.ImportProcessingRuleConditions", new[] { "ImportProcessingRuleOperatorId" });
            DropIndex("dbo.ImportProcessingRuleConditions", new[] { "ImportProcessingRuleFieldId" });
            DropIndex("dbo.ImportProcessingRuleConditions", new[] { "ImportProcessingRuleId" });
            DropIndex("dbo.ImportProcessingRuleActions", new[] { "ImportProcessingRuleFieldId" });
            DropIndex("dbo.ImportProcessingRuleActions", new[] { "ImportProcessingRuleId" });
            DropTable("dbo.ImportProcessingRules");
            DropTable("dbo.ImportProcessingRuleOperators");
            DropTable("dbo.ImportProcessingRuleConditions");
            DropTable("dbo.ImportProcessingRuleFields");
            DropTable("dbo.ImportProcessingRuleActions");
        }
    }
}
