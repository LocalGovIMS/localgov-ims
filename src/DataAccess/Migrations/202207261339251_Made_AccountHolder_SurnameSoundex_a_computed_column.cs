namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Made_AccountHolder_SurnameSoundex_a_computed_column : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountHolders", "SurnameSoundex", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountHolders", "SurnameSoundex", c => c.String(maxLength: 50));
        }
    }
}
