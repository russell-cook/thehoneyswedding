namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccommodationModelsAndController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Notes = c.String(),
                        HoneyComments = c.String(),
                        InFairPlay = c.Boolean(nullable: false),
                        DistanceFromVenue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccommodationRooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationLocationID = c.Int(nullable: false),
                        RoomName = c.String(),
                        SleepsTotal = c.Int(nullable: false),
                        SleepsBed = c.Int(nullable: false),
                        SleepsSofa = c.Int(nullable: false),
                        CostNightly = c.Decimal(precision: 18, scale: 2),
                        MinNights = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationLocations", t => t.AccommodationLocationID, cascadeDelete: true)
                .Index(t => t.AccommodationLocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccommodationRooms", "AccommodationLocationID", "dbo.AccommodationLocations");
            DropIndex("dbo.AccommodationRooms", new[] { "AccommodationLocationID" });
            DropTable("dbo.AccommodationRooms");
            DropTable("dbo.AccommodationLocations");
        }
    }
}
