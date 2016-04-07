namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBallerRatingToAccommodationModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationLocations", "BallerRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationLocations", "BallerRating");
        }
    }
}
