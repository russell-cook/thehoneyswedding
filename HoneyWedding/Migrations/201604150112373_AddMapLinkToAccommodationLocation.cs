namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMapLinkToAccommodationLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationLocations", "MapLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationLocations", "MapLink");
        }
    }
}
