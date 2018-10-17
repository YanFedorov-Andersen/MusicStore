namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changinguser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccount", "IdentityKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccount", "IdentityKey");
        }
    }
}
