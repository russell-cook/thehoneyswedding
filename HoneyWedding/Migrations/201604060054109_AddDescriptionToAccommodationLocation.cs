namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToAccommodationLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationLocations", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationLocations", "Description");
        }
    }
}
