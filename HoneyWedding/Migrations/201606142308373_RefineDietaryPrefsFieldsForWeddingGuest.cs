namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefineDietaryPrefsFieldsForWeddingGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Meatless", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Vegan", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "MeatlessPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "VeganPlusOne", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "NoMeat");
            DropColumn("dbo.AspNetUsers", "NoDairy");
            DropColumn("dbo.AspNetUsers", "NoMeatPlusOne");
            DropColumn("dbo.AspNetUsers", "NoDairyPlusOne");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NoDairyPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoMeatPlusOne", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoDairy", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NoMeat", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "VeganPlusOne");
            DropColumn("dbo.AspNetUsers", "MeatlessPlusOne");
            DropColumn("dbo.AspNetUsers", "Vegan");
            DropColumn("dbo.AspNetUsers", "Meatless");
        }
    }
}
