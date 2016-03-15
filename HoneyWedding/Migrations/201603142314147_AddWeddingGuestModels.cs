namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeddingGuestModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CanAttend", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "DidRsvp", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "RsvpDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "HasPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "RsvpPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Notes", c => c.String());
            AddColumn("dbo.AspNetUsers", "NoMeat", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoDairy", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoGluten", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "DietaryNotes", c => c.String());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "DietaryNotes");
            DropColumn("dbo.AspNetUsers", "NoGluten");
            DropColumn("dbo.AspNetUsers", "NoDairy");
            DropColumn("dbo.AspNetUsers", "NoMeat");
            DropColumn("dbo.AspNetUsers", "Notes");
            DropColumn("dbo.AspNetUsers", "RsvpPlusOne");
            DropColumn("dbo.AspNetUsers", "HasPlusOne");
            DropColumn("dbo.AspNetUsers", "RsvpDate");
            DropColumn("dbo.AspNetUsers", "DidRsvp");
            DropColumn("dbo.AspNetUsers", "CanAttend");
        }
    }
}
