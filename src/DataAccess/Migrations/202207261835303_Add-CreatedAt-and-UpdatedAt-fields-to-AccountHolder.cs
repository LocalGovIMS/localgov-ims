namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtandUpdatedAtfieldstoAccountHolder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountHolders", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AccountHolders", "CreatedByUserId", c => c.Int(nullable: false));
            AddColumn("dbo.AccountHolders", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AccountHolders", "UpdatedByUserId", c => c.Int());
            CreateIndex("dbo.AccountHolders", "CreatedByUserId");
            CreateIndex("dbo.AccountHolders", "UpdatedByUserId");
            AddForeignKey("dbo.AccountHolders", "CreatedByUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.AccountHolders", "UpdatedByUserId", "dbo.Users", "UserId");
            DropColumn("dbo.AccountHolders", "LastUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountHolders", "LastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.AccountHolders", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.AccountHolders", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.AccountHolders", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AccountHolders", new[] { "CreatedByUserId" });
            DropColumn("dbo.AccountHolders", "UpdatedByUserId");
            DropColumn("dbo.AccountHolders", "UpdatedAt");
            DropColumn("dbo.AccountHolders", "CreatedByUserId");
            DropColumn("dbo.AccountHolders", "CreatedAt");
        }
    }
}
