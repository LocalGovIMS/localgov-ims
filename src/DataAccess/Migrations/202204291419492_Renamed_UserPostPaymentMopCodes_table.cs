namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_UserPostPaymentMopCodes_table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserPostPaymentMopCodes", newName: "UserMopCodes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserMopCodes", newName: "UserPostPaymentMopCodes");
        }
    }
}
