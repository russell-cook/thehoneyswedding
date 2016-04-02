namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageAndAvailabilityToAccommodations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationLocations", "Img", c => c.String());
            AddColumn("dbo.AccommodationRooms", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationRooms", "IsAvailable");
            DropColumn("dbo.AccommodationLocations", "Img");
        }
    }
}
