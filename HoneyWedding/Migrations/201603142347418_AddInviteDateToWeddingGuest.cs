namespace HoneyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInviteDateToWeddingGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InviteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "InviteDate");
        }
    }
}
