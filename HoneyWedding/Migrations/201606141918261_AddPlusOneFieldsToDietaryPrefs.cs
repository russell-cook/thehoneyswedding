namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlusOneFieldsToDietaryPrefs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NoMeatPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoDairyPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoGlutenPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "DietaryNotesPlusOne", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DietaryNotesPlusOne");
            DropColumn("dbo.AspNetUsers", "NoGlutenPlusOne");
            DropColumn("dbo.AspNetUsers", "NoDairyPlusOne");
            DropColumn("dbo.AspNetUsers", "NoMeatPlusOne");
        }
    }
}
