namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlusOneIsKnownToWeddingGuestModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PlusOneIsKnown", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PlusOneIsKnown");
        }
    }
}
