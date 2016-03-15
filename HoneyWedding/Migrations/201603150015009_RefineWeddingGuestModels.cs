namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefineWeddingGuestModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PlusOneCanAtend", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "RsvpPlusOne");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RsvpPlusOne", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "PlusOneCanAtend");
        }
    }
}
