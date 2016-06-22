namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstLastNameToPlusOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InviteMessage", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstNamePlusOne", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastNamePlusOne", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastNamePlusOne");
            DropColumn("dbo.AspNetUsers", "FirstNamePlusOne");
            DropColumn("dbo.AspNetUsers", "InviteMessage");
        }
    }
}
