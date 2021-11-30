namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ApplicationLog_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Thread = c.String(maxLength: 255),
                        Level = c.String(maxLength: 50),
                        Logger = c.String(maxLength: 255),
                        Location = c.String(maxLength: 500),
                        Message = c.String(maxLength: 4000),
                        Exception = c.String(),
                        HostName = c.String(maxLength: 255),
                        ApplicationName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationLogs");
        }
    }
}
