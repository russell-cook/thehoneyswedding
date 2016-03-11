namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CanAttend", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "DidRsvp", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "RsvpDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "HasPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "RsvpPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Notes", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailSent", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "EmailSent");
            DropColumn("dbo.AspNetUsers", "Notes");
            DropColumn("dbo.AspNetUsers", "RsvpPlusOne");
            DropColumn("dbo.AspNetUsers", "HasPlusOne");
            DropColumn("dbo.AspNetUsers", "RsvpDate");
            DropColumn("dbo.AspNetUsers", "DidRsvp");
            DropColumn("dbo.AspNetUsers", "CanAttend");
        }
    }
}
