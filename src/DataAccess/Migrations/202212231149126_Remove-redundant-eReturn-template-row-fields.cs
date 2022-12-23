namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveredundanteReturntemplaterowfields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Templates", "ModifyVat");
            DropColumn("dbo.Templates", "ModifyReference");
            DropColumn("dbo.Templates", "ModifyDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "ModifyDescription", c => c.Boolean(nullable: false));
            AddColumn("dbo.Templates", "ModifyReference", c => c.Boolean(nullable: false));
            AddColumn("dbo.Templates", "ModifyVat", c => c.Boolean(nullable: false));
        }
    }
}
