namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_CheckDigitConfiguration_Name_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckDigitConfigurations", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckDigitConfigurations", "Name");
        }
    }
}
