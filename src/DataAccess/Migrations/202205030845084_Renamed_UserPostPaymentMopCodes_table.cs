namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_UserPostPaymentMopCodes_table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserPostPaymentMopCodes", newName: "UserMethodOfPayments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserMethodOfPayments", newName: "UserPostPaymentMopCodes");
        }
    }
}
