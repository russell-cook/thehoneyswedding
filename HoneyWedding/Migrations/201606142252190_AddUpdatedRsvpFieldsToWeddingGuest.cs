namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatedRsvpFieldsToWeddingGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UpdatedRsvp", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "UpdatedRsvpDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UpdatedRsvpDate");
            DropColumn("dbo.AspNetUsers", "UpdatedRsvp");
        }
    }
}
