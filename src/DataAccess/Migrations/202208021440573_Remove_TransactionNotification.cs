namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_TransactionNotification : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TransactionNotifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransactionNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MerchantReference = c.String(maxLength: 100),
                        EventCode = c.String(maxLength: 100),
                        OriginalReference = c.String(maxLength: 1000),
                        PspReference = c.String(maxLength: 1000),
                        EventDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PaymentMethod = c.String(maxLength: 100),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Success = c.Boolean(nullable: false),
                        Reason = c.String(maxLength: 1000),
                        Operations = c.String(maxLength: 100),
                        Live = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        MerchantAccountCode = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
